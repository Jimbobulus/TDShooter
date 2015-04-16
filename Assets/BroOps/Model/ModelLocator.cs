using System;
using com.gamehound.broops.model.eventbus;
using com.gamehound.broops.model.datamodel;

namespace com.gamehound.broops.model
{
	public static class ModelLocator 
	{
		//alternative to a singleton

		public static string GAME_THAWED = "the game data was deserialised";

		private static EventBus m_eventBus = null;
		public static EventBus EventBus
		{
			get
			{
				//lazy initialisation / factory method
				if(m_eventBus == null)
				{
					m_eventBus = new EventBus();
				}
				
				return m_eventBus;
			}
		}

		private static GameModel m_game = null;
		public static GameModel Game
		{
			get 
			{
				if(m_game == null)
				{
					//we initialise because dependency on the ModelRegistry within the constructor
					//can easily create a circular dependency and a nasty crash 
					//new models are requested, they are mid constructor, and so == null, so another 
					//instance is created, which then requests a new model, mid constructor and so on...
					m_game = new GameModel();
					m_game.Initialise();
				}
				
				return m_game;
			}
		}

		/*
		private static MinimalXMLLoader m_xml = null;
		public static MinimalXMLLoader XML
		{
			get
			{
				if(m_xml == null)
				{
					//this is important - we call initialise to prevent instantation loops 
					//if the constructor of one model relies on the model registry for another
					//model then it will branch infinitely - since at the time of calling each 
					//model will be mid - constructor and the private field will always reference null
					m_xml = new MinimalXMLLoader();
					m_xml.Initialise();
				}
				
				return m_xml;
			}
		}
		*/


		public static void Freeze()
		{
			UnityEngine.Debug.LogWarning("you need to provide serialisation functionality here");
			
			/*
			 * I have commented out this code, because it requires a commercial library:
			 * http://u3d.as/content/parent-element/json-net-for-unity/5q2
			 * 
			var freezedGame = JsonConvert.SerializeObject(m_game);
			UnityEngine.PlayerPrefs.SetString("gameModel", freezedGame);
			UnityEngine.PlayerPrefs.Save();
			*/
		}
		
		public static void Thaw()
		{
			UnityEngine.Debug.LogWarning("you need to provide deserialisation functionality here");
			
			/*
			 * I have commented out this code, because it requires a commercial library:
			 * http://u3d.as/content/parent-element/json-net-for-unity/5q2
			 * 
			var thawedGame = UnityEngine.PlayerPrefs.GetString("gameModel");
			m_game = JsonConvert.DeserializeObject<GameModel>(thawedGame);
			EventBus.DispatchGenericEvent(GAME_THAWED, null, null);
			*/
		}
	}
}
