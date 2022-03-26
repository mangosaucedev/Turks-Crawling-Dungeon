using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD
{
    public class FieldOfView : MonoBehaviour
    {
        private const int SIGHT_RADIUS = 10;
        private const float INVALID_SLOPE = -9999f;

        public static HashSet<Vector2Int> visiblePositions = new HashSet<Vector2Int>();

        private static FieldOfView current;
        private static Camera mainCamera;
        private static Tilemap tilemap;
        private static Tilemap ground;
        private static Quadrant currentQuadrant;

        private TileBase unseenTile;
        private BoundsInt bounds;

        public static Tilemap Tilemap
        {
            get
            {
                if (!tilemap)
                {
                    FogOfWarTilemapManager manager = ServiceLocator.Get<FogOfWarTilemapManager>();
                    tilemap = manager.Tilemap;
                }
                return tilemap;
            }
        }

        public static Tilemap Ground
        {
            get
            {
                if (!ground)
                {
                    GroundTilemapManager manager = ServiceLocator.Get<GroundTilemapManager>();
                    ground = manager.Tilemap;
                }
                return ground;
            }
        }

        private static FieldOfView Current
        {
            get
            {
                if (!current)
                    current = ServiceLocator.Get<FieldOfView>();
                return current;
            }
        }

        private static Camera MainCamera
        {
            get
            {
                if (!mainCamera)
                    mainCamera = Camera.main;
                return mainCamera;
            }
        }

        private TileBase UnseenTile
        {
            get
            {
                if (!unseenTile)
                    unseenTile = Assets.Get<TileBase>("Unseen");
                return unseenTile;
            }
        }

        private void OnEnable()
        {
            EventManager.Listen<AfterTurnTickEvent>(this, OnAfterTurnTick);
            EventManager.Listen<ZoneGenerationFinishedEvent>(this, OnZoneGenerationFinished);
        }

        private void OnDisable()
        {
            EventManager.StopListening<AfterTurnTickEvent>(this);
            EventManager.StopListening<ZoneGenerationFinishedEvent>(this);

        }

        private void OnAfterTurnTick(AfterTurnTickEvent e)
        {
            UpdateNextFrame();
        }

        private void OnZoneGenerationFinished(ZoneGenerationFinishedEvent e)
        {
            InitializeFogOfWar();
            UpdateNextFrame();
        }

        public static Vector2Int CellToGrid(int x, int y)
        {
            return CellToGrid(new Vector3Int(x, y, 0));
        }

        public static Vector2Int CellToGrid(Vector3Int position)
        {
            Vector3 worldPosition = Tilemap.CellToWorld(position);
            return GameGrid.WorldToGrid(worldPosition);
        }

        public static Vector3Int GridToCell(Vector2Int position)
        {
            Vector3 worldPosition = GameGrid.GridToWorld(position);
            return Tilemap.WorldToCell(worldPosition);
        }

        public static bool IsVisible(Vector2Int position) => IsVisible(position.x, position.y);

        public static bool IsVisible(int x, int y)
        {
            Vector3Int position = new Vector3Int(x, y, 0);
            return !Tilemap.HasTile(position);
        }

        public void InitializeFogOfWar()
        {
            bounds = Ground.cellBounds;

            for (int x = bounds.xMin; x < bounds.xMax; x++)
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    Tilemap.SetTile(position, UnseenTile);
                }
        }

        public static void UpdateNextFrame()
        {
            Current.StartCoroutine(UpdateNextFrameRoutine());
        }

        public static IEnumerator UpdateNextFrameRoutine()
        {
            yield return null;
            UpdateFieldOfView();
        }

        public static void UpdateFieldOfView()
        {
            visiblePositions.Clear();
            WipeScreen();
            RevealPlayerPosition();
            ScanQuadrantsForBlockedTiles();
        }

        public static void WipeScreen()
        {
            float padding = 0.5f;
            Vector2 cameraMin = MainCamera.ViewportToWorldPoint(new Vector2(-padding, -padding));
            Vector2 cameraMax = MainCamera.ViewportToWorldPoint(new Vector2(1 + padding, 1 + padding));
            Vector3Int cellMin = Tilemap.WorldToCell(cameraMin);
            Vector3Int cellMax = Tilemap.WorldToCell(cameraMax);

            int xMin = cellMin.x;
            int xMax = cellMax.x;
            int yMin = cellMin.y;
            int yMax = cellMax.y;

            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    if (!Tilemap.HasTile(position))
                        Tilemap.SetTile(position, Current.unseenTile);
                }
        }

        private static void RevealPlayerPosition()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Vector2Int playerPosition = player.cell.Position;
            RevealGridCell(playerPosition);
        }

        private static void RevealGridCell(Vector2Int position)
        {
            Vector3Int tilePosition = GridToCell(position);
            Tilemap.SetTile(tilePosition, null);
            visiblePositions.Add(position);
        }

        private static void ScanQuadrantsForBlockedTiles()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Vector2Int playerPosition = player.cell.Position;

            for (int i = 0; i < 4; i++)
            {
                Cardinal cardinal = (Cardinal) i;
                currentQuadrant = new Quadrant(cardinal, playerPosition);
                Row firstRow = new Row(1, -1, 1);
                ScanRowInQuadrant(firstRow);
            }
        }

        private static void ScanRowInQuadrant(Row row)
        {
            FOVLocalCell previousCell = null;

            foreach (FOVLocalCell cell in row.GetLocalCells())
            {
                if (cell.Depth > SIGHT_RADIUS)
                    return;

                if ((IsWall(cell) || IsLocalCellSymmetricInRow(cell, row)) && IsWithinDistance(cell))
                    RevealLocalCell(cell);

                if (IsWall(previousCell) && IsFloor(cell))
                    row.startSlope = GetLocalCellSlope(cell);

                if (IsFloor(previousCell) && IsWall(cell))
                {
                    Row nextRow = row.GetNext();
                    nextRow.endSlope = GetLocalCellSlope(cell);
                    ScanRowInQuadrant(nextRow);
                }

                previousCell = cell;
            }

            if (IsFloor(previousCell))
                ScanRowInQuadrant(row.GetNext());
        }

        private static bool IsWall(FOVLocalCell cell)
        {
            Vector2Int gridPosition = LocalToGridCell(cell);

            if (!GameGrid.Current.IsWithinBounds(gridPosition) || cell == null)
                return false;

            return IsGridCellLightBlocker(gridPosition);
        }

        private static bool IsFloor(FOVLocalCell cell)
        {
            Vector2Int gridPosition = LocalToGridCell(cell);

            if (!GameGrid.Current.IsWithinBounds(gridPosition) || cell == null)
                return false;

            return !IsGridCellLightBlocker(gridPosition);
        }

        private static bool IsWithinDistance(FOVLocalCell cell)
        {
            Vector2Int gridPosition = LocalToGridCell(cell);
            BaseObject player = PlayerInfo.currentPlayer;
            Vector2Int playerPosition = player.cell.Position;
            int distance = Mathf.FloorToInt(Vector2Int.Distance(gridPosition, playerPosition));
            return distance <= SIGHT_RADIUS;
        }

        private static bool IsGridCellLightBlocker(Vector2Int gridPosition)
        {
            GameGrid grid = CurrentZoneInfo.grid;
            Cell gridCell = grid[gridPosition];
            foreach (BaseObject obj in gridCell.objects)
            {
                if (obj.Parts.TryGet(out Obstacle obstacle) &&
                    obstacle.OccludesLineOfSight)
                    return true;
            }
            return false;
        }

        private static bool IsLocalCellSymmetricInRow(FOVLocalCell cell, Row row)
        {
            int position = cell.Position;
            return (position >= row.depth * row.startSlope && 
                    position <= row.depth * row.endSlope);
        }

        private static Vector2Int LocalToGridCell(FOVLocalCell cell)
        {
            if (cell == null)
                return -Vector2Int.one;
            Vector2Int localPosition = cell.localPosition;
            return currentQuadrant.LocalToGridPosition(localPosition);
        }

        private static void RevealLocalCell(FOVLocalCell cell)
        {
            Vector2Int gridPosition = currentQuadrant.LocalToGridPosition(cell.localPosition);
            RevealGridCell(gridPosition);
        }

        private static float GetLocalCellSlope(FOVLocalCell cell) =>
            ((float) (2f * cell.Position) - 1f) / (float) (2f * cell.Depth);
    }
}