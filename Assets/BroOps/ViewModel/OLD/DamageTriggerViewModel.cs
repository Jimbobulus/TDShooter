using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.gamehound.broops.model;

namespace com.gamehound.broops.viewmodel
{
	public enum DamageTag
	{
		Avatar,
		Enemy
	}

	[RequireComponent(typeof(Collider))]
	public class DamageTriggerViewModel : BaseViewModel 
	{
		public static string ON_DAMAGE_TRIGGER = "this damage trigger has fired";

		public List<DamageTag> willDamage;

		public int damage = 1;

		private Collider mCollider;

		private Renderer mRenderer;


		private bool triggerEnabled;
		public bool TriggerEnabled
		{
			get
			{
				if( mCollider != null )
					return mCollider.enabled;

				return false;
			}
			set
			{
				if( mCollider != null ) 
					mCollider.enabled = value;

				if( mRenderer != null )
					mRenderer.enabled = value;
			}
		}

		public override void Start()
		{
			base.Start();
			mCollider = GetComponent<Collider>() as Collider;
			mRenderer = GetComponent<Renderer>() as Renderer;
		}
		

		void OnTriggerEnter( Collider other ) 
		{
			if( TagPresent( other.tag ))
			{
				//print ("send damage message");

				if( other.GetComponent<HealthViewModel>() != null )
					other.GetComponent<HealthViewModel>().TakeDamage( damage );
			}
		}

		bool TagPresent( string otherTag )
		{
			for( int i = 0; i < willDamage.Count; i++ )
				if( willDamage[i].ToString() == otherTag )
					return true;

			return false;
		}
	}
}
