using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorizontalGame
{
	public class AxisRoll : MonoBehaviour
	{
		[Serializable]
		class ParalellObject
		{
			public Transform goTrans;
			public float multiple_X;
			public float multiple_Y;
		}

		[SerializeField] List<ParalellObject> paralellObjects;
		Vector3 cameraPos;
		new Camera camera;

        private void Awake()
        {
			camera = Camera.main;
			cameraPos = camera.transform.position;
        }
        private void LateUpdate()
        {
			var sub = camera.transform.position - cameraPos;
			foreach(var obj in paralellObjects)
			{
				obj.goTrans.position += new Vector3(sub.x * obj.multiple_X, sub.y * obj.multiple_Y, 0);
			}
            cameraPos = camera.transform.position;
        } 
    }
}