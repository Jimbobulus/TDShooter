using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {


	public Transform target;

	public LayerMask mask;

	private Vector3[] points;

	void Start ()
	{

		points = new Vector3[transform.childCount];
		Vector3 temp = Vector3.zero;
		for( int i = 0; i < transform.childCount; i++ )
		{
			temp = new Vector3( transform.GetChild(i).position.x, 0.0f, transform.GetChild(i).position.z );
			points[i] = temp;
		}
	}

	void Update ()
	{
		if( target != null )
		{
			transform.position = new Vector3( target.position.x, transform.position.y, target.position.z );
		}

		float nearestDist = 100.0f;
		Transform temp = null;
		for( int i = 0; i < points.Length; i++ )
		{
			Debug.DrawRay( transform.position, points[i], Color.blue );

			RaycastHit hit;
			if (Physics.Linecast(transform.position, points[i], out hit, mask)) 
			{

				if( hit.transform.tag == "Enemy" ) 
				{
					if( hit.distance < nearestDist )
					{
						Debug.DrawRay( transform.position, hit.point, Color.red );
						//print("HIT: "+hit.distance);
						nearestDist = hit.distance;
						temp = hit.transform;
					}
				}
			}
		}

		if( temp != null )
			target.SendMessage( "AimAtThisGuy", temp );
	}
}
