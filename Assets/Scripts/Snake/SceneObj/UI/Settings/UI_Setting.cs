using System.Collections;
using System.Collections.Generic;
using BF;
using BF.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.UI
{
 	public class UI_Setting : UIPanelObject
	{
        public UI_Pause pauseObj;
        public UI_Win winObj;
        public UI_Lose loseObj;
        public UI_LifeOver lifeOverObj;

        public FullButton settingBtn;

        public UIPanelObject timeLine;

        [SerializeField] FullButton exitBtn;

        protected override void Awake()
        {
            base.Awake();
            settingBtn.onClick.AddListener(Open);
            settingBtn.onClick.AddListener(pauseObj.Open);
            settingBtn.gameObject.SetActive(false);
        }
        protected override void Start()
        {
            base.Start();
            GameControl.Instance().onGameStart += OpenSettingBtn;
            GameControl.Instance().onWin += Open;
            GameControl.Instance().onWin += winObj.Open;

            GameControl.Instance().onLose += Open;
            GameControl.Instance().onLose += loseObj.Open;

            GameControl.Instance().onLifeOver += Open;
            GameControl.Instance().onLifeOver += lifeOverObj.Open;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            GameControl.Instance().onGameStart -= OpenSettingBtn;
            GameControl.Instance().onWin -= Open;
            GameControl.Instance().onWin -= winObj.Open;

            GameControl.Instance().onLose -= Open;
            GameControl.Instance().onLose -= loseObj.Open;

            GameControl.Instance().onLifeOver -= Open;
            GameControl.Instance().onLifeOver -= lifeOverObj.Open;
        }
        void OpenSettingBtn()=> settingBtn.gameObject.SetActive(true);
        void CloseSettingBtn()=> settingBtn.gameObject.SetActive(false);
        public override void Open()
        {
            base.Open();
            CursorManager.Instance().canInput = false;
            timeLine.Open();

            exitBtn.onClick.AddListener(GameManager.ExitGame);
        }
        public override void Close()
        {
            base.Close();
            CursorManager.Instance().canInput = true;

            exitBtn.onClick.RemoveListener(GameManager.ExitGame);
        }
    }
}
