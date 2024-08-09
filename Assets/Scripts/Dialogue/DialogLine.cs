using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogLine 
    {
       public string speaker;
       public string dialogue;
       public string commands;

       public DialogLine(string speaker, string dialogue, string commands)
       {
         this.speaker = speaker;
         this.dialogue = dialogue;
         this.commands = commands;
       }
    }
}
