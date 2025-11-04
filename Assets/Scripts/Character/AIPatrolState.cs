using UnityEngine;

namespace RPG.Character
{
    public class AIPatrolState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.patrolCmp.ResetTimers();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer < enemy.chaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            Vector3 oldPostion = enemy.patrolCmp.GetNextPosition();

            enemy.patrolCmp.CalculateNextPosition();

            Vector3 currentPosition = enemy.transform.position;
            Vector3 newPostion = enemy.patrolCmp.GetNextPosition();
            Vector3 offset = newPostion - currentPosition;

            enemy.movementCmp.MoveAgentByOffset(offset);

            Vector3 furtherOutNewPostion = enemy.patrolCmp.GetFartherOutPosition();
            Vector3 newForwardVector = furtherOutNewPostion - currentPosition;
            newForwardVector.y = 0;
            enemy.movementCmp.Rotate(newForwardVector);

            if (oldPostion == newPostion)
            {
                enemy.movementCmp.isMoving = false;
            }
        }
    }
}
