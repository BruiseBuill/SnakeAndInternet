using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace BF
{
	public abstract class BaseMomento
	{

	}

	public class MomentoManager : Single<MomentoManager>
	{
		[SerializeField] List<BaseMomento> momentList;
		[SerializeField] int maxMomentoCount;

		public void Save()
		{
			
		}
		public void Load()
		{
			
		}
	}
}