using BF;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.GameObject;

namespace Internet
{
	public class NetPoolManager : NetworkBehaviour
	{
        static NetPoolManager instance;
        public static NetPoolManager Instance()
        {
            return instance;
        }

        private void OnEnable()
        {
            instance = this;
        }

        [SerializeField] Pool[] netObjectPool;
        [SerializeField] Pool[] nonNetObjetPool;
        Dictionary<string, Pool> dictionary = new Dictionary<string, Pool>();

        protected void Awake()
        {
            int i = 0;

            for (i = 0; i < netObjectPool.Length; i++)
            {
                itemPool[i].Initialize(transform);
                dictionary.Add(itemPool[i].prefab.name, itemPool[i]);
            }
            for (i = 0; i < nonNetObjetPool.Length; i++)
            {
                nonNetObjetPool[i].Initialize(transform);
                dictionary.Add(nonNetObjetPool[i].prefab.name, nonNetObjetPool[i]);
            }
        }
        private void Start()
        {
            TransitManager.Instance().onSceneUnload += RecycleAll;
        }

        public GameObject ReleaseNetObj(string a)
        {
#if UNITY_EDITOR
            if (!dictionary.ContainsKey(a))
            {
                Debug.Log(string.Format("PoolManagerError,no such key:{0}", a));
            }
#endif 
            return dictionary[a].GetFromPool();
        }
        public GameObject ReleaseNonNetObj(string a)
        {
            return dictionary[a].GetFromPool();
        }
        public void RecycleNetObj(GameObject a)
        {
            dictionary[a.name].BackToPool(a);
        }
        public void RecycleNonNetObj(GameObject a)
        {
            dictionary[a.name].BackToPool(a);
        }

        public bool IsContain(string a)
        {
            return dictionary.ContainsKey(a);
        }
        void RecycleAll(Scene scene) => RecycleAll();
        public void RecycleAll()
        {
            var obj = FindObjectsByType<BaseObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            if (obj == null)
            {
                return;
            }
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].Close();
            }

        }
        private void OnDisable()
        {
            TransitManager.Instance().onSceneUnload -= RecycleAll;
            
        }

        #region NetObjPool
        [Serializable]
        class PoolNetObj
        {
            public GameObject prefab;
            public int size;
            Queue<GameObject> queue = new Queue<GameObject>();
            List<GameObject> list = new List<GameObject>();
            Transform transParent;
            void Create()
            {
                GameObject a = GameObject.Instantiate(prefab);
                a.GetComponent<NetworkObject>().Spawn();                
                a.name = prefab.name;
                queue.Enqueue(a);
            }
            public void Initialize(Transform parent)
            {
                transParent = parent;
                for (int i = 0; i < size; i++)
                {
                    Create();
                }
            }
            public GameObject GetFromPool()
            {
                GameObject a;
                if (queue.Count <= 0)
                {
                    Create();
                }
                a = queue.Dequeue();
                list.Add(a);
                return a;
            }
            public void BackToPool(GameObject a)
            {
                a.SetActive(false);
#if UNITY_EDITOR    
                if (queue.Contains(a))
                {
                    Debug.LogError(a.transform.position);
                    Debug.LogError(a.name);
                }
#endif
                list.Remove(a);
                queue.Enqueue(a);
            }
        }
        #endregion

        #region NonNetObjPool
        [Serializable]
        class PoolNonNetObj
        {
            public GameObject prefab;
        }
        #endregion

        
    }
}
