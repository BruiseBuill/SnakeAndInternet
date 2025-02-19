using BF;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Snake.Character
{
    public class SnakeAICP : BaseComponent
    {
        SnakeData snakeData;

        public Vector2 patrolLengthOffset;
        public Vector3 patrolPos;
        public float thinkBreak;
        public bool canThink=true;

        public float chaseBufferDistance = 1.5f;

        protected override void Awake()
        {
            base.Awake();
            snakeData = (SnakeData)data;
        }
        public override void Open()
        {
            if(snakeData.characterType== CharacterType.Player)
            {
                this.enabled = false;
            }
            snakeData.onTrack += Track;
            snakeData.onFindAim += FindAim;
            snakeData.onLoseAim += LoseAim;
        }
        public override void Close()
        {
            
        }
        private void Update()
        {
            if (!data.isAlive.Value)
            {
                return;
            }

            if (snakeData.state == SnakeData.SnakeState.Patrol)
            {
                if ((patrolPos - snakeData.model.position).sqrMagnitude < AnimalDataCP.minSqrDistance)
                {
                    CreatePatrolPoint();
                }
                snakeData.onMove.Invoke(patrolPos);
            }
            else
            {
                snakeData.onMove.Invoke(snakeData.lastAimPos);
            }
        }
        public void Track(Vector3 pos,Vector3 direction)
        {
            if (snakeData.state == SnakeData.SnakeState.Patrol) 
            {
                snakeData.lastAimPos = pos;
                patrolPos = snakeData.lastAimPos + direction * Random.Range(patrolLengthOffset.x, patrolLengthOffset.y);
                snakeData.aim = null;
            }
        }
        public void FindAim(Transform aim)
        {
            snakeData.aim = aim;
            if (canThink)
            {
                snakeData.state = SnakeData.SnakeState.Chase;
                snakeData.aim = aim;
                snakeData.lastAimPos = aim.position + (aim.position-snakeData.model.position).normalized * chaseBufferDistance;
                StartCoroutine("WaitingThinkBreak");
            }
        }
        IEnumerator WaitingThinkBreak()
        {
            canThink = false;
            yield return new WaitForSeconds(thinkBreak);
            canThink = true;
        }
        public void LoseAim()
        {
            snakeData.state = SnakeData.SnakeState.Patrol;
            snakeData.lastAimPos = snakeData.aim.position;

            patrolPos = snakeData.lastAimPos + snakeData.aim.up * Random.Range(patrolLengthOffset.x, patrolLengthOffset.y);
            snakeData.aim = null;
        }
        void CreatePatrolPoint()
        {
            do
            {
                patrolPos = snakeData.lastAimPos + (Vector3)Random.insideUnitCircle.normalized * Random.Range(patrolLengthOffset.x, patrolLengthOffset.y);
            }
            while (!MapManager.Instance().IsInMap(patrolPos));
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (1 << collision.collider.gameObject.layer == snakeData.wallLayer&&snakeData.state== SnakeData.SnakeState.Patrol)
            {
                CreatePatrolPoint();
            }
        }
    }
}
