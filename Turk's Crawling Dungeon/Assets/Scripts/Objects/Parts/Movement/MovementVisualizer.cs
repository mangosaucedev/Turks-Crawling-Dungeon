using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class MovementVisualizer
    {
        private const float VISUALIZATION_SPEED = 24f;
        private const float VISUALIZATION_MAX_TIME = 0.5f;

        private Movement movement;
        private Coroutine moveCoroutine;
        private Coroutine cancelCoroutine;
        private WaitForSeconds cancelWait = new WaitForSeconds(VISUALIZATION_MAX_TIME);

        private Vector2Int MovementVector => movement.movementVector;

        public MovementVisualizer(Movement movement)
        {
            this.movement = movement;
        }

        public void StartVizualization(IEnumerator coroutine)
        {
            movement.EnsureCoroutineStopped(ref moveCoroutine);
            movement.EnsureCoroutineStopped(ref cancelCoroutine);
            moveCoroutine = movement.StartCoroutine(coroutine);
            cancelCoroutine = movement.StartCoroutine(Cancel());
        }

        public IEnumerator MoveVisualizationRoutine()
        {
            MoveSpriteToStartingPosition();
            yield return MoveSpriteToPositionRoutine(Vector3.zero);
            movement.EnsureCoroutineStopped(ref cancelCoroutine);
            movement.MoveSpriteToOrigin();
        }

        private IEnumerator Cancel()
        {
            yield return cancelWait;
            movement.EnsureCoroutineStopped(ref moveCoroutine);
        }

        private void MoveSpriteToStartingPosition()
        {
            Transform spriteTransform = movement.parent.SpriteRenderer.transform;
            Vector2Int offset = -MovementVector * Cell.SIZE;
            Vector3 startingPosition = new Vector3(offset.x, offset.y);
            spriteTransform.localPosition = startingPosition;
        }

        private IEnumerator MoveSpriteToPositionRoutine(Vector3 targetPosition)
        {
            Transform spriteTransform = movement.parent.SpriteRenderer.transform;

            while (spriteTransform.localPosition != targetPosition)
            {
                Vector3 currentPosition = spriteTransform.localPosition;
                Vector2 direction = (targetPosition - currentPosition).normalized;
                float distanceRemaining = Vector3.Distance(targetPosition, currentPosition);
                float moveDistance = Mathf.Min(distanceRemaining, VISUALIZATION_SPEED * Time.deltaTime);
                Vector3 moveVector = direction * moveDistance;
                spriteTransform.localPosition = currentPosition + moveVector;
                yield return null;
            }
        }

        public IEnumerator MoveFailedVizualizationRoutine()
        {
            Vector3 midPoint = GetMidpointToNewCell();
            yield return movement.StartCoroutine(MoveSpriteToPositionRoutine(midPoint));
            yield return movement.StartCoroutine(MoveSpriteToPositionRoutine(Vector3.zero));
        }

        private Vector3 GetMidpointToNewCell()
        {
            Vector2Int offset = MovementVector * Cell.SIZE / 2;
            Vector3 midpointPosition = new Vector3(offset.x, offset.y);
            return midpointPosition;
        }
    }
}
