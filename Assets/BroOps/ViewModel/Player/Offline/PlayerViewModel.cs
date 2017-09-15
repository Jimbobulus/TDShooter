using UnityEngine;
using System.Collections;
using com.gamehound.broops.model;
using com.gamehound.broops.model.datamodel;


namespace com.gamehound.broops.viewmodel
{
	public class PlayerViewModel : OfflinePlayerViewBase
	{

		//public delegate void PlayerEventHandler(Player playerID);
		//public event PlayerEventHandler OnPlayerOutOfBounds; 
		
		public Player playerBinding;

		public Camera theCamera;

		public float cameraPanSpeedMultiplier = 1.0f;

		public float moveLimit = 10.0f;

		public float deadZoneLimit = 5.0f;

		public Transform targetMarker;

		public UnityEngine.AI.NavMeshAgent agent;

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
					bool isMouseOverCornerButton = oldPointerPos.x < Screen.width / 4.5f && oldPointerPos.y < Screen.height / 4.5f;
					
					if( !isMouseOverCornerButton )
					{
						oldCamPos = theCamera.transform.position;
						targetForMoving = theCamera.ScreenToWorldPoint( oldPointerPos );
						hit = theCamera.ScreenPointToRay( oldPointerPos );
						
						RaycastHit ray;
						if( Physics.Raycast( hit, out ray, 100.0f, mask ))
						{

							targetNewPos = ray.point;
							target = hit.origin;
						}
					}
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

			newCamPos = theCamera.transform.position; 
			mask = LayerMask.GetMask("Floor");

			//PlayerDataModel playerModel = ModelLocator.Game.GetPlayerModel(playerBinding);
			//print (playerModel);

			base.Start();
		}

		public virtual void Update()
		{
			if(null == ModelLocator.Game.GetPlayerModel(playerBinding))
				return;

			// down every frame
			if( pointerDown )
			{
				Ray tempHit = theCamera.ScreenPointToRay(currentPointerPos);
				RaycastHit ray;
				
				Physics.Raycast( tempHit, out ray, 100.0f, mask );
				
				bool deadZoneLeft = DeadZoneCheck( oldPointerPos, currentPointerPos );
				
				if( deadZoneLeft )
				{
					pointerMoved = true;
					
					Vector3 difference = targetNewPos - ray.point;

					newCamPos = oldCamPos + ( difference * cameraPanSpeedMultiplier	 );
					newCamPos = new Vector3( newCamPos.x, theCamera.transform.position.y, newCamPos.z ); 
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
					
					//AvatarType avatarType = ModelLocator.Game.GetPlayerModel(playerBinding).SelectedAvatar;
					//ModelLocator.Game.GetAvatarModel( avatarType ).TargetMarkerPosition = ray.point;
				}
				
			}


			////////////////
			/// TEMP AVATAR SWAP
			/// 
			/// 
			/*
			if( Input.GetKeyUp( KeyCode.Z ))
			{
				AvatarType selected = ModelLocator.Game.GetPlayerModel( playerBinding ).SelectedAvatar;
				switch( selected )
				{
				case AvatarType.Blue:
					ModelLocator.Game.GetPlayerModel( playerBinding ).SelectedAvatar = AvatarType.Red;
					break;
				default:
					ModelLocator.Game.GetPlayerModel( playerBinding ).SelectedAvatar = AvatarType.Blue;
					break;
				}

			}
			*/
			///
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
		
		//private float m_angle;

		/*
		// Update is called once per frame
		void Update () 
		{
			var playerModel = ModelLocator.Game.GetPlayerModel(playerBinding);
			
			if(playerModel == null)
			{
				return;
			}
			
			if(Input.GetKey(playerModel.upKey))
			{
				gameObject.transform.position += gameObject.transform.forward * -playerModel.speed;
			}
			else if(Input.GetKey(playerModel.downKey))
			{
				gameObject.transform.position += gameObject.transform.forward * playerModel.speed;
			}
			else if(Input.GetKey(playerModel.leftKey))
			{
				m_angle -= playerModel.speed;
			}
			else if(Input.GetKey(playerModel.rightKey))
			{
				m_angle += playerModel.speed;
			}
			
			gameObject.transform.rotation = Quaternion.AngleAxis(m_angle, gameObject.transform.up);
			
			ModelLocator.Game.GetPlayerModel(playerBinding).position = transform.position;
			ModelLocator.Game.GetPlayerModel(playerBinding).rotation = transform.rotation;
			
			//score for "surviving"
			ModelLocator.Game.GetPlayerModel(playerBinding).Score++;
		}
		
		void OnTriggerEnter(Collider other)
		{
			if(	other.name == "KillZone" &&
			   OnPlayerOutOfBounds != null)
			{
				ModelLocator.Game.GetPlayerModel(playerBinding).Lives --;
				OnPlayerOutOfBounds(playerBinding);
			}
		}
		*/
	}
}