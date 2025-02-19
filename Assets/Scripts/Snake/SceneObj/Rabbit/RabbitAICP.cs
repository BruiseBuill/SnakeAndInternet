using BF;
using BF.Utility;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class RabbitAICP : BaseComponent
    {
        RabbitData rabbitData;

        public float keepStateDuration;
        public float bufferDistance;

        public AnimalControl enemy;
        public Transform enemyTrans;

        public float patrolRadius;
        public Vector3 patrolPos;
        public float idlePossibility;

        public float[] detectGrassAngle = { -60, -30, 0, 30, 60 };
        WaitForSeconds wait_KeepState;

        [ReadOnly]
        public float presentRotateDistance;
        public Vector2 rotateDistanceRange;
        [ReadOnly]
        public float presentRotateAngle;
        public Vector2 rotateAngleRange;
        public bool rotateOrient;
        public float[] rotateFixedAngles = { -90, 90, 180, 0 };
        public bool isRotateCoolDown;
        public float rotateBreakTime = 2.5f;

        public float thinkBreak;
        public bool canThink = true;

        protected override void Awake()
        {
            base.Awake();
            wait_KeepState = new WaitForSeconds(keepStateDuration);
            rabbitData = (RabbitData)data;
        }
        public override void Open()
        {
            if (rabbitData.characterType == CharacterType.Player)
            {
                this.enabled = false;
            }

            rabbitData.onFindEnemy += FindEnemy;
            rabbitData.onLoseEnemy += LoseEnemy;

            CreateNewPatrolPos();
            rabbitData.state = RabbitData.RabbitState.Patrol;

            presentRotateDistance = Random.Range(rotateDistanceRange.x, rotateDistanceRange.y);
            presentRotateAngle = Random.Range(rotateAngleRange.x, rotateAngleRange.y);
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
            switch (rabbitData.state)
            {
                case RabbitData.RabbitState.Escape:
                    Escape();                    
                    break;
                case RabbitData.RabbitState.Idle:
                    Idle();
                    break;
                case RabbitData.RabbitState.Patrol:
                    Patrol();
                    break;
                case RabbitData.RabbitState.Rotate:
                    Rotate();
                    break;
            }
        }
        void Idle()
        {
            if (rabbitData.isInGrass)
            {
                rabbitData.onStopMove.Invoke();
            }
            else
            {
                GoToGrass();
            }
        }
        void Patrol()
        {
            if((rabbitData.model.position - patrolPos).magnitude < AnimalDataCP.minSqrDistance)
            {
                if (Random.Range(0, 1f) < idlePossibility)
                {
                    rabbitData.state = RabbitData.RabbitState.Idle;
                }
                else
                {
                    CreateNewPatrolPos();
                }
            }

        }
        void CreateNewPatrolPos()
        {
            patrolPos = rabbitData.model.position + new Vector3(Random.Range(0, patrolRadius), Random.Range(0, patrolRadius));
            while (true)
            {
                if (MapManager.Instance().IsInMap(patrolPos))
                {
                    break;
                }
                patrolPos = rabbitData.model.position + new Vector3(Random.Range(0, patrolRadius), Random.Range(0, patrolRadius));
            }
                
        }
        void Rotate()
        {
            if (rabbitData.state == RabbitData.RabbitState.Rotate || isRotateCoolDown)
            {
                return;
            }
            rabbitData.state = RabbitData.RabbitState.Rotate;

            presentRotateDistance = Random.Range(rotateDistanceRange.x, rotateDistanceRange.y);
            presentRotateAngle = Random.Range(rotateAngleRange.x, rotateAngleRange.y);

            rotateOrient = !(rabbitData.model.position - enemyTrans.position).RotateOrientVertical(enemyTrans.up);
            StartCoroutine("Rotating");
        }
        IEnumerator Rotating() 
        {
            float totalTime = (presentRotateAngle - Vector3.SignedAngle(enemyTrans.up, rabbitData.rotateModel.up, Vector3.forward) * (rotateOrient ? 1 : -1)) / rabbitData.rotateSpeed_Var.FullValue;
            while (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                if (rotateOrient)
                {
                    MoveFixed(rabbitData.model.position-rabbitData.rotateModel.right*bufferDistance);
                }
                else
                {
                    MoveFixed(rabbitData.model.position + rabbitData.rotateModel.right * bufferDistance);
                }
                yield return null;
            }
            rabbitData.state = RabbitData.RabbitState.Escape;
            yield return StartCoroutine("WaitRotataCoolDown");
        }
        IEnumerator WaitRotataCoolDown()
        {
            isRotateCoolDown = true;
            yield return new WaitForSeconds(rotateBreakTime);
            isRotateCoolDown = false; 
        }
        void Escape()
        {
            if (enemy && (enemyTrans.position - rabbitData.model.position).sqrMagnitude < presentRotateDistance * presentRotateDistance && !isRotateCoolDown) 
            {
                Rotate();
                return;
            }
            RunLinear();
        }
        void GoToGrass()
        {
            int index = -1;
            float minDis = 999999f;
            Vector3[] detectDir = new Vector3[detectGrassAngle.Length];

            for (int i = 0; i < detectGrassAngle.Length; i++)
            {
                detectDir[i] = rabbitData.rotateModel.up.RotateVertical(detectGrassAngle[i]);
                var ray = Physics2D.Raycast(rabbitData.model.position, detectDir[i], rabbitData.normalVisionLength, rabbitData.grassLayer);
                if (ray.collider != null && ray.distance < minDis)
                {
                    minDis = ray.distance;
                    index = i;
                }
            }
            if (index < 0)
            {
                RunLinear();
            }
            else
            {
                MoveFixed(rabbitData.model.position + detectDir[index] * bufferDistance);
            }

        }
        void FindEnemy(AnimalControl enemy)
        {
            if (this.enemy == null) 
            {
                StopCoroutine("KeepState");
                rabbitData.state = RabbitData.RabbitState.Escape;
                this.enemy = enemy;
                enemyTrans = enemy.Model;
            }
        }
        void LoseEnemy()
        {
            if (enemy == null)
            {
                return;
            }
            StartCoroutine("KeepState");
            enemy = null;
        }
        IEnumerator KeepState()
        {
            yield return wait_KeepState;
            StopCoroutine("Rotating");
            CreateNewPatrolPos();
            rabbitData.state = RabbitData.RabbitState.Patrol;
        }
        void RunLinear()
        {
            if (enemy && canThink) 
            {
                MoveFixed(rabbitData.model.position + bufferDistance * (rabbitData.model.position - enemyTrans.position).normalized);
                StartCoroutine("WaitingThinkBreak");
            }
            else
            {
                MoveFixed(rabbitData.model.position + bufferDistance * (rabbitData.rotateModel.up).normalized);
            }
        }
        IEnumerator WaitingThinkBreak()
        {
            canThink = false;
            yield return new WaitForSeconds(thinkBreak);
            canThink = true;
        }
        void MoveFixed(Vector3 pos)
        {
            if (MapManager.Instance().IsInMap(pos))
            {
                rabbitData.onMove.Invoke(pos);
            }
            else
            {
                Vector3[] fixedDirection = new Vector3[rotateFixedAngles.Length];
                for(int i = 0; i < fixedDirection.Length; i++)
                {
                    fixedDirection[i] = rabbitData.rotateModel.up.RotateVertical(rotateFixedAngles[i]);
                    if (MapManager.Instance().IsInMap(rabbitData.model.position + fixedDirection[i] * bufferDistance))
                    {
                        rabbitData.onMove.Invoke(rabbitData.model.position + fixedDirection[i] * bufferDistance);
                        return;
                    }
                }
            }
        }
    }
}
