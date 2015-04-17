using UnityEngine;
using System.Collections;

namespace com.gamehound.broops.viewmodel
{
	public class PlayerMouseInputViewModel : OfflinePlayerViewTest 
	{
		private Vector3 oldMousePos = Vector3.zero;

		public override void Start () 
		{
			base.Start();
		}
		

		public override void Update () 
		{
			currentPointerPos = Input.mousePosition;

			if( Input.GetMouseButtonDown(0) )
			{
				//print ("mouse down");

				oldPointerPos = Input.mousePosition;

				PointerDown = true;
				PointerUp = false;
				PointerMoved = false;
			}

			if( Input.GetMouseButtonUp(0) )
			{
				//print ("mouse up");

				if( PointerDown ) 
				{
					PointerUp = true;
					PointerDown = false;
				}
			}

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                PointerCount = 2;
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                PointerCount = 1;
            }

			base.Update();
		}
	}
}
