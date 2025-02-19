using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.ScriptableObj
{
    [CreateAssetMenu(fileName ="Buff",menuName ="Snake/Buff")]
 	public class Buff : ScriptableObject
	{
        public string name;
        public string description;
	}
}
