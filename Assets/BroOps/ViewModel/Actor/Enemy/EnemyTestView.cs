using UnityEngine;
using System.Collections;
using System;
using com.gamehound.broops.model;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;

namespace com.gamehound.broops.viewmodel
{
	public class EnemyTestView : EnemyViewBase 
	{
        public ActorNavigation navigation;
        public EnemyLocomotion locomotion;
        public Transform graphics;


        bool bindingSet = false;

        public override void Start()
		{
			base.Start();

            var model = EnemyFactory.CreateEnemy(EnemyModelType.TestEnemy);
            ModelLocator.Game.AddEnemy(model);
            modelBinding = model.modelID;
            bindingSet = true;

            OnConfigure();
		}

		protected override void HandleEventBus(GenericEvent genEvent)
		{
			base.HandleEventBus(genEvent);

			if( genEvent.token == AvatarModelBase.POSITION_CHANGED )
			{
                navigation.SetDestination((Vector3)genEvent.payload);
            }
		}

        
		void Update()
		{
            if (!bindingSet) return;

            if (navigation.state == ActorNavigationState.Idle)
            {
                //print("ATTACK!!");
                locomotion.lookTarget = navigation.destination;
                locomotion.state = navigation.state;
            }
            else if (navigation.state == ActorNavigationState.Moving)
            {
                //print("CHASE!!!");
                locomotion.state = navigation.state;
            }
		}
        

        

		protected override void OnConfigure()
		{
			base.OnConfigure();

            var model = ModelLocator.Game.GetEnemyModel( modelBinding );
            navigation.OnConfigure();
            //navigation.isChasing = true;


            //print("enemy configured");
		}
         
	}
}