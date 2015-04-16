using UnityEngine;
using System.Collections;
using com.gamehound.broops.model;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;


namespace com.gamehound.broops.viewmodel
{
	[RequireComponent(typeof(HealthViewModel))]
	public class AvatarViewModel : BaseViewModel 
	{
		public Transform targetMarker;
		public Player player = Player.one;
		public AvatarType avatarType = AvatarType.Blue;

		//public DamageTriggerViewModel damageTrigger;

		public Renderer renderer;

		public NavMeshAgent agent;

		private bool checkedPath = false;

		public override void Start()
		{
			base.Start();
			agent.speed = ModelLocator.Game.GetAvatarModel( avatarType ).Speed;
			agent.acceleration = ModelLocator.Game.GetAvatarModel( avatarType ).Acceleration;
			agent.angularSpeed = ModelLocator.Game.GetAvatarModel( avatarType ).AngularSpeed;
			
		}

		protected override void HandleEventBus(GenericEvent genEvent)
		{
			if( genEvent.token == AvatarDataModel.SET_MARKER )
			{
				if( IsSelectedByPlayer() )
				{
					targetMarker.position =(Vector3)genEvent.payload;
					agent.SetDestination( targetMarker.position );
					ModelLocator.Game.GetAvatarModel( avatarType ).State = AvatarState.moving;
					checkedPath = false;
				}
			}
		}

		public void OnDamage( int damage )
		{
			ModelLocator.Game.GetAvatarModel( avatarType ).Health -= damage;
		}

		bool IsSelectedByPlayer()
		{
			return ModelLocator.Game.GetPlayerModel( player ).SelectedAvatar == avatarType;
		}

		bool IsBoundToThisModel( AvatarDataModel avatarModel )
		{
			return avatarModel.AvatarID == avatarType;
		}

		void Update()
		{
			ModelLocator.Game.GetAvatarModel( avatarType ).CurrentPosition = transform.position;

			if( !agent.hasPath && !checkedPath )
			{
				checkedPath = true;
				ModelLocator.Game.GetAvatarModel( avatarType ).State = AvatarState.idle;


			}
		}
	}
}