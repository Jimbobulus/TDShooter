using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAvatar : MonoBehaviour 
{
	public int health = 100;
	public int damage = 5;

	public float range = 5.0f;

	public Transform enemyHolder;

	public CameraPanFromInput input;
	public Transform targetMarker;

	public Material selectedMaterial;

	public SpriteRenderer rangeUISprite;
	public float spriteFadeSpeed = 6.0f;

	public Transform pointer;

	public float fireRate = 0.15f;

	private float rangeSpriteFade = 5.0f;


	private Material unselectedMaterial;

	private UnityEngine.AI.NavMeshAgent agent;

	private bool canFire = true;

	private Renderer renderer;

	private LineRenderer lineRenderer;

	private Color origColour;



	void Start () 
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>() as UnityEngine.AI.NavMeshAgent;
		renderer = transform.Find("Graphics").GetComponent<Renderer>();
		unselectedMaterial = renderer.material;
		lineRenderer = transform.Find("Line").transform.GetComponent<LineRenderer>() as LineRenderer;
		lineRenderer.enabled = false;
		origColour = rangeUISprite.color;
	}


	public void Avatar1Select( Toggle toggle )
	{
		if( toggle.isOn )
		{
			renderer.material = unselectedMaterial;
		}
		else
		{
			input.targetMarker = targetMarker;
			input.agent = agent;
			renderer.material = selectedMaterial;
		}
	}

	public void Avatar2Select( Toggle toggle )
	{
		if( toggle.isOn )
		{
			input.targetMarker = targetMarker;
			input.agent = agent;
			renderer.material = selectedMaterial;
		}
		else
		{
			renderer.material = unselectedMaterial;
		}

	}

	Vector3 enemyTempPos = Vector3.zero;

	void Update () 
	{
		if( !agent.hasPath )
		{
			agent.updateRotation = false;
			if( canFire )
			{
				float maxDist = 1000.0f;
				Transform enemy = null;
				for( int i = 0; i < enemyHolder.childCount; i++ )
				{
					Transform item = enemyHolder.GetChild(i).transform;

					float dist = Vector3.Distance( transform.position, item.position );
				
					if( dist < maxDist )
					{
						maxDist = dist;

						if( dist < range )
						{
							canFire = false;
							enemy = item;
						}
					}
				}

				if( enemy != null )
				{
					//if( Vector3.Distance( transform.position, enemy.position ) <= range )
					if( enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled )
					{
						enemy.SendMessage( "TakeDamage", damage );

						//enemyTempPos = enemy.position;

						pointer.position = enemy.position;

						lineRenderer.SetPosition(0, transform.position );
						lineRenderer.SetPosition(1, enemy.position );

						//Debug.DrawLine( transform.position, enemy.position, Color.blue, 0.15f );

						enemyTempPos = enemy.position;


						StartCoroutine( "SetCanFire" );
					}
				}



			}

			if( Vector3.Distance( transform.position, enemyTempPos ) < range )
			{
				Vector3 diff = transform.position - enemyTempPos;
				diff.Normalize();
				
				float rotY = Mathf.Atan2( diff.y, diff.x ) * Mathf.Rad2Deg;
				
				Quaternion newRotation = Quaternion.Euler( new Vector3( 0.0f, rotY, 0.0f ));
				
				//transform.rotation = Quaternion.Lerp( transform.rotation, newRotation, Time.deltaTime * 100.0f );

				//transform.LookAt(enemyTempPos, transform.up);

				//transform.rotation = Quaternion.RotateTowards( transform.rotation,newRotation,Time.deltaTime * 1000.0f );

				//transform.rotation = Quaternion.Slerp( transform.rotation, newRotation, Time.deltaTime * 10.0f );

				dir = ( new Vector3( enemyTempPos.x, transform.position.y, enemyTempPos.z ) - transform.position).normalized;
				lookRot = Quaternion.LookRotation(dir);

				transform.rotation = Quaternion.Slerp( transform.rotation, lookRot , Time.deltaTime * 20.0f );
			}
		}
		else
		{
			agent.updateRotation = true;
		}

		if( Vector3.Distance( transform.position, enemyTempPos ) > range )
		{
			lineRenderer.enabled = false;
		}
		else
		{

		}



		rangeSpriteFade = Mathf.Lerp( rangeSpriteFade, 0.0f, Time.deltaTime * spriteFadeSpeed );

		rangeUISprite.color = new Color( origColour.r, origColour.g, origColour.b, rangeSpriteFade );

		lineRenderer.SetColors( rangeUISprite.color, rangeUISprite.color );
	}

	Vector3 dir = Vector3.zero;
	Quaternion lookRot;

	IEnumerator SetCanFire()
	{
		lineRenderer.enabled = true;
		rangeSpriteFade = 0.75f;

		yield return new WaitForSeconds(0.1f);
		lineRenderer.enabled = false;

		yield return new WaitForSeconds( fireRate );
		canFire = true;
	}

	public void TakeDamage( int theDamage )
	{
		health -= theDamage;


	}

	public void AimAtThisGuy( Transform enemy )
	{
		//float dist = Vector3.Distance( transform.position, enemy.position );

		//print("name: "+ enemy.name);

		enemy.SendMessage( "TakeDamage", damage );

	}
}
