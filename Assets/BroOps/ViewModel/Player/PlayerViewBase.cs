using UnityEngine;
using System;
using System.Collections;
using com.gamehound.broops.model.eventbus;
using com.gamehound.broops.model;

namespace com.gamehound.broops.viewmodel
{
	public class PlayerViewBase : BaseViewModel 
	{

		public Guid modelBinding;

		public override void Start()
		{
			base.Start();
			ModelLocator.Game.AddPlayer( new OfflinePlayerTest() );
		}

		protected override void HandleEventBus(GenericEvent genEvent)
		{
			if( genEvent.token == GameModel.PLAYER_ADDED )
			{
				if( BindingCheck( modelBinding, genEvent.payload ))
				{
					modelBinding =(Guid) genEvent.payload;
					base.OnConfigure();
				}
			}
		}
	}
}
