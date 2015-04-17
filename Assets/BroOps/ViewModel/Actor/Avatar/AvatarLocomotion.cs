using UnityEngine;
using System;


namespace com.gamehound.broops.viewmodel
{
    public class AvatarLocomotion : ActorLocomotion
    {
        public FindClosestByTag targetFinder;

        public override void Update()
        {
            base.Update();


            orientation = graphics.position - lastPosition;

            if (targetFinder.HasTarget)
            {
                graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.LookRotation(targetFinder.ClosestTarget - graphics.position), Time.deltaTime * lookSpeed);
            }
            else
            {
                if (orientation.sqrMagnitude > 0.1f)
                {
                    orientation.y = 0.0f;
                    graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.LookRotation(graphics.position - lastPosition), Time.deltaTime * lookSpeed);
                }
                else
                {
                    graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.LookRotation(navAgent.forward), Time.deltaTime * lookSpeed);
                }
            }


            lastPosition = graphics.position;
        }
    }
}
