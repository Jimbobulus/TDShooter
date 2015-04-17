using UnityEngine;
using System.Collections;

namespace com.gamehound.broops.viewmodel
{
    public class AvatarNavigationBase : ActorNavigation
    {
        private Vector3 oldDestination;
        bool pathChecked = true;


        public override void Update()
        {
            base.Update();

            if (!agent.hasPath)
            {
                StopMoving();
            }

            if (destination != oldDestination)
            {
                if (agent.enabled)
                    agent.destination = destination;
            }
        }

        public override void SetDestination(Vector3 newDestination)
        {
            base.SetDestination(newDestination);

            destination = newDestination;

            StartMoving();

            oldDestination = destination;
        }


        public override void StopMoving()
        {
            base.StopMoving();

            agent.enabled = false;

            if (!agent.isActiveAndEnabled)
                obstacle.enabled = true;
        }
    }
}