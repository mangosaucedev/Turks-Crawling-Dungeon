using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public class MainCursor : MonoBehaviour
    {
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
        }

        public void CenterOnPlayer()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            CenterOn(player);
        }

        public void CenterOn(BaseObject obj)
        {
            Vector3 position = obj.transform.position;
            MoveToPosition(position);
        }

        private void MoveToPosition(Vector3 position)
        {
            transform.position = position;
            int x = Mathf.FloorToInt(position.x / Cell.SIZE);
            int y = Mathf.FloorToInt(position.y / Cell.SIZE);
            EventManager.Send(new CursorMovedEvent(new Vector2Int(x, y)));
        }

        private void OnPlayerMoved(PlayerMovedEvent e)
        {
            CenterOnPlayer();
        }

        public bool Move(Vector2Int direction, bool forced = false)
        {
            Vector3 transformation = (Vector2) direction * Cell.SIZE;
            MoveToPosition(transform.position + transformation);
            return true;
        }

        public Vector2Int GetGridPosition()
        {
            int x = Mathf.FloorToInt(transform.position.x / Cell.SIZE);
            int y = Mathf.FloorToInt(transform.position.y / Cell.SIZE);
            return new Vector2Int(x, y);
        }
    }
}
