using System;

/// <summary>
/// Base class for all Models, ensures that all models have an Initialise method (requirement of Model-Locator)
/// </summary>
namespace com.gamehound.broops.model
{
	public abstract class BaseModel 
	{
		public abstract void Initialise();
	}
}
