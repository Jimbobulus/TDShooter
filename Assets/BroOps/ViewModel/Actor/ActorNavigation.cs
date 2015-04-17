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
        Transform agentTransform;

        public NavMeshObstacle obstacle;
        public ActorMovementStrategy strategy;

        public ActorNavigationState state;

        [HideInInspector]
        public Vector3 spawnPoint = Vector3.zero;

        [HideInInspector]
        private float speed = 0.0f;
        public float Speed
        {
            get
            {
                return agent.speed;
            }
            set
            {
                agent.speed = value;
            }
        }


         [HideInInspector]
        public float acceleration = 0.0f;

         [HideInInspector]
        public float angularSpeed = 0.0f;

        [HideInInspector]
        public Vector3 destination = Vector3.zero;

        public float pauseBeforeMoveTime = 0.5f;


        protected float distanceToTarget = 0.0f;
        public float DistanceToTarget
        {
            get
            {
                return distanceToTarget;
            }
        }
        
        

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
            if (agent.transform != transform)
                agentTransform = agent.transform;
        }


        public virtual void Update()
        {
            // component collision check - NEVER CROSS THE STREAMS!!!!
            if (agent.enabled && obstacle.enabled)
                obstacle.enabled = false;

            distanceToTarget = Vector3.Distance(destination, agentTransform.position);

        }

        public virtual void SetDestination(Vector3 newDestination) { }

        public virtual void StopMoving() {
            state = ActorNavigationState.Idle;
        }

        public virtual void StartMoving()
        {
            obstacle.enabled = false;

            if (!obstacle.isActiveAndEnabled)
            {
                agent.enabled = true;

                if (agent.isActiveAndEnabled)
                {
                    agent.destination = destination;
                    state = ActorNavigationState.Moving;
                }
            }
        }



        public void OnConfigure( float Speed, float Aceeleration, float AngularSpeed )
        {
            this.speed = Speed;
            this.acceleration = Aceeleration;
            this.angularSpeed = AngularSpeed;

            obstacle.enabled = false;

            if (!obstacle.isActiveAndEnabled)
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

        public void OnConfigure()
        {
            agent.enabled = false;

            if (!agent.isActiveAndEnabled)
                obstacle.enabled = true;
        }

        protected virtual void OnIdle()
        {
            CancelInvoke("OnIdle");
        }

        
    }
}
