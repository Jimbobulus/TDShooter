using UnityEngine;
using System.Collections;
using System;
using com.gamehound.broops.model;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;


namespace com.gamehound.broops.viewmodel
{
	public class AvatarTestView : AvatarViewBase 
	{
        public ActorNavigation navigation;
        public Transform graphics;
        bool bindingSet = false;

        void OnEnable()
        {            
            if (bindingSet)
                ModelLocator.Game.GetAvatarModel(modelBinding).IsEnabled = true;
        }

        void OnDisable()
        {
            if (bindingSet)
                ModelLocator.Game.GetAvatarModel(modelBinding).IsEnabled = false;
        }

		public override void Start()
		{
			base.Start();

			var model = AvatarFactory.CreateAvatar( AvatarModelType.TestAvatar );
			ModelLocator.Game.AddAvatar( model );
			modelBinding = model.modelID;
            bindingSet = true;

			OnConfigure();
		}


        void Update()
        {
            if (!bindingSet) return;
            
            ModelLocator.Game.GetAvatarModel(modelBinding).CurrentPosition = graphics.position;
        }

		

		protected override void HandleEventBus(GenericEvent genEvent)
		{
			base.HandleEventBus(genEvent);

            if (!gameObject.activeSelf) return;

			if( genEvent.token == AvatarTestModel.SET_MARKER )
			{
                if ( BindingCheck( (object)ModelLocator.Game.GetAvatarModel(modelBinding), genEvent.sender ))
                {
                    navigation.SetDestination(ModelLocator.Game.GetAvatarModel(modelBinding).TargetMarkerPosition);
                }
			}
		}

        protected override void OnConfigure()
        {
            base.OnConfigure();

            var model = (AvatarTestModel)ModelLocator.Game.GetAvatarModel(modelBinding) as AvatarTestModel;

            navigation.OnConfigure(model.Speed, model.Acceleration, model.AngularSpeed);
        }



        
        void RandomMovement()
        {
            float x = UnityEngine.Random.Range( -10.0f, 10.0f );
            float z = UnityEngine.Random.Range( -10.0f, 10.0f );
            Vector3 pos = new Vector3(x, 0.0f, z);
            ModelLocator.Game.GetAvatarModel( modelBinding ).TargetMarkerPosition = pos;

            //print ("model: "+modelBinding+"new pos: "+pos);
        }
        
		
	}
}
