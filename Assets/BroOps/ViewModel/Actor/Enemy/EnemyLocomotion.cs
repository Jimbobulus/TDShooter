using UnityEngine;
using System.Collections;
namespace com.gamehound.broops.viewmodel
{
    public class EnemyLocomotion : ActorLocomotion
    {

        public ActorNavigationState state = ActorNavigationState.Idle;

        public Vector3 lookTarget;

        public override void Update()
        {
            base.Update();

            /*
            if (orientation.sqrMagnitude > 0.1f)
            {
                orientation.y = 0.0f;
                graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.LookRotation(graphics.position - lastPosition), Time.deltaTime * lookSpeed);
            }
            else
            {
                graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.LookRotation(navAgent.forward), Time.deltaTime * lookSpeed);
            }
             * */

            if(state == ActorNavigationState.Idle)
            {
                graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.LookRotation(lookTarget - graphics.position), Time.deltaTime * lookSpeed);
            }

            if(state == ActorNavigationState.Moving)
            {
                print("!");
                graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.LookRotation(navAgent.forward), Time.deltaTime * lookSpeed);
            }

        }
    }
}
