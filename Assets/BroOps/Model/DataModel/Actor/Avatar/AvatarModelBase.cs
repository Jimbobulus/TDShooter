using UnityEngine;
using System.Collections;
using com.gamehound.broops.model.datamodel;


namespace com.gamehound.broops.model
{
	public class AvatarModelBase : ActorModelBase 
	{
		public static string POSITION_CHANGED = "the avatars transform has moved in the world";
		public static string SET_MARKER = "set a new position for the target marker";

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
				// set event here for moving the target marker to a new location
				ModelLocator.EventBus.DispatchGenericEvent( SET_MARKER, this, targetMarkerPosition );
			}
		}

		public AvatarModelBase()
		{

		}

		protected override void Configure() {
			base.Configure();
			//UnityEngine.Debug.Log(this+" activated");
		}
	}
}
