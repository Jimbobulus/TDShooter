using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.gamehound.broops.model.viewmodel
{
	public static class ViewModelLocator
	{
		private static Dictionary<string, GameObject> m_cachedGameObjects = new Dictionary<string, GameObject>();
		public static T GetViewModel<T>(string fromGameObjectNamed, bool cacheViewModel = true) where T:Component
		{
			if( false == cacheViewModel)
			{
				var found = FindAnywhere(fromGameObjectNamed);
				return found.GetComponent<T>();
			}
			
			if(false == m_cachedGameObjects.ContainsKey(fromGameObjectNamed))
			{
				var found = FindAnywhere(fromGameObjectNamed);
				m_cachedGameObjects.Add(fromGameObjectNamed, found);
			}
			
			GameObject entity = m_cachedGameObjects[fromGameObjectNamed];
			
			return entity.GetComponent<T>();
		}
		
		#region Code by Chris Underwood / DeeperBeige.com
		private static GameObject FindAnywhere(string named)
		{
			foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
			{
				// Root heirachy objects have no parent object
				if (obj.transform.parent != null) continue;
				
				// Looking for a root object?
				if (obj.transform.name == named) return obj;
				
				// Search children
				Transform result = FindInChildren(obj.transform, named);
				if (result != null) return result.gameObject;
			}
			
			return null;
		}
		private static Transform FindInChildren(Transform transform, string named)
		{
			for (int j = 0; j < transform.childCount; j++)
			{
				Transform child = transform.GetChild(j);
				if (child.name == named) return child;
				
				Transform found = FindInChildren(child, named);
				if (found != null) return found;
			}
			
			return null;
		}
		/*
		//gareth williams / Lonewolfwilliams.com
		private static Transform[] FindAllInChildren(Transform transform, string named)
		{
			for (int j = 0; j < transform.childCount; j++)
			{
				Transform child = transform.GetChild(j);
				if (child.name == named) return child;
				
				Transform found = FindInChildren(child, named);
				if (found != null) return found;
			}
			
			return null;
		}
		*/
		#endregion
	}
}
