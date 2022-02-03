using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class GameResetOnDestroy : Part
    {
        public override string Name => "Game Reset On Destroy";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == BeforeDestroyObjectEvent.id)
            {
                StopAllCoroutines();
                StartCoroutine(OnBeforeDestroyObjectRoutine());
            }
            return base.HandleEvent(e);
        }

        private IEnumerator OnBeforeDestroyObjectRoutine()
        {
            yield return ViewManager.OpenAndWaitForViewRoutine("Player Death View");
            TCDGame.StartNewGame();
        }
    }
}
