using UnityEngine;
using System.Collections;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;

namespace com.gamehound.broops.model
{
	public class EnemyModelBase : ActorModelBase 
	{
		public EnemyModelBase()
		{

		}
		
		protected override void Configure() {
			base.Configure();
			//UnityEngine.Debug.Log(this+" configured");
		}

	}
}