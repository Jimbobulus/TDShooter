using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour {

	public int damage = 2;

	void Start () {
	
	}
	

	void OnTriggerEnter( Collider other )
	{
		print("HIT");
		if( other.tag == "Player" )
		{
			other.SendMessage( "TakeDamage", damage );
		}
	}
}
