using BF;
using BF.UI;
using BF.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.UI
{
 	public class UI_Menu : UIPanelObject
	{
        public FullButton enterBtn;
        public FullButton extBtn;
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] float timePerCharacter;
        [SerializeField] string title;

        protected override void Start()
        {
            base.Start();
            if (!GameControl.Instance().isInitialOpen)
            {
                enterBtn.onClick.AddListener(GameControl.Instance().EnterGame);
                enterBtn.onClick.AddListener(Close);
                extBtn.onClick.AddListener(GameManager.ExitGame);
                titleText.DoText(title, timePerCharacter * title.Length);
            }
            else
            {
                Close();
                GameControl.Instance().EnterGame();
            }
        }
    }
}
