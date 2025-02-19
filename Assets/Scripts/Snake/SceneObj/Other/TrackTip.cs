using BF;
using BF.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Other
{
 	public class TrackTip : SimpleObject
	{
        public float Priority => renderer.color.a;
        public Vector3 Pos => transform.position;
        public Vector3 Orient => transform.up;
        SpriteRenderer renderer;

        protected override void Awake()
        {
            base.Awake();
            renderer = GetComponent<SpriteRenderer>();
        }
        void SetLifeTime(float time)
        {
            lifeTime= time;
            wait_LifeTime = new WaitForSeconds(time);
        }
        public void Open(Vector3 pos, Vector3 orient,float time)
        {
            SetLifeTime(time);
            base.Open(pos, orient);
            StartCoroutine("ChangingColor");

            gameObject.SetActive(true);
        }
        IEnumerator ChangingColor()
        {
            float start = 0;
            while (start < lifeTime) 
            {
                start += Time.deltaTime;

                renderer.color = renderer.color.Alpha(1 - start / lifeTime);

                yield return null;
            }
        }
        public override void Close()
        {
            base.Close();
        }
    }
}
