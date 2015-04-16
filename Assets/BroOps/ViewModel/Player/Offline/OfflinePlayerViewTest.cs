using UnityEngine;
using System;
using System.Collections;
using com.gamehound.broops.model.eventbus;
using com.gamehound.broops.model;

namespace com.gamehound.broops.viewmodel
{
	public enum PlayerInputState
	{
		PointerDown,
		PointerUp,
		PointerMoving,
		None
	}

	public class OfflinePlayerViewTest : OfflinePlayerViewBase 
	{
		public Camera theCamera;
		
		public float cameraPanSpeedMultiplier = 1.0f;
		
		public float moveLimit = 10.0f;
		
		public float deadZoneLimit = 5.0f;
		
		protected Vector3 target;

        protected Vector3 newCamPos = Vector3.zero;
		protected Vector3 oldCamPos = Vector3.zero;
		
		protected Vector3 oldPointerPos = Vector3.zero;
		protected Vector3 currentPointerPos = Vector3.zero;
		
		protected Vector3 targetForMoving = Vector3.zero;
		
		protected Vector3 targetNewPos = Vector3.zero;
		
		protected int mask;
		
		protected Ray hit;
		
		private bool pointerDown = false;
		public bool PointerDown
		{
			get
			{
				return pointerDown;
			}
			set
			{
				pointerDown = value;
				
				// down once
				if( pointerDown )
				{
					//bool isMouseOverCornerButton = oldPointerPos.x < Screen.width / 4.5f && oldPointerPos.y < Screen.height / 4.5f;
					//print ("pointer down");
					//if( !isMouseOverCornerButton )
					//{
						oldCamPos = theCamera.transform.position;
						targetForMoving = theCamera.ScreenToWorldPoint( oldPointerPos );
						hit = theCamera.ScreenPointToRay( oldPointerPos );
						
						RaycastHit ray;
						if( Physics.Raycast( hit, out ray, 100.0f, mask ))
						{
							targetNewPos = ray.point;
							target = hit.origin;
						}
					//}
				}
			}
			
		}
		
		private bool pointerMoved = false;
		public bool PointerMoved
		{
			get
			{
				return pointerMoved;
			}
			set
			{
				pointerMoved = value;
			}
		}
		
		
		private bool pointerUp = false;
		public bool PointerUp
		{
			get
			{
				return pointerUp;
			}
			set
			{
				pointerUp = value;
			}
		}
		
		public override void Start()
		{
			base.Start();

			newCamPos = theCamera.transform.position; 
			mask = LayerMask.GetMask("Floor");

            OnConfigure();
		}
		
		public virtual void Update()
		{
			// BINDING CHECKER
            if (modelBinding == Guid.Empty)
            {
                //print("Player "+transform.name+" not bound to a model");
                return;
            }
            else if(null == ModelLocator.Game.GetPlayerModel(modelBinding))
			{
				//print ("no player model was found in the game");
				return;
			}
			///////////////


            // AVATAR CHECKER
			if( ModelLocator.Game.GetPlayerModel( modelBinding ).SelectedAvatarBinding == Guid.Empty )
			{
				//print (transform.name+" polling for an avatar...");
				if( ModelLocator.Game.GetAvatarModel(0) != null )
				{
                    //print("binding avatar " + ModelLocator.Game.GetAvatarModel(0).modelID);
					ModelLocator.Game.GetPlayerModel( modelBinding ).SelectedAvatarBinding = ModelLocator.Game.GetAvatarModel(0).modelID;
				}
			}
			//////////////////

            
			// down every frame
			if( pointerDown )
			{
				//print ("pointer held down");

                Ray tempHit = theCamera.ScreenPointToRay(currentPointerPos);
                RaycastHit ray;

                Physics.Raycast(tempHit, out ray, 100.0f, mask);

                Vector3 difference = targetNewPos - ray.point;

                newCamPos = oldCamPos + (difference * cameraPanSpeedMultiplier);
                newCamPos = new Vector3(newCamPos.x, theCamera.transform.position.y, newCamPos.z); 
				bool deadZoneLeft = DeadZoneCheck( oldPointerPos, currentPointerPos );
				
				if( deadZoneLeft )
				{
					//print ("dead zone left");
					pointerMoved = true;
				}
			}

            
			
			theCamera.transform.position = Vector3.Lerp( theCamera.transform.position, newCamPos, Time.deltaTime * moveLimit );
			
			// up once
			if( pointerUp )
			{
				pointerUp = false;
				if( !pointerMoved )
				{
                    Ray tempHit = theCamera.ScreenPointToRay(currentPointerPos);
					RaycastHit ray;
					
					Physics.Raycast( tempHit, out ray, 100.0f, mask );

					//print ("model binding "+modelBinding);
					var avatarBinding = ModelLocator.Game.GetPlayerModel( modelBinding ).SelectedAvatarBinding;
					//print ("avatar binding "+avatarBinding);

					if( (Guid)avatarBinding != Guid.Empty )
						ModelLocator.Game.GetAvatarModel( avatarBinding ).TargetMarkerPosition = ray.point;
				}
			}
		}

        protected override void HandleEventBus(GenericEvent genEvent)
        {
            base.HandleEventBus(genEvent);
            
            if (genEvent.token == GameModel.AVATAR_ADDED)
            {
                print("FOUND AVATAR");
                if (ModelLocator.Game.GetPlayerModel(modelBinding).SelectedAvatarBinding == Guid.Empty)
                    ModelLocator.Game.GetPlayerModel(modelBinding).SelectedAvatarBinding = (Guid)genEvent.payload;
            }
        }

        protected override void OnConfigure()
        {
            base.OnConfigure();

            //print("CREATED:::: " + ModelLocator.Game.GetPlayerModel(modelBinding).modelID);
        }
		
		
		bool DeadZoneCheck( Vector3 oldPos, Vector3 newPos )
		{
			if((( newPos.x + deadZoneLimit ) < oldPos.x ) || 
			   (( newPos.x - deadZoneLimit ) > oldPos.x ))
			{
				//print("PAN X");
				//leftDeadZone = false;
				return true;
			}
			
			if((( newPos.y + deadZoneLimit ) < oldPos.y ) || 
			   (( newPos.y - deadZoneLimit ) > oldPos.y ))
			{
				//print("PAN Y");
				//leftDeadZone = false;
				return true;
			}
			
			if((( newPos.z + deadZoneLimit ) < oldPos.z ) || 
			   (( newPos.z - deadZoneLimit ) > oldPos.z ))
			{
				//print("PAN Z");
				//leftDeadZone = false;
				return true;
			}
			
			return false;
		}
	}
}
