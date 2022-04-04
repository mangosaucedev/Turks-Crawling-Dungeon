using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TCD.Texts
{
    public class GameText
    {
        private const string PATTERN =
            @"(?<command>((?!\<(i|b)\>|\<(size|color|material|quad))(\<(?<type>[a-zA-Z0-9]+)((\s*)\=(\s*)(((?<argument>[a-zA-Z0-9_]+)|)(,|))+|)(\>(?<text>([^\<\>]+|))(\<\/\>)|\/\>))))";

        private static Regex regex = new Regex(PATTERN, RegexOptions.IgnoreCase);

        public string name;
        public List<Command> commands = new List<Command>();

        private string text;
        private string parsedText;
        private Match currentMatch;

        public GameText(string text)
        {
            this.text = text;
            ParseString();
        }

        public static implicit operator string(GameText text) => text.ToString();

        public override string ToString() => parsedText;

        private void ParseString()
        {
            parsedText = text;
            currentMatch = regex.Match(parsedText);
            while (currentMatch.Success)
            {
                ParseCommand();
                currentMatch = regex.Match(parsedText);
            }
        }

        private void ParseCommand()
        {
            CommandType commandType = GetCommandType();

            if (commandType > CommandType.Unknown)
            {
                Command command = CommandFactory.GetFromType(commandType);
                command.arguments = GetCommandArguments();

                string text = GetText();
                command.HandleText(ref text);
                ExtractTextFromCommand(text);

                command.startIndex = currentMatch.Index;
                command.endIndex = currentMatch.Index + command.Length;
                commands.Add(command);
            }
            else
                ExtractTextFromCommand(GetText());
        }

        private CommandType GetCommandType()
        {
            Group group = currentMatch.Groups["type"];
            
            if (!group.Success)
                ExceptionHandler.Handle(new CommandParseException(CommandParseException.TYPE_NOT_FOUND));
            
            string type = group.Value;
            return CommandFactory.GetCommandType(type);
        }

        private string[] GetCommandArguments()
        {
            Group group = currentMatch.Groups["argument"];

            if (!group.Success)
                return new string[] { };

            CaptureCollection captures = group.Captures;
            int count = captures.Count;
            string[] arguments = new string[count];

            for (int i = 0; i < count; i++)
            {
                Capture capture = captures[i];
                arguments[i] = capture.Value;
            }

            return arguments;
        }

        private string GetText()
        {
            Group group = currentMatch.Groups["text"];
            
            if (!group.Success)
                return "";

            return group.Value;
        }

        private void ExtractTextFromCommand(string text)
        {
            parsedText = parsedText.Remove(currentMatch.Index, currentMatch.Value.Length);
            parsedText = parsedText.Insert(currentMatch.Index, text);
        }
    }
}
