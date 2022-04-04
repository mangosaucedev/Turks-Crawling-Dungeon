using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.IO;

namespace TCD
{
    public class AssetLoadingOperation : ILoadingOperation
    {
        private IAssetLoader assetDeserializer;

        public string Name => "Asset Loading Operation";

        public string Message => "Loading assets...";

        public string Description => Message;

        public bool HasStarted { get; set; }

        public bool IsDone { get; set; }

        public float Progress
        {
            get
            {
                if (assetDeserializer == null)
                    return 0f;
                return assetDeserializer.Progress;
            }
        }

        public AssetLoadingOperation(IAssetLoader assetDeserializer)
        {
            this.assetDeserializer = assetDeserializer;
        }

        public IEnumerator Load()
        {
            yield return assetDeserializer.LoadAll();
            IsDone = true;
        }
    }
}
