using UnityEngine;
using System.Collections;

public class ActorLocomotion : MonoBehaviour 
{
	public Transform graphics;
	public Transform navAgent;

	public float followSpeed = 5.0f;
	public float lookSpeed = 8.0f;

	private Vector3 lastPosition = Vector3.zero;

	Vector3 orientation;

    void OnDisable()
    {
        if (!ComponentsNull()) return;

        graphics.position = navAgent.position;
    }

	void Start () 
	{
        if (!ComponentsNull())
        {
            print("one of your components are not set, " + transform.name + " is on strike");
        }

	}

	void Update () 
	{
        if (!ComponentsNull()) return;

		graphics.position = Vector3.Lerp( graphics.position, navAgent.position, Time.deltaTime * followSpeed );
		
		orientation = graphics.position - lastPosition;
		
		if( orientation.sqrMagnitude > 0.1f )
		{
			orientation.y = 0.0f;
			graphics.rotation = Quaternion.Lerp( graphics.rotation, Quaternion.LookRotation( graphics.position - lastPosition ), Time.deltaTime * lookSpeed );
		}
		else
		{
			graphics.rotation = Quaternion.Lerp( graphics.rotation, Quaternion.LookRotation( navAgent.forward ), Time.deltaTime * lookSpeed );
		}
		lastPosition = graphics.position;
	}

    bool ComponentsNull()
    {
        if (graphics == null) return false;
        if (navAgent == null) return false;

        return true;
    }
}
