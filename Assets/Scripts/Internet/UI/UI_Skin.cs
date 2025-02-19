using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Internet
{
	public class UI_Skin : MonoBehaviour
	{
		[SerializeField] GenericEventChannel<object> onSelectSkin;
		[SerializeField] Sprite sprite;

        private void Awake()
        {
			GetComponentInChildren<Button>().onClick.AddListener(BeSelected);
        }
        public void Initialize(Sprite sprite)
		{
			this.sprite = sprite;
            GetComponentInChildren<Image>().sprite = sprite;
        }
		public void BeSelected()
		{
			onSelectSkin.Invoke(sprite);
		}
        private void OnDisable()
        {
            
        }
    }
}