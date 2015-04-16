using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.gamehound.broops.model;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;

namespace com.gamehound.broops.viewmodel
{
	public class EnemyViewModel : BaseViewModel 
	{
		public NavMeshAgent agent;

		public DamageTriggerViewModel damageTrigger;

		private Transform mTransform;
		private Vector3 targetPosition = Vector3.zero;
		private Vector3 prevPosition = Vector3.zero;

		float dist = 0.0f;
		float newDist = 0.0f;

		public override void Start()
		{
			base.Start();

			mTransform = transform;
			damageTrigger.TriggerEnabled = true;

			// get model binding
			//damageTrigger.damage = ModelLocator.Game
		}

		protected override void HandleEventBus(GenericEvent genEvent)
		{
			if( genEvent.token == EnemyDataModel.HEALTH_CHANGED )
			{
				print("enemy health has changed");
			}

			if( genEvent.token == AvatarDataModel.POSITION_CHANGED )
			{
				targetPosition =(Vector3)genEvent.payload;

				if( agent.hasPath )
				{
					dist = Vector3.Distance( mTransform.position, prevPosition );
					newDist = Vector3.Distance( mTransform.position, targetPosition );

					if( newDist < dist )
						agent.SetDestination( targetPosition );
				}
				else
				{
					agent.SetDestination( targetPosition );
				}
				prevPosition = targetPosition;
			}

			if( genEvent.token == DamageTriggerViewModel.ON_DAMAGE_TRIGGER )
			{
				if( (DamageTriggerViewModel)genEvent.sender == damageTrigger )
				{
					//print("I have hit an avatar!!");
				}
			}
		}

		void Update()
		{

		}
	}
}