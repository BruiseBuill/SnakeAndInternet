using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class DebugCP : MonoBehaviour
    {
        [SerializeField] float radius;
        [SerializeField] bool isDebug;

        private void OnDrawGizmos()
        {
            if (isDebug)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, radius);
            }
                
            
        }
    }
}
