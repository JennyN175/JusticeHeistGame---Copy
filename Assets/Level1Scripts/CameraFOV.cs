using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{

    public bool lostGame = false;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    Vector3 directionToTarget;
    float distanceToTarget;
    Transform target;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDistanceThreshold;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine("FindTargetWithDelay", 0.2f);
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void LateUpdate()
    {
        CreateFieldOfView();
    }

    //Finds targets inside field of view not blocked by walls
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        //Adds targets in view radius to an array
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        //For every targetsInViewRadius, check if they are inside the field of view
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            target = targetsInViewRadius[i].transform;
            directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, directionToTarget) < viewAngle / 2)
            {
                distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (PlayerFound())
                {
                    lostGame = true;
                }
            }
        }
    }

    bool PlayerFound()
    {
        //If line draw from object to target is not interrupted by wall, add target to list of visible 
        //targets
        if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
        {
            print("target found");
            visibleTargets.Add(target);
            return true;
        }
        else
        {
            return false;
        }
    }


    void CreateFieldOfView()
    {
        int numRays = Mathf.RoundToInt(viewAngle * meshResolution);
        float angleBetweenRays = viewAngle / numRays;
        List<Vector3> collisionPoints = new List<Vector3>();
        Vector3 oldPoint = new Vector3();

        //If raycast hits an obstacle, obscure viewpoint mesh behind it by only using new vertices (guard's position to the wall's position) to generate appropriate mesh
        for (int i = 0; i <= numRays; i++)
        {
            float angle = -(transform.eulerAngles.z) - viewAngle / 2 + angleBetweenRays * i;

            Vector3 newPoint = ViewCast(angle);

            collisionPoints.Add(newPoint);
            oldPoint = newPoint;
        }

        //Constructing mesh using triangles (3 triangles used)
        int vertexCount = collisionPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        //First vertice set to guard's position
        vertices[0] = Vector3.zero;

        //Setting vertices of each triangle, one after the other
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(collisionPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        //Create mesh using the new vertices 
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    Vector3 ViewCast(float globalAngle)
    {
        Vector3 direction = DirectionFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

        //Check if raycast hit an obstacle, if it did, return the point of collision with the obstacle
        if (Physics2D.Raycast(transform.position, direction, viewRadius, obstacleMask))
        {
            return hit.point;
        }
        else
        {
            //No collision with obstacle, return position of raycast end point
            return transform.position + direction * viewRadius;
        }
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
