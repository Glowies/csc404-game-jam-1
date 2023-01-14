using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex; // Index to choose waypoints
    Vector3 target;

    public Transform centrePoint;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        //if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        //{
        //    Vector3 point;
        //    if (GetRandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
        //    {
        //        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
        //        agent.SetDestination(point);
        //    }
        //}


        if (Vector3.Distance(transform.position, target) < 1)
        {   // If distance to waypoint is less than 1m, update waypoint
            iterateWaypointIndex();
            UpdateDestination();
        }
    }

    //bool GetRandomPoint(Vector3 center, float range, out Vector3 result)
    //{
    //    Vector3 newPoint = center + Random.insideUnitSphere * range;

    //    NavMeshHit hit;

    //    if (NavMesh.SamplePosition(newPoint, out hit, 1.0f, NavMesh.AllAreas))
    //    {
    //        result = hit.position;
    //        return true;
    //    }

    //    result = Vector3.zero;
    //    return false;
    //}

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position; // get position of current waypoint
        agent.SetDestination(target); // Set NavMesh agent to target position

    }

    void iterateWaypointIndex()
    {
        waypointIndex++;

        if(waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
