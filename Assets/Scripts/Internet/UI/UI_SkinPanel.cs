using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internet
{
	public class UI_SkinPanel : UIPanelObject
	{
        public int width = 200;
        public int interval = 100;

        [SerializeField] GenericEventChannel<object> onSelectSkin;
        [SerializeField] GameObject skinPrefab;
        [SerializeField] RectTransform contentTrans;

        protected override void Awake()
        {
            base.Awake();
            onSelectSkin.AddListener(SelectSkin);
        }
        protected override void Start()
        {
            base.Start();

            Generate();
        }
        void Generate()
        {
            contentTrans.sizeDelta = new Vector2(width * SkinManager.Instance().allSkinList.Count + interval * (SkinManager.Instance().allSkinList.Count + 1), contentTrans.sizeDelta.y);

            for (int i = 0; i < SkinManager.Instance().allSkinList.Count; i++)
            {
                var s = PoolManager.Instance().Release(skinPrefab.name).GetComponent<UI_Skin>();
                s.Initialize(SkinManager.Instance().allSkinList[i]);
                s.transform.parent = contentTrans;
                s.gameObject.SetActive(true);
            }


        }
        void SelectSkin(object ob)
        {
            Close();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            onSelectSkin.RemoveListener(SelectSkin);
        }

    }
}