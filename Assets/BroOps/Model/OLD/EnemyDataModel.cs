using System;
using com.gamehound.broops.model;
using UnityEngine;

namespace com.gamehound.broops.model.datamodel
{
	public class EnemyDataModel 
	{
		public static string HEALTH_CHANGED = "the health of this enemy has changed";


		private int health = 100;
		private int origHealhValue;
		public int Health
		{
			get
			{
				return health;
			}
			set
			{
				health = value;
				ModelLocator.EventBus.DispatchGenericEvent( HEALTH_CHANGED, this, health );
			}
		}


		private float speed = 5.0f;
		public float Speed
		{
			get
			{
				return speed;
			}
			set
			{
				speed = value;
			}
		}
		
		private float angularSpeed = 360.0f;
		public float AngularSpeed
		{
			get
			{
				return angularSpeed;
			}
			set
			{
				angularSpeed = value;
			}
		}
		
		
		private float acceleration = 8.0f;
		public float Acceleration
		{
			get
			{
				return acceleration;
			}
			set
			{
				acceleration = value;
			}
		}


		// need to put in a weapons class...
		private int damage = 20;
		public int Damage
		{
			get
			{
				return damage;
			}
			set
			{
				damage = value;
			}
		}
		
		// need to put in a weapons class...
		private float range = 10.0f;
		public float Range
		{
			get
			{
				return range;
			}
			set
			{
				range = value;
			}
		}
		
		// need to put in a weapons class...
		private float fireRate = 0.15f;
		public float FireRate
		{
			get
			{
				return fireRate;
			}
			set
			{
				fireRate = value;
			}
		}

		public EnemyDataModel()
		{
			origHealhValue = health;
		}

		private void Reset()
		{
			health = origHealhValue;
		}
	}
}
