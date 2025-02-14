using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Other
{
 	public class VFXGR : MonoBehaviour
	{
        [SerializeField] GameObject clickEffectPrefab;
        [SerializeField] float interval;
        float start;

        private void Start()
        {
            CursorManager.Instance().onMove += Create;
            start = 0;
        }
        void Create(Vector3 pos)
        {
            if (Time.time - start < interval)
            {
                return;
            }
            start = Time.time;
            var vfx = PoolManager.Instance().Release(clickEffectPrefab.name);
            vfx.GetComponent<SimpleObject>().Open(pos);
        }
        private void OnDisable()
        {
            //CursorManager.Instance().onMove -= Create;
        }
    }
}
