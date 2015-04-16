﻿//using UnityEngine;
using System.Collections;
using System;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;

namespace com.gamehound.broops.model
{
	public class ActorModelBase
	{
		public static string HEALTH_CHANGED = "This actors health has changed";
		public static string INVINCIBLE = "This actor has toggled invincibility";
        public static string ENABLED = "This actor has toggled enabled";
		
		private int health = 1;
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
		private int _health;

		private bool isInvincible = false;
		public bool IsInvincible
		{
			get
			{
				return isInvincible;
			}
			set
			{
				isInvincible = value;
				ModelLocator.EventBus.DispatchGenericEvent( INVINCIBLE, this, isInvincible );
			}
		}


        private bool isEnabled = false;
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                ModelLocator.EventBus.DispatchGenericEvent(ENABLED, this, isEnabled);
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

		private float angularSpeed = 750.0f;
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


		public readonly Guid modelID;

		public ActorModelBase()
		{
			modelID = Guid.NewGuid();

			//UnityEngine.Debug.Log( "Create new AvatarModel: "+modelID.ToString());
		}

		protected virtual void Configure() {
			//UnityEngine.Debug.Log(this+" configured");
		}
	}
}
