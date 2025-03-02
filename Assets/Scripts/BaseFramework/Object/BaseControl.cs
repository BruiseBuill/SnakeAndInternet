using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF
{
	public interface ControlInitialization { }

	public abstract class BaseControl : BaseObject
	{
		protected BaseShareData data;
		public bool IsAlive
		{
			protected set => data.isAlive.Value = value;
            get => data.isAlive.Value;
		}
		protected virtual void Awake()
		{
			data = GetComponentInChildren<BaseShareData>();
		}
		public abstract void Initialize<T>(T para) where T : ControlInitialization;
		public abstract void Open(); 
	}
}