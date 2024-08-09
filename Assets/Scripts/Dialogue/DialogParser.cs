using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogParser
    {
        private const string commandRegexPattern = "\\w*[^\\s]\\(";
        public static DialogLine Parse(string rawLine)
        {
            Debug.Log($"Parsing line- '{rawLine}'");

            (string speaker, string dialogue, string commands) = RipContent(rawLine);

            Debug.Log($"Speaker = '{speaker}'\nDialogue = '{dialogue}'\nCommands = '{commands}'");

            return new DialogLine(speaker, dialogue, commands);
        }

        //Get three different contents of a line 
        //Pass all the different fields back
        //This is a tuple
        private static (string, string, string) RipContent(string rawLine)
        {
            string speaker = "", dialogue = "", commands = "";
            //Variables for finding quotation marks
            int dialogueStart = -1;
            int dialogueEnd = -1;
            bool isEscaped = false;

            for (int i = 0; i < rawLine.Length; i++)
            {
                char current = rawLine[i];
                if (current == '\\') //if the current character is an escaped character
                    isEscaped = !isEscaped;
                else if (current == '"' && !isEscaped)
                {
                    if (dialogueStart == -1)
                        dialogueStart = i;
                    else if (dialogueEnd == -1)
                        dialogueEnd = i;
                }
                else
                    isEscaped = false;
            }

            //Indentify Command Pattern
            Regex commandRegex = new Regex(commandRegexPattern);
            Match match = commandRegex.Match(rawLine);
            int commandStart = -1;
            if (match.Success)
            {
                commandStart = match.Index;
                if (dialogueStart == -1 && dialogueEnd == -1)
                    return ("", "", rawLine.Trim());
            }

            //If we are here then we either have dialogue or a multi word argument in a command. Figure out if this is dialogue.
            if (dialogueStart != -1 && dialogueEnd != -1 && (commandStart == -1 || commandStart > dialogueEnd))
            {
                //We know that we have valid dialogue
                speaker = rawLine.Substring(0, dialogueStart).Trim();
                dialogue = rawLine.Substring(dialogueStart + 1, dialogueEnd - dialogueStart - 1).Replace("\\\"", "\"");
                if (commandStart != -1)
                    commands = rawLine.Substring(commandStart).Trim();
            }
            else if (commandStart != -1 && dialogueStart > commandStart)
                commands = rawLine;
            else
                speaker = rawLine;

            return (speaker, dialogue, commands);
        }
    }
}
