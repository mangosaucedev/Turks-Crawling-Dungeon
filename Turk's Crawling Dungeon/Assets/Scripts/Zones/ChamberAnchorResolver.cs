using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class ChamberAnchorResolver
    {
        public struct AnchorCell
        {
            public int x;
            public int y;
            public Direction direction;

            public AnchorCell(int x, int y, Direction direction)
            {
                this.x = x;
                this.y = y;
                this.direction = direction;
            }
        }

        private const int SURROUNDING_CELLS_FOR_ANCHOR = 5;
        private const int MIN_ANCHORS = 1;
        private const int MAX_ANCHORS_PLUS_ONE = 6;

        private static readonly List<Direction> validAnchorDirections =
            new List<Direction> { 
                Direction.North, Direction.South, Direction.West, Direction.East};

        private IChamber chamber;
        private int currentX;
        private int currentY;
        private List<AnchorCell> currentNeighbors = 
            new List<AnchorCell>();
        private List<AnchorCell> validPositions = 
            new List<AnchorCell>();
        private Dictionary<Direction, int> anchorsInDirection =
            new Dictionary<Direction, int>();
        private int anchorCount;
        private int anchorsPlaced;

        private TGrid<ChamberCellType> Cells => chamber.Cells;

        private int Width => Cells.width;

        private int Height => Cells.height;

        private List<ChamberAnchor> Anchors => chamber.Anchors;

        private int AnchorCount
        {
            get
            {
                if (anchorCount == 0)
                    anchorCount = 
                        Random.Range(MIN_ANCHORS, MAX_ANCHORS_PLUS_ONE);
                return anchorCount;
            }
        }

        public ChamberAnchorResolver(IChamber chamber)
        {
            this.chamber = chamber;
        }

        public void ResolveAnchors()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    if (Cells[x, y] != ChamberCellType.None)
                    {
                        currentX = x;
                        currentY = y;
                        currentNeighbors.Clear();
                        EvaluateIfValidAnchorPoint();
                    }
                }
            PlaceRandomAnchors();
            if (anchorsPlaced == 0)
                PlaceRandomAnchor();
            if (anchorsPlaced == 0)
                throw new ChamberException(
                    "Chamber Anchor Resolver could not place any anchors " +
                    "successfully!");
        }

        private void EvaluateIfValidAnchorPoint()
        {
            for (int xoffset = -1; xoffset <= 1; xoffset++)
                for (int yoffset = -1; yoffset <= 1; yoffset++)
                {
                    if (xoffset == 0 && yoffset == 0)
                        continue;
                    int checkX = currentX + xoffset;
                    int checkY = currentY + yoffset;
                    if (!EvaluateNeighbor(checkX, checkY))
                        return;
                    if (currentNeighbors.Count > SURROUNDING_CELLS_FOR_ANCHOR)
                        return;
                }
            if (currentNeighbors.Count == SURROUNDING_CELLS_FOR_ANCHOR)
                AddCellToValidPositions();
        }

        private bool EvaluateNeighbor(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return true;
            ChamberCellType cell = Cells[x, y];
            if (cell == ChamberCellType.Anchor)
                return false;
            if (cell != ChamberCellType.None)
            {
                int directionX = x - currentX;
                int directionY = y - currentY;
                Direction direction = Direction.North;
                if (directionX == -1 && directionY == 1)
                    direction = Direction.NorthWest;
                if (directionX == 1 && directionY == 1)
                    direction = Direction.NorthEast;
                if (directionX == -1 && directionY == 0)
                    direction = Direction.West;
                if (directionX == 1 && directionY == 0)
                    direction = Direction.East;
                if (directionX == -1 && directionY == -1)
                    direction = Direction.SouthWest;
                if (directionX == 1 && directionY == -1)
                    direction = Direction.SouthEast;
                AnchorCell neighbor = new AnchorCell(x, y, direction);
                currentNeighbors.Add(neighbor);
            } 
            return true;
        }

        private void AddCellToValidPositions()
        {
            Direction direction = GetAnchorDirectionBasedOnNeighbors();
            AnchorCell anchorCell = new AnchorCell(currentX, currentY, direction);
            validPositions.Add(anchorCell);
        }

        private Direction GetAnchorDirectionBasedOnNeighbors()
        {
            bool hasNorthWest = HasNeighborToDirection(Direction.NorthWest);
            bool hasNorth = HasNeighborToDirection(Direction.North);
            bool hasNorthEast = HasNeighborToDirection(Direction.NorthEast);
            bool hasWest = HasNeighborToDirection(Direction.West);
            bool hasEast = HasNeighborToDirection(Direction.East);
            bool hasSouthWest = HasNeighborToDirection(Direction.SouthWest);
            bool hasSouth = HasNeighborToDirection(Direction.South);
            bool hasSouthEast = HasNeighborToDirection(Direction.SouthEast);
            if (hasNorthWest && hasNorth && hasNorthEast && hasWest && hasEast)
                return Direction.South;
            if (hasNorth && hasNorthEast && hasEast && hasSouth && hasSouthEast)
                return Direction.West;
            if (hasNorthWest && hasNorth && hasWest && hasSouthWest && hasSouth)
                return Direction.East;
            return Direction.North;
        }

        private bool HasNeighborToDirection(Direction direction)
        {
            foreach (AnchorCell neighbor in currentNeighbors)
            {
                if (neighbor.direction == direction)
                    return true;
            }
            return false;
        }

        private void PlaceRandomAnchors()
        {
            if (validPositions.Count == 0)
                throw new ChamberException(
                    "No valid anchor positions found by Chamber Anchor Resolver!");
            AssignZeroToAnchorsInDirectionEntries();
            AssignRandomRangeForDirections();
            foreach (Direction direction in validAnchorDirections)
                PlaceAnchorsInDirection(direction);
        }

        private void AssignZeroToAnchorsInDirectionEntries()
        {
            foreach (Direction direction in validAnchorDirections)
                anchorsInDirection[direction] = 0;
        }

        private void AssignRandomRangeForDirections()
        {
            for (int i = 0; i < AnchorCount; i++)
            {
                Direction direction = Choose.Random(validAnchorDirections);
                anchorsInDirection[direction] += 1;
            }
        }

        private void PlaceAnchorsInDirection(Direction direction)
        {
            int count = anchorsInDirection[direction];
            List<AnchorCell> anchors = SelectValidPositionForDirection(direction);
            for (int i = 0; i < count; i++)
            {
                if (anchors.Count == 0)
                    return;
                int index = Random.Range(0, anchors.Count);
                AnchorCell anchorCell = anchors[index];
                PlaceAnchor(anchorCell);
            }
        }

        private List<AnchorCell> SelectValidPositionForDirection(
            Direction direction)
        {
            List<AnchorCell> list = new List<AnchorCell>();
            foreach (AnchorCell cell in validPositions)
            {
                if (cell.direction == direction)
                    list.Add(cell);
            }
            return list;
        }

        private void PlaceAnchor(AnchorCell anchorCell)
        {
            int x = anchorCell.x;
            int y = anchorCell.y;
            Cardinal cardinal = DirectionToCardinal(anchorCell.direction);
            ChamberAnchor anchor = new ChamberAnchor(chamber, x, y, cardinal); 
            Anchors.Add(anchor);
            Cells[x, y] = ChamberCellType.Anchor;
            anchorsPlaced++;
        }

        private Cardinal DirectionToCardinal(Direction direction)
        {
            if (direction == Direction.North)
                return Cardinal.North;
            if (direction == Direction.South)
                return Cardinal.South;
            if (direction == Direction.West)
                return Cardinal.West;
            if (direction == Direction.East)
                return Cardinal.East;
            throw new ChamberException(
                "Chamber Anchor Resolver encountered invalid anchor cell " +
                "direction and could not convert it into a valid cardinal!");
        }

        private void PlaceRandomAnchor()
        {
            if (validPositions.Count == 0)
                return;
            int index = Random.Range(0, validPositions.Count);
            AnchorCell anchorCell = validPositions[index];
            PlaceAnchor(anchorCell);
        }
    }
}
