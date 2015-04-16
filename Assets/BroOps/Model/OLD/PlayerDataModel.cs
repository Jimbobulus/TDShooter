using System;
using System.Collections.Generic;
using com.gamehound.broops.model;
using UnityEngine;

namespace com.gamehound.broops.model.datamodel
{

	public enum Player
	{
		one,
		two
	}

	public class PlayerDataModel 
	{

		public static string LIVES_CHANGED = "player lives have changed";
		public static string AVATARS_CHANGED = "the avatars have changed";
		public static string CURRENT_AVATAR_CHANGED = "the currently active avatar has changed";
		public static string SELECTED_AVATAR_CHANGED = "the currently selected avatar";

		public PlayerDataModel( Player playerID )
		{
			this.PlayerID = playerID;
		}
		public readonly Player PlayerID;

		private AvatarType selectedAvatar = AvatarType.Blue;
		public AvatarType SelectedAvatar
		{
			get
			{
				return selectedAvatar;
			}
			set
			{
				selectedAvatar = value;
				ModelLocator.EventBus.DispatchGenericEvent( SELECTED_AVATAR_CHANGED, this, selectedAvatar );
			}
		}

		/*
		private List<AvatarDataModel> avatars = new List<AvatarDataModel>();
		public AvatarDataModel GetAvatarDataModel(AvatarType forID)
		{
			return avatars.Find(x => x.AvatarID == forID);
		}
		public void AddAvatar(AvatarDataModel avatar)
		{
			avatars.Add( avatar );
			ModelLocator.EventBus.DispatchGenericEvent( AVATARS_CHANGED, this, avatar.AvatarID );
		}
		public void RemoveAvatar(AvatarDataModel avatar)
		{
			avatars.Remove(avatar);
		}

		private AvatarDataModel activeAvatar;
		public AvatarDataModel ActiveAvatar
		{
			get
			{
				return avatars[ currentAvatarIndex ];
			}
		}


		private int currentAvatarIndex = 0;

		public void SelectAvatar( int newAvatarIndex )
		{
			if( newAvatarIndex < avatars.Count && newAvatarIndex  > 0 )
			{
				currentAvatarIndex = newAvatarIndex;
				ModelLocator.EventBus.DispatchGenericEvent( CURRENT_AVATAR_CHANGED, this, currentAvatarIndex );
			}
		}
		*/

	}
}
