using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internet.UI
{
	public class UI_Joystick : MonoBehaviour
	{
		[SerializeField] Joystick joy;
        [SerializeField] GenericEventChannel<Vector3> onMove;


        private void Awake()
        {
#if !UNITY_ANDROID
            gameObject.SetActive(false);    
#endif
        }    
        private void Update()
        {
            if (joy.Direction.sqrMagnitude > 0.1f)
            {
                onMove.Invoke(joy.Direction.normalized); 
            }
        }
    }
}