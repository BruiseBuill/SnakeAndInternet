using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.UI
{
 	public class UI_LifeCount : UIPanelObject
	{
        [SerializeField] Sprite[] sprites;
        [SerializeField] List<Image> imageList;

        protected override void Start()
        {
            base.Start();
            GameControl.Instance().onGameStart += RefreshLife;
            GameControl.Instance().onLose += RefreshLife;
            GameControl.Instance().onLifeOver += RefreshLife;
#if UNITY_EDITOR
            if (imageList.Count < GameControl.Instance().maxRemainLife)
            {
                Debug.LogError(1);
            }
#endif
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            GameControl.Instance().onGameStart -= RefreshLife;
            GameControl.Instance().onLose -= RefreshLife;
            GameControl.Instance().onLifeOver -= RefreshLife;
        }
        void RefreshLife()
        {
            gameObject.SetActive(true);
            for(int i = 0; i < imageList.Count; i++)
            {
                imageList[i].sprite = GameControl.Instance().isPresentSnake ? sprites[0] : sprites[1];
                imageList[i].enabled = i < GameControl.Instance().remainLife;
            }
        }
    }
}
