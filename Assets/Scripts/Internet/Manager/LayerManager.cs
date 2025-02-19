using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internet
{
	public class LayerManager : Single<LayerManager>
	{
		[SerializeField] int firstLayer;
		[SerializeField] LayerMask figherLayer;
		[SerializeField] LayerMask foodLayer;
		int playerCount;

        private void Awake()
        {
			playerCount = 0;
        }
        public LayerMask GetLeagurLayer()
		{
			playerCount++;
			return firstLayer + playerCount - 1;
			
		}
		public LayerMask GetEnemyLayer(LayerMask layer)
		{
			return ((1 << layer) ^ (figherLayer) + foodLayer);
		}
	}
}