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

        public float walkingSpeed = 3.5f;
        public float runningSpeed = 7.0f;

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


            /*
            // TODO: REMOVE TO PLAYER INPUT!!!!!
            ///
            if( Input.GetKeyDown(KeyCode.LeftControl ))
            {
                //var model = (AvatarTestModel)ModelLocator.Game.GetAvatarModel(modelBinding) as AvatarTestModel;
                navigation.Speed = walkingSpeed;
            }

            if( Input.GetKeyUp(KeyCode.LeftControl))
            {
                //var model = (AvatarTestModel)ModelLocator.Game.GetAvatarModel(modelBinding) as AvatarTestModel;
                navigation.Speed = runningSpeed;
            }
            //
            ////
             * */
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

            if( genEvent.token == AvatarTestModel.MOVEMENT_TYPE_CHANGED)
            {
                if (BindingCheck((object)ModelLocator.Game.GetAvatarModel(modelBinding), genEvent.sender))
                {



                    print((MovementType)genEvent.payload);

                    switch( (MovementType)genEvent.payload )
                    {
                            
                        case MovementType.walking :
                            
                            navigation.Speed = walkingSpeed;
                            break;

                        case MovementType.running :
                           
                            navigation.Speed = runningSpeed;
                            break;

                        default :
                            navigation.Speed = ModelLocator.Game.GetAvatarModel(modelBinding).Speed;
                            break;
                    }
                    //navigation.Speed = ModelLocator.Game.GetAvatarModel(modelBinding).Speed;
                }
            }
		}

        protected override void OnConfigure()
        {
            base.OnConfigure();

            var model = (AvatarTestModel)ModelLocator.Game.GetAvatarModel(modelBinding) as AvatarTestModel;

            model.Speed = runningSpeed;
            model.AngularSpeed = 2000.0f;
            model.Acceleration = 20.0f;

            navigation.OnConfigure(model.Speed, model.Acceleration, model.AngularSpeed);
            GetComponent<ActorLocomotion>().followSpeed = model.Speed;
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
