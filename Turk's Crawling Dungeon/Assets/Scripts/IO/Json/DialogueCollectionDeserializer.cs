using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics.Dialogues;

namespace TCD.IO
{
    [AssetLoader]
    public class DialogueCollectionDeserializer : JsonDeserializer<DialogueCollection>
    {
        protected override string FullPath => Directories.Dialogues;

        protected override string Extension => Extensions.Dialogue;

        public override void AddAsset(string name, DialogueCollection obj)
        {
            foreach (Dialogue dialogue in obj.dialogue)
                Assets.Add(dialogue.name, dialogue);
        }
    }
}
