using UnityEngine;
using System.Collections;
using com.gamehound.broops.viewmodel;

public class PlayerMouseInput : MonoBehaviour 
{
	void Update () 
	{
		//OfflinePlayerViewTest.currentPointerPos = Input.mousePosition;

		if( Input.GetMouseButtonDown(0) )
		{
			//OfflinePlayerViewTest.oldPointerPos = Input.mousePosition;
			//OfflinePlayerViewTest.state = PlayerInputState.PointerDown;
		}

		if( Input.GetMouseButtonUp(0) )
		{
			//OfflinePlayerViewTest.state = PlayerInputState.PointerUp;
		}

	}
}
