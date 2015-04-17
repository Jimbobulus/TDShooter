using UnityEngine;
using System.Collections;

namespace com.gamehound.broops.viewmodel
{
    public class ActorLocomotion : MonoBehaviour
    {
        public Transform graphics;
        public Transform navAgent;

        public float followSpeed = 5.0f;
        public float lookSpeed = 8.0f;

        protected Vector3 lastPosition = Vector3.zero;

        protected Vector3 orientation;

        void OnDisable()
        {
            if (!ComponentsNull()) return;

            graphics.position = navAgent.position;
        }

        void Start()
        {
            if (!ComponentsNull())
            {
                print("one of your components are not set, " + transform.name + " is on strike");
            }

        }

        public virtual void Update()
        {
            if (!ComponentsNull()) return;

            graphics.position = Vector3.Lerp(graphics.position, navAgent.position, Time.deltaTime * followSpeed);

           
        }

        bool ComponentsNull()
        {
            if (graphics == null) return false;
            if (navAgent == null) return false;

            return true;
        }
    }
}