using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class ChamberPlanner : ZoneGeneratorMachine
    {
        private const int BORDER_SIZE = 8;
        private const int X_STEP = 12;
        private const int Y_STEP = 12;
        private const int MAX_HEAP_SIZE = 1024;

        private IChamber currentChamber;
        private int previousChamberX;
        private int previousChamberY;
        private Heap<ChamberChildPosition> currentPositions = 
            new Heap<ChamberChildPosition>(MAX_HEAP_SIZE);
        private bool currentChamberPlacedSuccessfully;
        private TGrid<ChamberCellType> chamberCells;

        private int MaxChambers => ZoneParams.MaxChambers;

        private int MinChamberWidth => ZoneParams.MinChamberWidth;
        
        private int MinChamberHeight => ZoneParams.MinChamberHeight;

        private int MaxChamberWidth => ZoneParams.MaxChamberWidth;
        
        private int MaxChamberHeight => ZoneParams.MaxChamberHeight;

        private int ChambersCount => Zone.Chambers.Count;

        private int MinChamberChildDistance => ZoneParams.MinChamberChildDistance;

        private int MinChamberBufferDistance => ZoneParams.MinChamberBufferDistance;

        public override IEnumerator Generate()
        {
            chamberCells = new TGrid<ChamberCellType>(Width, Height);
            MakeBorder();
            yield return AccreteRooms();
        }

        private void MakeBorder()
        {
            for (int x = 0; x < BORDER_SIZE; x++)
                for (int y = 0; y < Zone.Height; y++)
                {
                    chamberCells[x, y] = ChamberCellType.Border;
                }
            for (int x = Zone.Width - BORDER_SIZE - 1; x < Zone.Width; x++)
                for (int y = 0; y < Zone.Height; y++)
                {
                    chamberCells[x, y] = ChamberCellType.Border;
                }
            for (int x = 0; x < Zone.Width; x++)
                for (int y = 0; y < BORDER_SIZE; y++)
                {
                    chamberCells[x, y] = ChamberCellType.Border;
                }
            for (int x = 0; x < Zone.Width; x++)
                for (int y = Zone.Height - BORDER_SIZE - 1; y < Zone.Height; y++)
                {
                    chamberCells[x, y] = ChamberCellType.Border;
                }
        }

        private IEnumerator AccreteRooms()
        {
            while (ChambersCount < MaxChambers)
            {
                int width = Random.Range(MinChamberWidth, MaxChamberWidth);
                int height = Random.Range(MinChamberHeight, MaxChamberHeight);
                currentChamber = ChamberFactory.Build(width, height);
                Vector2Int startPosition = GetStartPositionForChildChamber();
                yield return TryPlacingCurrentChamberFromPosition(startPosition);
                if (!currentChamberPlacedSuccessfully)
                    yield break;
            }
        }

        private Vector2Int GetStartPositionForChildChamber()
        {
            int direction = Choose.Random(-1, 1);
            int maxDistance = Mathf.Min(Width, Height) / 24;
            int distance = Random.Range(MinChamberChildDistance, maxDistance);
            float xMagnitude = 1f - Random.Range(0f, 1f);
            float yMagnitude = 1f - xMagnitude;
            int xOffset = Mathf.RoundToInt(xMagnitude * direction * distance);
            int yOffset = Mathf.RoundToInt(yMagnitude * direction * distance);
            int x = previousChamberX + xOffset;
            int y = previousChamberY + yOffset;
            x = Mathf.Clamp(x, 0, Width);
            y = Mathf.Clamp(y, 0, Height);
            return new Vector2Int(x, y);
        }

        private IEnumerator TryPlacingCurrentChamberFromPosition(Vector2Int startPosition)
        {
            int startX = startPosition.x;
            int startY = startPosition.y;
            currentPositions.Reset();
            currentChamberPlacedSuccessfully = false;
            for (int i = 0; i < Width; i += X_STEP)
                for (int j = 0; j < Height; j += Y_STEP)
                {
                    int x = (startX + i) < Width ? startX + i : startX + i - Width;
                    int y = (startY + j) < Height ? startY + j : startY + j - Height;
                    if (BufferSpaceIsClear(x, y) && 
                        IsValidPositionForCurrentChamber(x, y))
                    {
                        Vector2Int position = new Vector2Int(x, y);
                        ChamberChildPosition childPosition =
                            new ChamberChildPosition(startPosition, position);
                        currentPositions.Add(childPosition);
                    }
                }
            ChamberChildPosition chosenPosition = currentPositions.RemoveFirst();
            if (chosenPosition != null)
            {
                currentChamberPlacedSuccessfully = true;
                AddCurrentChamberCellsToPlan(chosenPosition.position);
            }
            yield break;
        }

        private bool BufferSpaceIsClear(int originX, int originY)
        {
            int xMin = originX - MinChamberBufferDistance;
            int xMax = originX + currentChamber.Width + MinChamberBufferDistance;
            int yMin = originY - MinChamberBufferDistance;
            int yMax = originY + currentChamber.Height + MinChamberBufferDistance;
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    bool inChamber = x >= originX && x < originX + currentChamber.Width &&
                        y >= originY && y < originY + currentChamber.Height;
                    if (inChamber)
                        continue;
                    bool outOfBounds = x < 0 || x >= Width || y < 0 || y >= Height;
                    if (outOfBounds || PositionOverlapsExistingChamber(x, y))
                        return false;
                }
            return true;
        }

        private bool IsValidPositionForCurrentChamber(int xMin, int yMin)
        {
            int width = currentChamber.Width;
            int height = currentChamber.Height;
            for (int xChamber = 0; xChamber < width; xChamber++)
                for (int yChamber = 0; yChamber < height; yChamber++)
                {
                    int xReal = xMin + xChamber;
                    int yReal = yMin + yChamber;

                    bool outOfBounds = xReal < 0 || xReal >= Width || 
                        yReal < 0 || yReal >= Height;
                    if (outOfBounds)
                        return false;

                    bool overlapsExistingChamber = 
                        PositionOverlapsExistingChamber(xReal, yReal);
                    if (currentChamber.Cells[xChamber, yChamber] != ChamberCellType.None &&
                        overlapsExistingChamber)
                        return false;
                }
            return true;
        }

        private bool PositionOverlapsExistingChamber(int x, int y) =>
            chamberCells[x, y] != ChamberCellType.None;

        private void AddCurrentChamberCellsToPlan(Vector2Int position)
        {
            int xMin = position.x;
            int yMin = position.y;
            int width = currentChamber.Width;
            int height = currentChamber.Height;
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    int xReal = xMin + x;
                    int yReal = yMin + y;
                    ChamberCellType cell = currentChamber.Cells[x, y];
                    chamberCells[xReal, yReal] = cell;
                    if (cell >= ChamberCellType.Floor)
                        currentChamber.OccupiedPositions.Add(new Vector2Int(xReal, yReal));
                }
            previousChamberX = xMin;
            previousChamberY = yMin;
            currentChamber.SetPosition(xMin, yMin);
            Zone.Chambers.Add(currentChamber);
            Zone.Features.Add(currentChamber);
        }
    }
}
