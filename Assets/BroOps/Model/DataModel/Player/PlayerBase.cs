using System.Collections;
using System;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;

namespace com.gamehound.broops.model
{
	public class PlayerBase 
	{
		public static string SCORE_CHANGED = "The score has changed for player";
		public static string SELECTED_AVATAR_CHANGED = "the currently selected avatar";

		public readonly Guid modelID;

		private Guid selectedAvatarBinding;
		public Guid SelectedAvatarBinding
		{
			get
			{
				return selectedAvatarBinding;
			}
			set
			{
				if( ModelLocator.Game.AvatarIsNotOwned( value ))
				{
					selectedAvatarBinding = value;
					ModelLocator.EventBus.DispatchGenericEvent( SELECTED_AVATAR_CHANGED, this, selectedAvatarBinding );
				}
				else
				{
					UnityEngine.Debug.Log( "Avatar has already been claimed by a player" );
					selectedAvatarBinding = new Guid();
				}
			}
		}

		private int score = 0;
		public int Score
		{
			get
			{
				return score;
			}
			set
			{
				score = value;

			}
		}

		public PlayerBase()
		{
			modelID = Guid.NewGuid();

			//UnityEngine.Debug.Log( "Created new : "+this+" modelID:"+modelID.ToString());
		}
	}
}