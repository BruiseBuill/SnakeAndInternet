using BF.Utility;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace BF
{
	public class BaseShareData : MonoBehaviour
	{
        public DataWithEvent<bool> isAlive;
		public int _IdentityCode;
        public Sequece<BaseComponent> componentList;

        bool isInitialized;

        public virtual void Awake()
		{
            isInitialized = true;
            componentList = new Sequece<BaseComponent>();
		}
		public virtual void Open()
		{
			isAlive.data = true;
			_IdentityCode++;

            for (int i = 0; i < componentList.Count; i++)
            {
                componentList[i].Open();
                Debug.Log(2);
            }
            Debug.Log(1);
        }
		public virtual void Close()
		{
			isAlive.data = false;

            for (int i = 0; i < componentList.Count; i++)
            {
                componentList[i].Close();
            }
        }
        public void Register(BaseComponent component, int priority)
        {
            if (!isInitialized)
            {
                Awake();
            }
            componentList.Add(component, priority);
            Debug.Log(componentList.Count);
        }
        public void Unregister(BaseComponent component)
        {
            componentList.Remove(component);
        }
        [Serializable]
		public class DataWithEvent<T>
		{
			public UnityAction<T> onValueChange = delegate { };
			public UnityAction<T, T> onValueChange2Value = delegate { };
            //Sometimes onValueChange.Invoke(data) will change value again, then you should instead change data
            public T data;
			public virtual T Value 
            {
				get => data;
				set
				{
					onValueChange2Value.Invoke(data, value);
                    data = value;
					onValueChange.Invoke(data);
				}
			}
			public DataWithEvent()
			{
				onValueChange = delegate { };
			}
        }
		[Serializable]
		public class DataWithEventHop: DataWithEvent<bool> 
		{
            public override bool Value 
			{ 
				get => base.Value;
				set 
				{
					if (value != data) 
					{
                        data = value;
                        onValueChange.Invoke(data);
                    }
				}

			}
		}
		[Serializable]
		public abstract class DataWithVariableValue<T> : DataWithEvent<T> 
		{
			[ReadOnly]
			public T additive;
			public virtual T FullValue
			{
				get;
			}
            
            public abstract void ChangeAdditive(T additive);
        }
		[Serializable]
        public class DataWithVariableFloat : DataWithVariableValue<float>
		{
            public override float FullValue 
			{
				get => data + additive;
			}
            public override float Value
            {
                get => base.Value;
                set
                {
                    onValueChange2Value.Invoke(FullValue,additive+ value); 
                    data = value;
                    onValueChange.Invoke(FullValue);
                }
            }
            public override void ChangeAdditive(float additive)
            {
				onValueChange2Value.Invoke(FullValue, FullValue + additive);
				this.additive += additive;
				onValueChange.Invoke(FullValue);
            }
        }
        [Serializable]
        public class DataWithVariableInt : DataWithVariableValue<int>
        {
            public override int FullValue
            {
                get => data + additive;
            }
            public override int Value
            {
                get => base.Value;
                set
                {
                    onValueChange2Value.Invoke(FullValue, additive + value);
                    data = value;
                    onValueChange.Invoke(FullValue);
                }
            }
            public override void ChangeAdditive(int additive)
            {
                onValueChange2Value.Invoke(FullValue, FullValue + additive);
                this.additive += additive;
                onValueChange.Invoke(FullValue);
            }
        }
    }
}