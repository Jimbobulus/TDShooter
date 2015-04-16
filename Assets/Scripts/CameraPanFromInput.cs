using UnityEngine;
using System.Collections;

public class CameraPanFromInput : MonoBehaviour {

	public Vector3 target;
	public Transform targetMarker;
	public NavMeshAgent agent;
	public Camera theCamera;
	public float moveLimit = 10.0f;
	public float cameraPanSpeedMultiplier = 1.0f;
	public float deadZoneLimit = 5.0f;

	//private int floorMask;

	private int mask;

	private bool mouseIsDown = false;
	private bool mouseMovedWhileDown = false;
	private bool mouseReleased = false;
	private Vector3 oldCamPos = Vector3.zero;

	private Vector3 targetForMoving = Vector3.zero;

	private Vector3 targetNewPos = Vector3.zero;

	private Vector3 newCamPos = Vector3.zero;
	private Vector3 oldMousePos = Vector3.zero;


	private Ray hit;

	void Start() {
		newCamPos = theCamera.transform.position; 
		mask = LayerMask.GetMask("Floor");
	}



	void Update()
	{

		if( Input.GetMouseButtonDown(0) )
		{
			oldMousePos = Input.mousePosition;

			if( oldMousePos.x < Screen.width / 4.5f && oldMousePos.y < Screen.height / 4.5f )
			{

			}
			else
			{
				oldCamPos = theCamera.transform.position;
				
				targetForMoving = theCamera.ScreenToWorldPoint( Input.mousePosition );
				hit = theCamera.ScreenPointToRay( Input.mousePosition );
				
				RaycastHit ray;
				
				if( Physics.Raycast( hit, out ray, 100.0f, mask ))
				{
					//if( ray.transform.tag == "UI" )
					//print ("Name: "+ray.transform.name);
					targetNewPos = ray.point;
					target = hit.origin;
					
					mouseIsDown = true;
					mouseReleased = false;
					mouseMovedWhileDown = false;
				}
			}
		}


		if( Input.GetMouseButtonUp(0) )
		{
			if( mouseIsDown ) 
			{
				mouseReleased = true;
				mouseIsDown = false;
			}
		}

		if( mouseIsDown )
		{
			//Vector3 temp = theCamera.ScreenToWorldPoint( Input.mousePosition );

			Ray tempHit = theCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit ray;
			
			Physics.Raycast( tempHit, out ray, 100.0f, mask );

			bool mouseMoved = DeadZoneCheck( oldMousePos, Input.mousePosition );

			if( mouseMoved )
			{
				mouseMovedWhileDown = true;

				Vector3 difference = targetNewPos - ray.point;

				
				newCamPos = oldCamPos + ( difference * cameraPanSpeedMultiplier	 );
				//newCamPos.Normalize();
				//newCamPos *= cameraPanSpeedMultiplier;
				//print(difference);
				//theCamera.transform.position = new Vector3( newCamPos.x, theCamera.transform.position.y, newCamPos.z );
				newCamPos = new Vector3( newCamPos.x, theCamera.transform.position.y, newCamPos.z ); 
				

			}


		}

		theCamera.transform.position = Vector3.Lerp( theCamera.transform.position, newCamPos, Time.deltaTime * moveLimit );

		if( mouseReleased )
		{
			mouseReleased = false;
			//Vector3 temp = theCamera.ScreenToWorldPoint( Input.mousePosition );

			//if( hit != null )
				

			if( !mouseMovedWhileDown )
			{
				Ray tempHit = theCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit ray;
				
				Physics.Raycast( tempHit, out ray, 100.0f, mask );

				targetMarker.position = ray.point;
				agent.SetDestination( targetMarker.position );
			}
		}

		//Vector3 newPos = new Vector3( agent.transform.position.x, theCamera.transform.position.y, agent.transform.position.z - 6.0f );
		
		//theCamera.transform.position = Vector3.Lerp( theCamera.transform.position, newPos, Time.deltaTime * moveLimit );
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
