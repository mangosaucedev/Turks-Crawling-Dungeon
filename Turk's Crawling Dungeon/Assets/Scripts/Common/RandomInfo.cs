using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace TCD
{
    public static class RandomInfo
    {
        private static string seedString;
        private static int seed;
        private static Random random;

        public static string SeedString
        {
            get
            {
                if (string.IsNullOrEmpty(seedString))
                    seedString = GenerateRandomSeedString();
                return seedString;
            }
            set => seedString = value;
        }

        public static int Seed
        {
            get
            {
                if (seed == default)
                    seed = SeedString.GetHashCode();
                return seed;
            }
            set => seed = value;
        }

        public static Random Random
        {
            get
            {
                if (random == null)
                    random = new Random(Seed);
                return random;
            }
        }

        private static string GenerateRandomSeedString()
        {
            int characters = UnityEngine.Random.Range(16, 32);
            StringBuilder stringBuilder = new StringBuilder(characters);
            char[] possibleCharacters = new char[]
                { 'a', 'A', 'b', 'B', 'c', 'C', 'd', 'D', 'e', 'E', 'f', 'F', 'g', 'G', 'h', 'H', 'i', 'I',
                  'j', 'J', 'k', 'K', 'l', 'L', 'm', 'M', 'n', 'N', 'o', 'O', 'p', 'P', 'q', 'Q', 'r', 'R',
                  's', 'S', 't', 'T', 'u', 'U', 'v', 'V', 'w', 'W', 'x', 'X', 'y', 'Y', 'z', 'Z', '0', '1',
                  '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '+', '[', ']', ';', '<', '>', '?', '!' };
            int possibleCharacterCount = possibleCharacters.Length;

            for (int i = 0; i < characters; i++)
            {
                int index = UnityEngine.Random.Range(0, possibleCharacterCount);
                char character = possibleCharacters[index];
                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }
    }
}
