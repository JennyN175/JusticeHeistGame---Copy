using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class guard : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypointID = 0;
    enum EnemyState { Patrol, Pursue };
    EnemyState currentState = EnemyState.Patrol;
    Path path;
    bool reachedEndOfPath = false;
    float nextWaypointDistance = 1f;
    float distance;
    private Vector3 previousPos;

    GameObject waypoint;
    GameObject viewpoint;

    // Start is called before the first frame update
    void Start()
    {
        waypoint = GameObject.Find("waypointMarker");
        viewpoint = GameObject.Find("pivotviewpoint");

    }

    private void RotateTowardsTarget()
    {
        float rotationSpeed = 10f;
        float offset = 90f;
        Vector3 direction = -(waypoint.transform.position - transform.position);
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
        viewpoint.transform.rotation = Quaternion.Slerp(viewpoint.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        RotateTowardsTarget();
        
        distance = Vector2.Distance(GetComponent<Rigidbody2D>().position, waypoint.transform.position);
        
        if (distance < nextWaypointDistance)
        {
            if (currentWaypointID <= waypoints.Length)
            {
                currentWaypointID++;
            }
            else
            {
                currentWaypointID = 0;
            }
            waypoint.transform.position = waypoints[currentWaypointID].transform.position; 
        }

    }
}
