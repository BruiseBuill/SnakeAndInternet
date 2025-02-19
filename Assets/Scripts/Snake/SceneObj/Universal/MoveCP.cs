using BF;
using BF.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class MoveCP : BaseComponent
    {
        protected AnimalDataCP dataCP;

        public float accelerate;
        public float maxMoveSpeed;
        public float rotateSpeed;
        public float minMoveSpeedWhenRotate;
        public float decelerateWhenRotate;
        public float decelerateWhenStop;

        public Vector3 orient;
        
        public Vector3 aimPos;
        public bool isCompleted;
        public float presentSpeed;

        protected override void Awake()
        {
            base.Awake();
            dataCP = (AnimalDataCP)data;
        }
        public override void Open()
        {
            dataCP.onMove += Move;
            dataCP.onStopMove += Stop;

            isCompleted = true;
        }
        public override void Close()
        {
            dataCP.onMove -= Move;
            dataCP.onStopMove -= Stop;
        }
        protected void Update()
        {
            if (!data.isAlive.Value)
            {
                return;
            }

            if (!isCompleted)
            {
                if ((aimPos - dataCP.model.position).sqrMagnitude < AnimalDataCP.minSqrDistance)
                {
                    Stop();
                    return;
                }

                if (Mathf.Acos(Vector3.Dot((aimPos - dataCP.model.position).normalized, orient)) * Mathf.Rad2Deg < Time.deltaTime * rotateSpeed)
                {
                    orient = (aimPos - dataCP.model.position).normalized;
                    Run();
                }
                else
                {
                    Rotate();
                }
            }
            dataCP.model.position += presentSpeed * orient * Time.deltaTime;
            dataCP.rotateModel.up = orient;
        }
        public void Move(Vector3 aimPos)
        {
            this.aimPos = aimPos;
            isCompleted = false;
            StopCoroutine("Stopping");
        }
        protected void Run()
        {
            presentSpeed = Mathf.Min(maxMoveSpeed, presentSpeed + Time.deltaTime * accelerate);
        }
        protected void Rotate()
        {

            var aimOrient= (aimPos - dataCP.model.position).normalized;
            var angle = Mathf.Acos(Vector3.Dot(orient,aimOrient));
            if (angle * Mathf.Rad2Deg < Time.deltaTime * rotateSpeed) 
            {
                orient = aimOrient;
            }
            else
            {
                bool direction = ((orient.y * aimOrient.x - orient.x * aimOrient.y) < 0);
                orient = orient.RotateVertical(Time.deltaTime * rotateSpeed * (direction ? 1 : -1)).normalized;
            }

            presentSpeed = Mathf.Max(presentSpeed - Time.deltaTime * decelerateWhenRotate, minMoveSpeedWhenRotate);
        }
        protected void Stop()
        {
            isCompleted = true;
            StartCoroutine("Stopping");
        }
        IEnumerator Stopping()
        {
            while (presentSpeed > 0)
            {
                yield return null;
                presentSpeed = Mathf.Max(0, presentSpeed - Time.deltaTime * decelerateWhenStop);
                //dataCP.rb.velocity = presentSpeed * orient;
                //dataCP.model.up = orient;
            }
        }

    }
}
