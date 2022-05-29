using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;
using TCD.Objects.Parts;
using TCD.Inputs.Actions;
using TCD.TimeManagement;
using Resources = TCD.Objects.Parts.Resources;

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
            UpdateMovementTimer();
            UpdateFastMovementTimer();

            if (ServiceLocator.Get<PlayerActionManager>().currentAction == null)
            {
                UpdateAttackVectorPressed();
                if (movementInputVector != Vector2Int.zero)
                {
                    TryAttack();
                    return;
                }
                UpdateAttackVectorHeld();
                if (movementInputVector != Vector2Int.zero && movementTimer <= 0)
                {
                    TryAttack();
                    return;
                }
            }

            UpdateMovementVectorPressed();
            if (movementInputVector != Vector2Int.zero)
            {
                TryMove();
                return;
            }
            UpdateMovementVectorHeld();
            if (movementInputVector != Vector2Int.zero && movementTimer <= 0)
                TryMove();
        }

        private void UpdateAttackVectorPressed()
        {
            movementInputVector = Vector2Int.zero;

            if (Keys.GetCommand(KeyCommand.AttackNorth, KeyState.PressedThisFrame) ||
                Keys.GetCommand(KeyCommand.AttackNorthAlt, KeyState.PressedThisFrame))
                movementInputVector += new Vector2Int(0, 1);
            if (Keys.GetCommand(KeyCommand.AttackSouth, KeyState.PressedThisFrame) ||
                Keys.GetCommand(KeyCommand.AttackSouthAlt, KeyState.PressedThisFrame))
                movementInputVector += new Vector2Int(0, -1);
            if (Keys.GetCommand(KeyCommand.AttackWest, KeyState.PressedThisFrame) ||
                Keys.GetCommand(KeyCommand.AttackWestAlt, KeyState.PressedThisFrame))
                movementInputVector += new Vector2Int(-1, 0);
            if (Keys.GetCommand(KeyCommand.AttackEast, KeyState.PressedThisFrame) ||
                Keys.GetCommand(KeyCommand.AttackEastAlt, KeyState.PressedThisFrame))
                movementInputVector += new Vector2Int(1, 0);

            if (movementInputVector == Vector2Int.zero)
            {
                if (Keys.GetCommand(KeyCommand.AttackNorthwest, KeyState.PressedThisFrame))
                    movementInputVector = new Vector2Int(-1, 1);
                if (Keys.GetCommand(KeyCommand.AttackNortheast, KeyState.PressedThisFrame))
                    movementInputVector = Vector2Int.one;
                if (Keys.GetCommand(KeyCommand.AttackSouthwest, KeyState.PressedThisFrame))
                    movementInputVector = -Vector2Int.one;
                if (Keys.GetCommand(KeyCommand.AttackSoutheast, KeyState.PressedThisFrame))
                    movementInputVector = new Vector2Int(1, -1);
            }
        }

        private void UpdateAttackVectorHeld()
        {
            movementInputVector = Vector2Int.zero;

            if (Keys.GetCommand(KeyCommand.AttackNorth, KeyState.Pressed) ||
                Keys.GetCommand(KeyCommand.AttackNorthAlt, KeyState.Pressed))
                movementInputVector += new Vector2Int(0, 1);
            if (Keys.GetCommand(KeyCommand.AttackSouth, KeyState.Pressed) ||
                Keys.GetCommand(KeyCommand.AttackSouthAlt, KeyState.Pressed))
                movementInputVector += new Vector2Int(0, -1);
            if (Keys.GetCommand(KeyCommand.AttackWest, KeyState.Pressed) ||
                Keys.GetCommand(KeyCommand.AttackWestAlt, KeyState.Pressed))
                movementInputVector += new Vector2Int(-1, 0);
            if (Keys.GetCommand(KeyCommand.AttackEast, KeyState.Pressed) ||
                Keys.GetCommand(KeyCommand.AttackEastAlt, KeyState.Pressed))
                movementInputVector += new Vector2Int(1, 0);

            if (movementInputVector == Vector2Int.zero)
            {
                if (Keys.GetCommand(KeyCommand.AttackNorthwest, KeyState.Pressed))
                    movementInputVector = new Vector2Int(-1, 1);
                if (Keys.GetCommand(KeyCommand.AttackNortheast, KeyState.Pressed))
                    movementInputVector = Vector2Int.one;
                if (Keys.GetCommand(KeyCommand.AttackSouthwest, KeyState.Pressed))
                    movementInputVector = -Vector2Int.one;
                if (Keys.GetCommand(KeyCommand.AttackSoutheast, KeyState.Pressed))
                    movementInputVector = new Vector2Int(1, -1);
            }
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
                Movement movement = Player.Parts.Get<Movement>();
                return movement.TryToMove(direction, isForced);
            }
            return false;
        }

        private void TryAttack()
        {
            movementTimer = TIME_BETWEEN_MOVES;

            DebugLogger.Log($"Attacking in direction {movementInputVector}...");

            BaseObject player = PlayerInfo.currentPlayer;
            PlayerMovement playerMovement = player.Parts.Get<PlayerMovement>();
            GameGrid grid = CurrentZoneInfo.grid;
            Cell cell = grid[player.cell.Position + movementInputVector];
            if (TryGetObjectToAttack(cell, out BaseObject target) && player.Parts.TryGet(out Combat combat))
            {
                AttackHandler.AutoAttack(player, target);
                TimeScheduler.Tick(combat.GetAttackCost(target));
            }
        }

        private bool TryGetObjectToAttack(Cell cell, out BaseObject target)
        {
            target = null;
            foreach (BaseObject obj in cell.Objects)
            {
                Brain brain = obj.Parts.Get<Brain>();
                if (obj.Parts.Has(typeof(Resources)) 
                    && (!obj.Parts.TryGet(out FactionAllegiance factionAllegiance) || (factionAllegiance.FactionName != "Player")))
                {
                    target = obj;
                    return true;
                }
            }
            return false;
        }
    }
}
