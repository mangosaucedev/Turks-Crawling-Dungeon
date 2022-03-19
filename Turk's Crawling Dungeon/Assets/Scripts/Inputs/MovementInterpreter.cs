using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Inputs.Actions;

namespace TCD.Inputs
{
    public class MovementInterpreter : IInterpreter
    {
        private const float TIME_BETWEEN_MOVES = 0.2f;
        private const float TIME_TO_FAST_MOVE = 1f;
        private const float FAST_MOVEMENT_MULTIPLIER = 2f;

        public static MovementMode movementMode;

        private delegate bool MovementDelegate(Vector2Int vector, bool isForced);
        private Vector2Int movementInputVector;
        private float movementTimer = TIME_BETWEEN_MOVES;
        private float fastMovementTimer = TIME_TO_FAST_MOVE;

        public InputGroup InputGroup => InputGroup.Gameplay;

        private BaseObject Player => PlayerInfo.currentPlayer;

        public void Update()
        {
            UpdateMovementVectorPressed();

            if (movementInputVector != Vector2Int.zero)
            {
                TryMove();
                return;
            }

            UpdateMovementVectorHeld();
            UpdateMovementTimer();
            UpdateFastMovementTimer();

            if (movementInputVector != Vector2Int.zero && movementTimer <= 0)
                TryMove();
        }

        private void UpdateMovementVectorPressed()
        {
            movementInputVector = Vector2Int.zero;

            if (Keys.GetCommand(KeyCommand.MoveNorth, KeyState.PressedThisFrame) ||
                Keys.GetCommand(KeyCommand.MoveNorthAlt, KeyState.PressedThisFrame))
                movementInputVector += new Vector2Int(0, 1);
            if (Keys.GetCommand(KeyCommand.MoveSouth, KeyState.PressedThisFrame) ||
                Keys.GetCommand(KeyCommand.MoveSouthAlt, KeyState.PressedThisFrame))
                movementInputVector += new Vector2Int(0, -1);
            if (Keys.GetCommand(KeyCommand.MoveWest, KeyState.PressedThisFrame) ||
                Keys.GetCommand(KeyCommand.MoveWestAlt, KeyState.PressedThisFrame))
                movementInputVector += new Vector2Int(-1, 0);
            if (Keys.GetCommand(KeyCommand.MoveEast, KeyState.PressedThisFrame) ||
                Keys.GetCommand(KeyCommand.MoveEastAlt, KeyState.PressedThisFrame))
                movementInputVector += new Vector2Int(1, 0);

            if (movementInputVector == Vector2Int.zero)
            {
                if (Keys.GetCommand(KeyCommand.MoveNorthwest, KeyState.PressedThisFrame))
                    movementInputVector = new Vector2Int(-1, 1);
                if (Keys.GetCommand(KeyCommand.MoveNortheast, KeyState.PressedThisFrame))
                    movementInputVector = Vector2Int.one;
                if (Keys.GetCommand(KeyCommand.MoveSouthwest, KeyState.PressedThisFrame))
                    movementInputVector = -Vector2Int.one;
                if (Keys.GetCommand(KeyCommand.MoveSoutheast, KeyState.PressedThisFrame))
                    movementInputVector = new Vector2Int(1, -1);
            }
        }

        private void TryMove()
        {
            MovementDelegate movementDelegate = TryGetMovementDelegate();
            movementTimer = TIME_BETWEEN_MOVES;
            movementDelegate?.Invoke(movementInputVector, false);
        }

        private void UpdateMovementVectorHeld()
        {
            movementInputVector = Vector2Int.zero;

            if (Keys.GetCommand(KeyCommand.MoveNorth, KeyState.Pressed) ||
                Keys.GetCommand(KeyCommand.MoveNorthAlt, KeyState.Pressed))
                movementInputVector += new Vector2Int(0, 1);
            if (Keys.GetCommand(KeyCommand.MoveSouth, KeyState.Pressed) ||
                Keys.GetCommand(KeyCommand.MoveSouthAlt, KeyState.Pressed))
                movementInputVector += new Vector2Int(0, -1);
            if (Keys.GetCommand(KeyCommand.MoveWest, KeyState.Pressed) ||
                Keys.GetCommand(KeyCommand.MoveWestAlt, KeyState.Pressed))
                movementInputVector += new Vector2Int(-1, 0);
            if (Keys.GetCommand(KeyCommand.MoveEast, KeyState.Pressed) ||
                Keys.GetCommand(KeyCommand.MoveEastAlt, KeyState.Pressed))
                movementInputVector += new Vector2Int(1, 0);

            if (movementInputVector == Vector2Int.zero)
            {
                if (Keys.GetCommand(KeyCommand.MoveNorthwest, KeyState.Pressed))
                    movementInputVector = new Vector2Int(-1, 1);
                if (Keys.GetCommand(KeyCommand.MoveNortheast, KeyState.Pressed))
                    movementInputVector = Vector2Int.one;
                if (Keys.GetCommand(KeyCommand.MoveSouthwest, KeyState.Pressed))
                    movementInputVector = -Vector2Int.one;
                if (Keys.GetCommand(KeyCommand.MoveSoutheast, KeyState.Pressed))
                    movementInputVector = new Vector2Int(1, -1);
            }
        }

        private void UpdateMovementTimer()
        {
            if (movementTimer > 0)
            {
                float speedMultiplier = fastMovementTimer < 0 ? 1f * FAST_MOVEMENT_MULTIPLIER : 1f;
                movementTimer -= Time.deltaTime * speedMultiplier;
            }
        }

        private void UpdateFastMovementTimer()
        {
            if (movementInputVector == Vector2Int.zero)
                fastMovementTimer = TIME_TO_FAST_MOVE;
            else
                fastMovementTimer -= Time.deltaTime;
        }

        private MovementDelegate TryGetMovementDelegate()
        {
            switch (movementMode)
            {
                case MovementMode.Point:
                    return PointAction;
                case MovementMode.Cursor:
                case MovementMode.CursorFast:
                    return MoveCursor;
                default:
                    return MovePlayer;
            }
        }

        private bool PointAction(Vector2Int direction, bool isForced = false)
        {
            Vector2Int pointPosition = Player.cell.Position + direction;
            GameGrid grid = CurrentZoneInfo.grid;
            if (!grid.IsWithinBounds(pointPosition))
                return false;
            Cell cell = grid[pointPosition];
            PlayerActionManager manager = ServiceLocator.Get<PlayerActionManager>();
            manager.OnCell(cell);
            return true;
        }

        private bool MoveCursor(Vector2Int direction, bool isForced = false)
        {
            MainCursor mainCursor = ServiceLocator.Get<MainCursor>();
            return mainCursor.Move(direction, isForced);
        }

        private bool MovePlayer(Vector2Int direction, bool isForced = false)
        {
            if (KeyEventManager.GetInputGroupEnabled(InputGroup.Gameplay))
            {
                Movement movement = Player.parts.Get<Movement>();
                return movement.TryToMove(direction, isForced);
            }
            return false;
        }
    }
}
