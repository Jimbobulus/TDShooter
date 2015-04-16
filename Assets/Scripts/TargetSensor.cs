using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSensor : MonoBehaviour
{

	public List<Transform> enemiesInRange = new List<Transform>();
		
	void Start ()
	{
		
	}
	/*
	void OnTriggerEnter( Collider other )
	{
		print ("Enter: "+other.name);
		if( other.tag == "Enemy" )
		{
			//print("adding..");
			if( !enemiesInRange.Contains( other.transform ))
				enemiesInRange.Add( other.transform );
		}
	}

	void OnTriggerExit( Collider other )
	{
		print ("Leave: "+other.name);
		if( other.tag == "Enemy" )
		{
			//print ("removing...");
			if( enemiesInRange.Contains( other.transform ))
				enemiesInRange.Remove( other.transform );
		}
	}
	*/

	void Update ()
	{
		
	}
}
