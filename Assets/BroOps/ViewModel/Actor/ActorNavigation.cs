using UnityEngine;
using System.Collections;
using com.gamehound.broops.model;
using com.gamehound.broops.model.datamodel;

namespace com.gamehound.broops.viewmodel
{
    public enum ActorNavigationState
    {
        Moving,
        Idle
    }

    public enum ActorMovementStrategy
    {
        Follow,
        Directed
    }

    public class ActorNavigation : MonoBehaviour
    {

        public NavMeshAgent agent;
        public NavMeshObstacle obstacle;
        public ActorMovementStrategy strategy;

        public ActorNavigationState state;

        [HideInInspector]
        public Vector3 spawnPoint = Vector3.zero;

        [HideInInspector]
        public bool isChasing = true;

         [HideInInspector]
        public float speed = 0.0f;

         [HideInInspector]
        public float acceleration = 0.0f;

         [HideInInspector]
        public float angularSpeed = 0.0f;

        [HideInInspector]
        public Vector3 destination = Vector3.zero;
        private Vector3 oldDestination;


        //Vector3 lastPosition = Vector3.zero;
        bool pathChecked = true;
        bool checkedIdle = false;

        float dist = 0.0f;
        float targetDist = 0.0f;
        Vector3 newPosition = Vector3.zero;

        Vector3 targetPosition = Vector3.zero;
        Vector3 prevPosition = Vector3.zero;

        void OnEnable()
        {
            if (agent.enabled)
                agent.ResetPath();

            agent.enabled = false;
            obstacle.enabled = false;
        }

        void OnDisable()
        {

            // TODO: setspawn method
            spawnPoint = new Vector3(spawnPoint.x, transform.position.y, spawnPoint.z);

            agent.Warp(spawnPoint);
        }


        // Use this for initialization
        void Start()
        {
            destination = transform.position;
        }

        
        void Update()
        {
            // component collision check - NEVER CROSS THE STREAMS!!!!
            if (agent.enabled && obstacle.enabled)
                obstacle.enabled = false;

            switch(strategy)
            {
                
                case ActorMovementStrategy.Directed :

                    if (!agent.hasPath)
                    {
                        if (!pathChecked)
                        {
                            pathChecked = true;
                            agent.enabled = false;
                            //Invoke("ObstacleOn", 0.1f);
                            obstacle.enabled = true;
                            //state = ActorNavigationState.Idle;
                        }
                    }

                    if (destination != oldDestination)
                    {
                        if (agent.enabled)
                            agent.destination = destination;
                    }

                    break;



                default :

                    if (Vector3.Distance(destination, agent.transform.position) <= agent.stoppingDistance + 1.0f) // bool isChasing = true;
                    {
                        if (!pathChecked)
                        {

                            if( agent.enabled )
                            {
                                pathChecked = true;
                                agent.enabled = false;
                                obstacle.enabled = true;
                            }
                        }
                    }

                    break;
            }
        }

        public void OnConfigure( float Speed, float Aceeleration, float AngularSpeed )
        {
            this.speed = Speed;
            this.acceleration = Aceeleration;
            this.angularSpeed = AngularSpeed;

            obstacle.enabled = false;

            if( !obstacle.isActiveAndEnabled )
            {
                agent.enabled = true;

                agent.ResetPath();

                agent.speed = speed;
                agent.acceleration = acceleration;
                agent.angularSpeed = angularSpeed;

                agent.enabled = false;
                obstacle.enabled = true;
            }

        }


        public void SetDestination( Vector3 newDestination )
        {
            switch(strategy)
            {
                case ActorMovementStrategy.Directed :

                    destination = newDestination;
                    obstacle.enabled = false;

                    if (!obstacle.isActiveAndEnabled)
                    {
                        agent.enabled = true;
                        agent.destination = destination;
                        pathChecked = false;
                    }

                    break;




                default :
                    
                    if (newDestination != destination)
                    {
                        dist = Vector3.Distance(agent.transform.position, destination);
                        targetDist = Vector3.Distance(agent.transform.position, newDestination);

                        if (targetDist > dist)
                            destination = newDestination;

                        if (targetDist > agent.stoppingDistance && dist > agent.stoppingDistance)
                        {
                            
                            if (obstacle.enabled)
                            {

                                obstacle.enabled = false;

                                if (!obstacle.isActiveAndEnabled)
                                {
                                    agent.enabled = true;
                                    agent.ResetPath();

                                    agent.destination = destination;
                                    oldDestination = destination;

                                    pathChecked = false;
                                }
                            }
                            else if (agent.enabled)
                            {
                                
                                agent.destination = destination;
                                //state = ActorNavigationState.Moving;
                            }
                        }
                        oldDestination = destination;
                    }

                    break;
            }

            
        }
    }
}
