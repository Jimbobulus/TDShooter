using UnityEngine;
using System.Collections;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;

namespace com.gamehound.broops.model
{
	public class AvatarTestModel : AvatarModelBase 
	{
		public AvatarTestModel()
		{
			//UnityEngine.Debug.Log("Avatar Test activated: "+modelID);
		}

		protected override void Configure() {
			base.Configure();
			//UnityEngine.Debug.Log(this+" configured");
		}

	}
}
