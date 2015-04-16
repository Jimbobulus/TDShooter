using System;

/// <summary>
/// A Generic Event bus
/// </summary>
namespace com.gamehound.broops.model.eventbus
{
	public class EventBus : BaseModel 
	{
		public delegate void GenericEventHandler(GenericEvent genEvent);
		public event GenericEventHandler OnGenericEvent;
		public void DispatchGenericEvent(string token, object sender, object payload)
		{
			if(OnGenericEvent != null)
			{
				GenericEvent genEventArgs 	= 	new GenericEvent()
				{
					token 	= token,
					sender 	= sender,
					payload = payload
				};
				
				//Jesper Juul - don't allow poor eventhandling code to crash the app
				try
				{
					OnGenericEvent(genEventArgs);
				}
				catch(Exception e)
				{
					UnityEngine.Debug.Log(e.Message + " " + e.StackTrace);
				}
			}
		}
		
		public override void Initialise()
		{
			UnityEngine.Debug.Log("event bus initialised");
		}
	}
}
