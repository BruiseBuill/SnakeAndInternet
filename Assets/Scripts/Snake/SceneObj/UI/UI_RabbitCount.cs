using BF;
using BF.UI;
using BF.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

namespace Snake.UI
{
 	public class UI_RabbitCount : UIPanelObject
	{
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] GenericEventChannel<int> onRabbitCountChange;

        protected override void Start()
        {
            base.Start();
            onRabbitCountChange.AddListener(OnRabbitCountChange);
            GameControl.Instance().onGameStart += Open;
        }
        
        public override void Open()
        {
            base.Open();
            if (GameControl.Instance().isPresentSnake)
            {
                OnRabbitCountChange(GameControl.Instance().turnCount / 2 + 1);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            onRabbitCountChange.RemoveListener(OnRabbitCountChange);
            GameControl.Instance().onGameStart -= Open;
        }
        void OnRabbitCountChange(int count)
        {
            text.text = string.Format("Ê£Óà {0} Ö»ÍÃ×Ó", count); 
        }

    }
}
