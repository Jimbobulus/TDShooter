using UnityEngine;
using System.Collections;
using System;
using com.gamehound.broops.model.eventbus;
using com.gamehound.broops.model;

namespace com.gamehound.broops.viewmodel
{
	public abstract class BaseViewModel : MonoBehaviour
	{
		public virtual void Start()
		{
			ModelLocator.EventBus.OnGenericEvent += HandleEventBus;
		}
		
		protected virtual void HandleEventBus(GenericEvent genEvent)
		{
			return;
		}
		
		public virtual void OnDestroy()
		{
			ModelLocator.EventBus.OnGenericEvent -= HandleEventBus;
		}

		protected virtual void OnConfigure() {}

		protected bool BindingCheck( Guid guidA, object guidB )
		{
			if( guidB.GetType() != typeof(Guid) ) 	return false;

			if( (Guid)guidB == Guid.Empty ) 		return false;

			if( guidA != Guid.Empty ) 				return false;

			return true;
		}

        protected bool BindingCheck( object objA, object objB )
        {
            return Guid.Equals(objA, objB);
        }
	}
}
