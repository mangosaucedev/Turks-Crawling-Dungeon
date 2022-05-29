using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Templates
{
    public class ZoneTemplate 
    {
        public const char EMPTY_CHAR = '.';
        
        private const char FLOOR_CHAR = ':';
        private const char WALL_CHAR = 'W';
        private const char DOOR_CHAR = 'D';
        private const char EXIT_CHAR = '+';

        public readonly TGrid<ZoneTemplateTile> tiles;
        public readonly List<ZoneTemplateSymbol> symbols = new List<ZoneTemplateSymbol>();
        
        private readonly Dictionary<char, List<ZoneTemplateSymbol>> symbolsByChar = 
            new Dictionary<char, List<ZoneTemplateSymbol>>();

        private TGrid<char> characters;

        public int Width => tiles.width;

        public int Height => tiles.height;  

        public ZoneTemplate(TGrid<char> characters)
        { 
            this.characters = characters;
            tiles = new TGrid<ZoneTemplateTile>(characters.width, characters.height);
            ReadTiles();
        }

        private void ReadTiles()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    char character = characters[x, y]; 
                    var tile = GetTile(character);
                    tiles[x, y] = tile;

                    if (tile == ZoneTemplateTile.Symbol)
                        AddSymbol(new Vector2Int(x, y), character);
                }
        }

        private ZoneTemplateTile GetTile(char character)
        {
            switch (character)
            {
                case EMPTY_CHAR:
                    return ZoneTemplateTile.Empty;
                case FLOOR_CHAR:
                    return ZoneTemplateTile.Floor;
                case WALL_CHAR:
                    return ZoneTemplateTile.Wall;
                case DOOR_CHAR:
                    return ZoneTemplateTile.Door;
                case EXIT_CHAR:
                    return ZoneTemplateTile.Exit;
                default:
                    return ZoneTemplateTile.Symbol;
            }
        }

        private void AddSymbol(Vector2Int position, char character)
        {
            var symbol = new ZoneTemplateSymbol { position = position, character = character };
            symbols.Add(symbol);
            GetSymbols(character).Add(symbol);  
        }

        public List<ZoneTemplateSymbol> GetSymbols(char character)
        {
            if (!symbolsByChar.TryGetValue(character, out var symbols))
            {
                symbols = new List<ZoneTemplateSymbol>();
                symbolsByChar[character] = symbols; 
            }
            return symbols;
        }
    }
}
