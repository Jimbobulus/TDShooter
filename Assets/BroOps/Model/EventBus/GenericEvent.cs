using System;

namespace com.gamehound.broops.model.eventbus
{
	public class GenericEvent
	{
		//should be as generic as possible
		public string token;
		public object sender;
		public object payload;
	}
}