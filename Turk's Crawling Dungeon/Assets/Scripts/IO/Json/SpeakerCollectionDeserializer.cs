using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics.Dialogues;

namespace TCD.IO
{
    [AssetDeserializer]
    public class SpeakerCollectionDeserializer : JsonDeserializer<SpeakerCollection>
    {
        protected override string FullPath => Directories.Speakers;

        protected override string Extension => Extensions.Speaker;

        public override void AddAsset(string name, SpeakerCollection obj)
        {
            foreach (Speaker speaker in obj.speakers)
                Assets.Add(speaker.name, speaker);
        }
    }
}
