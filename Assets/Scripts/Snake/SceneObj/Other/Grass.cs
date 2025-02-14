using BF;
using BF.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Other
{
 	public class Grass : SimpleObject
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] float minAlpha;
        [SerializeField] float transitTime;
        [SerializeField] bool isInVision;
        [SerializeField] bool isComplete;

        protected override void Awake()
        {
            base.Awake(); 
        }

        public void OnEnable()
        {
            spriteRenderer.color = spriteRenderer.color.Alpha(1);
        }
        public void DecreaseAlpha()
        {
            isInVision = true;
            isComplete = false;
        }
        private void Update()
        {
            if (isComplete)
            {
                return;
            }
            if (isInVision)
            {
                float a = spriteRenderer.color.a - Time.deltaTime / transitTime * (1 - minAlpha);
                if (a < minAlpha)
                {
                    a = minAlpha;
                    isComplete= true;
                }
                spriteRenderer.color = spriteRenderer.color.Alpha(a);
                
            }
            else
            {
                float a = spriteRenderer.color.a + Time.deltaTime / transitTime * (1 - minAlpha);
                if (a >1)
                {
                    a = 1;
                    isComplete = true;
                }
                spriteRenderer.color = spriteRenderer.color.Alpha(a);
            }
        }
        public void IncreaseAlpha()
        {
            isInVision = false;
            isComplete = false;
        }
    }
}
