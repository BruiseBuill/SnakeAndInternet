using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Other
{
 	public class TimeGR : MonoBehaviour
	{
        [SerializeField] float timeScale;
        
        [ContextMenu("Run")]
        void Run()
        {
            Time.timeScale = timeScale;
        }
	}
}
