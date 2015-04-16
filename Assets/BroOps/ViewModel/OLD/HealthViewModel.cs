using UnityEngine;
using System.Collections;
using com.gamehound.broops.model.eventbus;
using com.gamehound.broops.viewmodel;

namespace com.gamehound.broops.viewmodel
{
	public class HealthViewModel : BaseViewModel 
	{

		public override void Start()
		{
			base.Start();
		}

		public void TakeDamage( int damage )
		{
			SendMessage( "OnDamage", damage );
		}
	}
}