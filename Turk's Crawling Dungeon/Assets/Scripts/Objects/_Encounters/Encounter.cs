using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones;

namespace TCD.Objects.Encounters
{
    public class Encounter : ICloneable
    {
        public string name;
        public int tier;
        public float weight;
        public EncounterType type;
        public EncounterDensity density;
        public bool forced;
        public bool exclusive;
        public List<EncounterObject> objects = new List<EncounterObject>();

        private List<BaseObject> lastBuiltObjects = new List<BaseObject>();

        public List<BaseObject> BuildObjects(Vector2Int position)
        {
            lastBuiltObjects.Clear();

            foreach (EncounterObject obj in GetForcedObjects())
                BuildObject(obj, position);

            var randomObjs = GetRandomItems();
            foreach (var randomObj in randomObjs)
            {
                if (randomObj != null)
                    BuildObject(randomObj, position);
            }

            return lastBuiltObjects;
        }

        public List<BaseObject> BuildForcedObjects(Vector2Int position)
        {
            lastBuiltObjects.Clear();

            foreach (EncounterObject obj in GetForcedObjects())
                BuildObject(obj, position);

            return lastBuiltObjects;
        }

        private void BuildObject(EncounterObject encounterObject, Vector2Int position)
        {
            List<string> objects = encounterObject.GetObjects();
            foreach (string obj in objects)
                lastBuiltObjects.Add(ObjectFactory.BuildFromBlueprint(obj, position));
        }

        public void BuildAtPosition(Vector2Int position)
        {
            foreach (EncounterObject obj in GetForcedObjects())
                PlaceObjectRandomly(obj, position);
            var randomObjs = GetRandomItems();
            foreach (var randomObj in randomObjs)
            {
                if (randomObj != null)
                    PlaceObjectRandomly(randomObj, position);
            }
        }

        private List<EncounterObject> GetForcedObjects()
        {
            List<EncounterObject> forcedObjects = new List<EncounterObject>();
            foreach (EncounterObject obj in objects)
            {
                if (obj.forced)
                    forcedObjects.Add(obj);
            }
            return forcedObjects;
        }

        private void PlaceObjectRandomly(EncounterObject encounterObject, Vector2Int startPosition)
        {
            List<string> objects = encounterObject.GetObjects();
            foreach (string obj in objects)
            {
                if (TryGetRandomPointNearPosition(startPosition, out Vector2Int position))
                    ObjectFactory.BuildFromBlueprint(obj, position);
            }
        }

        private bool TryGetRandomPointNearPosition(Vector2Int startPosition, out Vector2Int position)
        {
            position = Vector2Int.zero;
            int radius = GetPlacementRadius();
            List<Vector2Int> positions = new List<Vector2Int>();
            int xMin = startPosition.x - radius;
            int xMax = startPosition.x + radius;
            int yMin = startPosition.y - radius;
            int yMax = startPosition.y + radius;
            for (int x = xMin; x <= xMax; x++)
                for (int y = yMin; y <= yMax; y++)
                {
                    if (PositionChecker.IsEmpty(x, y))
                        positions.Add(new Vector2Int(x, y));
                }
            if (positions.Count > 0)
            {
                position = Choose.Random(positions);
                return true;
            }
            return false;
        }

        private int GetPlacementRadius()
        {
            switch (density)
            {
                default:
                    return 1;
                case EncounterDensity.Tight:
                    return 3;
                case EncounterDensity.Loose:
                    return 6;
                case EncounterDensity.Sparse:
                    return 12;
            }
        }

        private List<EncounterObject> GetRandomItems()
        {
            List<EncounterObject> items = new List<EncounterObject>();
            using (GrabBag<EncounterObject> bag = new GrabBag<EncounterObject>())
            {
                foreach (EncounterObject obj in objects)
                {
                    if (RandomInfo.Random.Next(100) <= obj.chanceIn100)
                        bag.AddItem(obj, 1);
                }
                if (bag.Count > 1)
                {
                    for (int i = 0; i < RandomInfo.Random.Next(1, bag.Count); i++)
                        items.Add(bag.Grab());
                } 
                else
                    items.Add(bag.Grab());
            }
            return items;
        }

        public object Clone()
        {
            Encounter encounter = (Encounter) MemberwiseClone();
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                EncounterObject obj = objects[i];
                encounter.objects[i] = obj;
            }
            return encounter;
        }
    }
}
