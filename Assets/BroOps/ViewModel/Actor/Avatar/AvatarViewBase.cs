using UnityEngine;
using System.Collections;
using System;
using com.gamehound.broops.model;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;

namespace com.gamehound.broops.viewmodel
{
	public class AvatarViewBase : ActorViewBase 
	{
		
		public Vector3 markerPosition = Vector3.zero;

		public override void Start()
		{
			base.Start();

		}

		protected override void HandleEventBus(GenericEvent genEvent)
		{
		}

		protected override void OnConfigure()
		{
			base.OnConfigure();
			//print ("Configured: "+modelBinding);
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			//ModelLocator.Game.RemoveAvatar( ModelLocator.Game.GetAvatarModel( modelBinding ));
		}
	}
}