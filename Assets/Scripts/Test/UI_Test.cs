using BF.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Test
{
 	public class UI_Test : UIPanelObject
	{
        [SerializeField] TextMeshProUGUI nameText;
        protected override void Start()
        {
            base.Start();
            nameText.DoText("12345678", 12);
        }
        [ContextMenu("Play")]
        void Play()
        {
            nameText.DoText("12345678", 12).Play();
        }
    }
}
