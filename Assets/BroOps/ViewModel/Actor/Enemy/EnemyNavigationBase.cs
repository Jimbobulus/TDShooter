using UnityEngine;
using System.Collections;

namespace com.gamehound.broops.viewmodel
{
    public class EnemyNavigationBase : ActorNavigation
    {
        public override void Update()
        {
            base.Update();

            if (distanceToTarget <= (agent.stoppingDistance))
            {
                StopMoving();
            }
        }

        public override void SetDestination(Vector3 newDestination)
        {
            base.SetDestination(newDestination);
           
            if (newDestination != destination)
            {
                destination = newDestination;
                //print(agent.enabled);
                if (agent.enabled && agent.destination != newDestination)
                {
                    agent.destination = newDestination;
                }
                else
                {
                    if (distanceToTarget <= agent.stoppingDistance)
                        StopMoving();
                    else if (!IsInvoking("OnIdle"))
                        Invoke("OnIdle", pauseBeforeMoveTime);
                }
            }
        }


        public override void StopMoving()
        {
            base.StopMoving();
            
            agent.enabled = false;

            if (!agent.isActiveAndEnabled)
            {
                obstacle.enabled = true;
            }
        }


        protected override void OnIdle()
        {
            base.OnIdle();

            StartMoving();
        }
    }

}