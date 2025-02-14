using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internet
{
	public class SkinManager : Single<SkinManager>
	{
		[SerializeField] GenericEventChannel<object> onSkinSelected;
		public List<Sprite> allSkinList;
		int playerSelectedSkinIndex;

        private void Awake()
        {
			onSkinSelected.AddListener(SelectSkin);
        }
        public List<Sprite> GetSkinList(int count)
		{
			List<Sprite> list = new List<Sprite>();
			list.Add(allSkinList[playerSelectedSkinIndex]);
			for (int i = 0; i < count - 1; i++) 
			{
				if (i == playerSelectedSkinIndex)
				{
					list.Add(allSkinList[count]);
					continue;
				}
				list.Add(allSkinList[i]);
			}
			return list;
		}
		void SelectSkin(object obj)
		{
			playerSelectedSkinIndex = allSkinList.IndexOf((Sprite)obj);
			Generator.Instance().StartGame();
		}
        private void OnDestroy()
        {
            onSkinSelected.RemoveListener(SelectSkin);
        }
    }
}