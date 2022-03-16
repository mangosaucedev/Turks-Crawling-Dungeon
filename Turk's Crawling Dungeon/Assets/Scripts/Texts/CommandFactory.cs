using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Texts
{
    public static class CommandFactory 
    {
        public static CommandType GetCommandType(string type)
        {
            switch (type.ToLower())
            {
                case "c":
                    return CommandType.AdvColor;
                case "v":
                case "var":
                case "variable":
                    return CommandType.DisplayVariable;
                case "sv":
                case "set":
                case "setvar":
                case "setvariable":
                    return CommandType.SetVariable;
                case "l":
                case "link":
                    return CommandType.EncyclopediaLink;
                default:
                    return CommandType.Unknown;
            }
        }

        public static Command GetFromType(CommandType type)
        {
            switch (type)
            {
                case CommandType.AdvColor:
                    return new AdvancedColor();
                case CommandType.DisplayVariable:
                    return new DisplayVariable();
                default: 
                    throw new CommandParseException(CommandParseException.INVALID_TYPE);
            }
        }
    }
}
