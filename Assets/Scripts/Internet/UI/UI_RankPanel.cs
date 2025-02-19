using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Internet
{
	public class UI_RankPanel : UIPanelObject
	{
        [SerializeField] EventChannel onAnyUnitChange;
		[SerializeField] GenericEventChannel<object> onFlockGenerate;

        [SerializeField] GameObject rankPrefab;
		[SerializeField] Transform rankPanelTransform;
		[SerializeField] List<FlockControl> flockList;
		[SerializeField] List<TextMeshProUGUI> textList;

        protected override void Awake()
        {
            base.Awake();
			onAnyUnitChange.AddListener(Refresh);
			onFlockGenerate.AddListener(Generate);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            onAnyUnitChange.RemoveListener(Refresh);
			onFlockGenerate.RemoveListener(Generate);
        }
        public void Generate(object f)
		{
			var flock = f as FlockControl;
			var rank = PoolManager.Instance().Release(rankPrefab.name);
			rank.transform.parent = rankPanelTransform;

			rank.GetComponentInChildren<Image>().sprite = flock.skin;
			rank.SetActive(true);
			flockList.Add(flock);
			textList.Add(rank.GetComponentInChildren<TextMeshProUGUI>());
		}
		public void Refresh()
		{
			for (int i = 0; i < flockList.Count; i++)
			{
				textList[i].text = flockList[i].flockList.Count.ToString();
			}
		}
	}
}