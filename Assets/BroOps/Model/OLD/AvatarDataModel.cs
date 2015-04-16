using System;
using com.gamehound.broops.model;
using UnityEngine;

namespace com.gamehound.broops.model.datamodel
{
	public enum AvatarType
	{
		Blue,
		Red
	}

	public enum AvatarState
	{
		idle,
		moving,
		aiming,
		firing
	}

	public class AvatarDataModel 
	{
		//public static string 
		public static string HEALTH_CHANGED = "the health of this avatar has changed";
		public static string SET_MARKER = "set a new position for the target marker";
		public static string POSITION_CHANGED = "the avatars transform has moved in the world";
		public static string STATE_CHANGED = "tha avatars state has changed";

		public AvatarDataModel( AvatarType avatarID )
		{
			this.AvatarID = avatarID;
		}
		public readonly AvatarType AvatarID;

		private AvatarState state = AvatarState.idle;
		public AvatarState State
		{
			get
			{
				return state;
			}
			set
			{
				state = value;
				ModelLocator.EventBus.DispatchGenericEvent( STATE_CHANGED, this, state );
			}
		}


		private int health = 100;
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


		private Vector3 targetMarkerPosition = Vector3.zero;
		public Vector3 TargetMarkerPosition
		{
			get
			{
				return targetMarkerPosition;
			}
			set
			{

				targetMarkerPosition = value;
				// set event here for moving to a new location
				ModelLocator.EventBus.DispatchGenericEvent( SET_MARKER, this, targetMarkerPosition );
			}
		}

		private Vector3 currentPosition = Vector3.zero;
		public Vector3 CurrentPosition
		{
			get
			{
				return currentPosition;
			}
			set
			{
				
				currentPosition = value;
				// set event here for moving to a new location
				ModelLocator.EventBus.DispatchGenericEvent( POSITION_CHANGED, this, currentPosition );
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
	}
}
