using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.gamehound.broops.model.datamodel;
using com.gamehound.broops.model.eventbus;

namespace com.gamehound.broops.model
{
	public enum GameState
	{
		init,
		title,
		gameOn,
		gameOver
	}

	public class GameModel : IDisposable 
	{
		public static string PAUSED_CHANGED = "game model pause status changed";
		public static string STATE_CHANGED = "game model state changed";
		public static string PLAYER_ADDED = "a player was added to the game";
		public static string AVATAR_ADDED = "an avatar was added to the game";
		public static string ENEMY_ADDED = "an enemy was added to the game";


		#region Observable Properties

		private bool m_isPaused = false;
		public bool IsPaused
		{
			get 
			{
				return m_isPaused;
			}
			set 
			{
				var oldValue = m_isPaused;
				if(value == oldValue)
				{
					return;
				}
				
				m_isPaused = value;
				
				ModelLocator.EventBus.DispatchGenericEvent(PAUSED_CHANGED, this, m_isPaused);
			}
		}

		private GameState m_previousState = GameState.init;
		public GameState PreviousState
		{
			get
			{
				return m_previousState;
			}
		}

		private GameState m_state = GameState.init;
		public GameState State
		{
			get
			{
				return m_state;
			}
			set 
			{
				var oldValue = m_state;
				if(value == oldValue)
				{
					return;
				}
				
				m_previousState = oldValue;
				m_state 		= value;
				
				ModelLocator.EventBus.DispatchGenericEvent(STATE_CHANGED, this, m_state);
			}
		}

		#endregion

		//
		//
		//----------------- OBSOLETE
		private List<PlayerDataModel> m_players = new List<PlayerDataModel>();
		public PlayerDataModel GetPlayerModel(Player forID)
		{
			return m_players.Find(x => x.PlayerID == forID);
		}
		public void AddPlayer(PlayerDataModel player)
		{
			m_players.Add(player);
			ModelLocator.EventBus.DispatchGenericEvent(PLAYER_ADDED, this, player.PlayerID);
		}
		public void RemovePlayer(PlayerDataModel player)
		{
			m_players.Remove(player);
		}

		private List<AvatarDataModel> m_avatars = new List<AvatarDataModel>();
		public AvatarDataModel GetAvatarModel( AvatarType forID)
		{
			return m_avatars.Find(x => x.AvatarID == forID);
		}
		public void AddAvatar( AvatarDataModel avatar )
		{
			m_avatars.Add( avatar );
			ModelLocator.EventBus.DispatchGenericEvent( AVATAR_ADDED, this, avatar.AvatarID );
		}
		public void RemoveAvatar( AvatarDataModel avatar )
		{
			m_avatars.Remove(avatar);
		}
		//----------------- OBSOLETE
		//
		//



		/*
		 * PLAYER
		 */ 
		private List<PlayerBase> m_PlayerModels = new List<PlayerBase>();

		public PlayerBase GetPlayerModel( Guid forID )
		{
			return m_PlayerModels.Find(x => x.modelID == forID);
		}

		public PlayerBase GetPlayerModel( int index )
		{
			if( index < m_PlayerModels.Count && index > -1 )
				return m_PlayerModels[ index ];

			// TODO: replace with a custom null player
			return null;
		}


		public bool AvatarIsNotOwned( Guid avatarBinding )
		{
			if( m_PlayerModels.Find( x => x.SelectedAvatarBinding == avatarBinding ) != null)
				return false;

			return true;
		}

		public void AddPlayer( PlayerBase player )
		{
			m_PlayerModels.Add( player );
			ModelLocator.EventBus.DispatchGenericEvent( PLAYER_ADDED, this, player.modelID );
			//UnityEngine.Debug.Log("adding new player "+player.modelID);
		}
		/// 


		private List<AvatarModelBase> m_avatarModels = new List<AvatarModelBase>();
		public AvatarModelBase GetAvatarModel( Guid forID )
		{
			return m_avatarModels.Find(x => x.modelID == forID);
		}
		public AvatarModelBase GetAvatarModel( int index )
		{
			if( index < m_avatarModels.Count && index > -1 )
				return m_avatarModels[ index ];

			return null;
		}
		public void AddAvatar( AvatarModelBase avatar )
		{
			m_avatarModels.Add( avatar );
			ModelLocator.EventBus.DispatchGenericEvent( AVATAR_ADDED, this, avatar.modelID );
			//UnityEngine.Debug.Log("adding new avatar "+avatar.modelID);
		}
		public void RemoveAvatar( AvatarModelBase avatar )
		{
			m_avatarModels.Remove(avatar);
		} 


		private List<EnemyModelBase> m_enemyModels = new List<EnemyModelBase>();
		public EnemyModelBase GetEnemyModel( Guid forID )
		{
			return m_enemyModels.Find(x => x.modelID == forID);
		}
		public void AddEnemy( EnemyModelBase enemy )
		{
			m_enemyModels.Add( enemy );
			ModelLocator.EventBus.DispatchGenericEvent( ENEMY_ADDED, this, enemy.modelID );
		}
		public void RemoveEnemy( EnemyModelBase enemy )
		{
			m_enemyModels.Remove(enemy);
		} 

		public GameModel()
		{
			//AddPlayer( new PlayerDataModel( Player.one ));
			//UnityEngine.Debug.Log("created player 1");

			// set to player one by default
			//AddAvatar( new AvatarDataModel( AvatarType.Blue ));
			//AddAvatar( new AvatarDataModel( AvatarType.Red ));

			//AvatarTestModel model = new AvatarTestModel();
		}

		public void Initialise()
		{
			ModelLocator.EventBus.OnGenericEvent += HandleOnGenericEvent;
		}

		void HandleOnGenericEvent (GenericEvent genEvent)
		{
			if( genEvent.token == PlayerDataModel.LIVES_CHANGED )
			{
				if( m_players.Count == 0 )
				{
					return;
				}
			}

		}



		#region IDisposable implementation
		
		public void Dispose ()
		{
			ModelLocator.EventBus.OnGenericEvent -= HandleOnGenericEvent;
		}
		
		#endregion
	}
}
