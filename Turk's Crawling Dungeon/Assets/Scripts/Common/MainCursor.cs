using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public class MainCursor : MonoBehaviour
    {
        private CursorState state;

        private void OnEnable()
        {
            EventManager.Listen<ZoneGenerationFinishedEvent>(this, OnZoneGenerationFinished);
            EventManager.Listen<PlayerMovedEvent>(this, OnPlayerMoved);
        }

        private void OnDisable()
        {
            EventManager.StopListening<ZoneGenerationFinishedEvent>(this);
            EventManager.StopListening<PlayerMovedEvent>(this);
        }

        private void OnZoneGenerationFinished(ZoneGenerationFinishedEvent e)
        {
            CenterOnPlayer();
            ChangeState(CursorState.Locked);
        }

        private void ChangeState(CursorState newState)
        {
            state = newState;
            EventManager.Send(new CursorStateChangeEvent(newState));
        }

        private void CenterOnPlayer()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Vector3 position = player.transform.position;
            MoveToPosition(position);
        }

        private void MoveToPosition(Vector3 position)
        {
            transform.position = position;
            EventManager.Send(new CursorMovedEvent());
        }

        private void OnPlayerMoved(PlayerMovedEvent e)
        {
            if (state != CursorState.Disabled && state != CursorState.Unlocked)
                CenterOnPlayer();
        }
    }
}
