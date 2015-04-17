using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindClosestByTag : MonoBehaviour 
{
    public string tagName = "Enemy";

    public List<Transform> targetsInRange;

    private Vector3 closestTarget;
    public Vector3 ClosestTarget
    {
        get
        {
            return closestTarget;
        }
    }

    private bool hasTarget = false;
    public bool HasTarget
    {
        get
        {
            return hasTarget;
        }
    }

    private Transform mTransform;

	void Start () 
    {
        mTransform = transform;
	}

    void Update()
    {
        float targetDist = 10.0f;

        if (targetsInRange.Count == 1)
        {
            hasTarget = true;

            if (Object.Equals(closestTarget, targetsInRange[0]))
                closestTarget = targetsInRange[0].position;
        }
        else if (targetsInRange.Count > 0)
        {
            hasTarget = true;

            foreach (Transform enemy in targetsInRange)
            {
                float t = Vector3.Distance(transform.position, enemy.position);
                if (t < targetDist)
                {
                    closestTarget = enemy.position;
                }
            }
        }
        else
        {
            hasTarget = false;
        }
    }
	
	void OnTriggerEnter( Collider other ) 
    {
        if (other.CompareTag(tagName))
        {
            //print("Found: " + other.name);
            if (!targetsInRange.Contains(other.transform))
            {
                //print("Added: "+other.transform.parent.name);
                targetsInRange.Add(other.transform);
            }
        }
         
	}

    void OnTriggerExit( Collider other )
    {
        if(other.CompareTag(tagName))
        {
            //print("Lost: " + other.name);
            if (targetsInRange.Contains(other.transform))
            {
                //print("removing: " + other.transform.parent.name);
                targetsInRange.Remove(other.transform);
            }
        }
    }
}
