using UnityEngine;
using System.Collections;

public class ChasePlayer : MonoBehaviour {

	public int health = 100;

	public float range = 1;
	public Collider detectPlayerCollider;

	private Renderer detectPlayerRenderer;
	private int origHealh;

	private GameObject[] players;
	private Transform playerTarget;
	private UnityEngine.AI.NavMeshAgent agent;


	private bool canFire = true;

	void Start () 
	{
		origHealh = health;
		players = GameObject.FindGameObjectsWithTag("Player");
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>() as UnityEngine.AI.NavMeshAgent;
		detectPlayerCollider.enabled = false;

		detectPlayerRenderer = detectPlayerCollider.transform.GetComponent<Renderer>() as Renderer;
		detectPlayerRenderer.enabled = false;

	}
	

	void Update () 
	{
		if( agent.enabled )
		{
			float closestPlayer = 2000.0f;
			for( int i = 0; i < players.Length; i++ )
			{

				Transform player = players[i].transform;
				if( player != null )
				{

					float dist = Vector3.Distance( transform.position, player.position );
					if( dist < closestPlayer )
					{
						closestPlayer = dist;
						playerTarget = player;
					}
				}
			}

			if( playerTarget != null )
			{
				agent.SetDestination( playerTarget.position );
				if( closestPlayer < range )
				{
					if( canFire )
					{
						canFire = false;
						StartCoroutine( "Fire" );
					}
				}
			}
		}
		/*
		if( player != null && agent.enabled )
		{
			agent.SetDestination( player.position );

			float dist = Vector3.Distance( transform.position, player.position );

			if( dist < range )
			{
				if( canFire )
				{
					canFire = false;
					StartCoroutine( "Fire" );
				}
			}
		}
		*/
	}

	IEnumerator Fire()
	{
		detectPlayerCollider.enabled = true;
		detectPlayerRenderer.enabled = true;
		yield return new WaitForSeconds(0.5f);


		detectPlayerCollider.enabled = false;
		detectPlayerRenderer.enabled = false;

		yield return new WaitForSeconds(3.0f);

		canFire = true;
	}


	public void TakeDamage( int damage )
	{
		health -= damage;
		agent.velocity = Vector3.zero;
		if( health <= 0 )
		{
			agent.enabled = false;
			transform.position = new Vector3( 1000.0f, 0.0f, 0.0f );
			Invoke( "Respawn", 5.0f );
		}
	}

	void Respawn()
	{
		transform.position = new Vector3( Random.Range( -20.0f, 20.0f ), 0.5f, Random.Range( -20.0f, 20.0f ));
		agent.enabled = true;
		health = origHealh;
	}
}
