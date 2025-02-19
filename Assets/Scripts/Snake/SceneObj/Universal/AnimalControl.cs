using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Snake.Character
{
 	public abstract class AnimalControl : BaseControl
	{
        public Transform Model => animalData.model;
        public Rigidbody2D rb => animalData.rb;

        public UnityAction onBeKill => animalData.onBeKill;

        protected AnimalDataCP animalData;

        protected override void Awake()
        {
            base.Awake();
            animalData = (AnimalDataCP)data;
        }

    }
}
