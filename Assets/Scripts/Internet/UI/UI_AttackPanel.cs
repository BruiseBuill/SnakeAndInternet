using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Internet.UI
{
	public class UI_AttackPanel : MonoBehaviour
	{
		[SerializeField] Button attackBtn;
		[SerializeField] EventChannel onAttack;

        private void Awake()
        {
            attackBtn.onClick.AddListener(() => onAttack.Invoke());
        }
        private void OnDisable()
        {
            attackBtn.onClick.RemoveAllListeners();
        }

    }
}