using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class rope : MonoBehaviour
{ 
    public Transform bob;
    public Transform target1;
    private Transform curTarget;
    LineRenderer lr;
    private bool isTarget1 = true;
    public bool hasParent;
    public Transform[] closest_tether;

    // Use this for initialization
    void Start()
    {

        lr = GetComponent<LineRenderer>();
        if (hasParent)
        {
            lr.SetPosition(1, transform.InverseTransformPoint(bob.position));
        }
        else
        {
            lr.SetPosition(1, bob.position);
        }

        curTarget = target1;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindClosestTether();     
    }
    void FindClosestTether()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in closest_tether)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            lr.enabled = true;
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            lr.enabled = false;
        }
        if (hasParent)
        {
            lr.SetPosition(0, transform.InverseTransformPoint(bestTarget.position));
        }
        else
        {
            lr.SetPosition(0, bestTarget.position);
        }
       

        lr = GetComponent<LineRenderer>();
        if (hasParent)
        {
            lr.SetPosition(1, transform.InverseTransformPoint(bob.position));
        }
        else
        {
            lr.SetPosition(1, bob.position);
        }

    }
}
