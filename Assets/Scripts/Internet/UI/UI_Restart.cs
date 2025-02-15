using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Internet.UI
{
 	public class UI_Restart : MonoBehaviour
	{
        [SerializeField] Button restartBtn;

        private void Awake()
        {
            restartBtn.onClick.AddListener(() => TransitManager.Instance().TransitScene("Internet"));
        }
    }
}
