using UnityEngine;
using System.Collections;
using com.gamehound.broops.model.datamodel;

namespace com.gamehound.broops.model
{
	public enum AvatarModelType
	{
		TestAvatar
	}

	public static class AvatarFactory 
	{
		public static AvatarModelBase CreateAvatar( AvatarModelType avatar )
		{

			AvatarModelBase toConfigure;

			switch( avatar )
			{
			default :
				toConfigure = new AvatarTestModel();
				break;
			}

			return toConfigure;
		}

	}
}