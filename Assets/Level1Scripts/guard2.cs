using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class guard2 : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypointID = 0;
    Path path;
    float nextWaypointDistance = 1f;
    float distance;
    private Vector3 previousPos;

    GameObject waypoint;
    GameObject viewpoint;

    MazeMinigame mazeScript;
    GameObject mazeScriptGetter;

    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        waypoint = GameObject.Find("waypointMarker2");
        viewpoint = GameObject.Find("pivotviewpoint2");

        mazeScriptGetter = GameObject.Find("MazeGame");
        mazeScript = mazeScriptGetter.GetComponent<MazeMinigame>();
    }

    private void RotateTowardsTarget()
    {
        float rotationSpeed = 1f;
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

        //Minigames only appear in the second level
        if (sceneName == "scene2")
        {
            if (!mazeScript.mazeGameOngoing)
            {
                GuardMove();
            }
            else
            {
                //If minigame is ongoing, pause movement of guards
                waypoint.transform.position = GetComponent<Rigidbody2D>().position;
            }
        }

        if (sceneName == "scene1")
        {
            GuardMove();
        }
    }

    void GuardMove()
    {
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
