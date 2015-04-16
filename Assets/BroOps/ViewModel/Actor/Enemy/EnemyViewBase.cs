using UnityEngine;
using System.Collections;
using System;
using com.gamehound.broops.model;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;

namespace com.gamehound.broops.viewmodel
{
	public class EnemyViewBase : ActorViewBase 
	{
		protected Vector3 targetPosition = Vector3.zero;
		protected Vector3 prevPosition = Vector3.zero;

		public override void Start()
		{
			base.Start();
			
		}

		protected override void HandleEventBus(GenericEvent genEvent)
		{
			if( genEvent.token == GameModel.ENEMY_ADDED )
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
