using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer line;
    public Transform lineEndPoint;
    public int onTime = 2;
    public int offTime = 2;
    public bool touchedLaser = false;
    public bool laserSwitchOff = false;
    bool laserActive;
    RaycastHit2D raycastHit;

    //public Transform startLinePoint;


    // Start is called before the first frame update
    void Start()
    {
        laserActive = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (laserSwitchOff == false)
        {
            if (laserActive)
            {
                StartCoroutine("TurnOnLaser");
            }
            else
            {
                StartCoroutine("TurnOffLaser");
            }
        }

        if (laserSwitchOff)
        {
            line.enabled = false;
            raycastHit = Physics2D.Raycast(transform.position, transform.position - transform.position, 100, 1 << 9);
        }
        
    }

    IEnumerator TurnOnLaser()
    {
        line.enabled = true;
        line.SetPosition(0, transform.position);

        raycastHit = Physics2D.Raycast(transform.position, lineEndPoint.position - transform.position, 100, 1 << 9);
        if (raycastHit == GameObject.FindGameObjectWithTag("Player"))
        {
            line.SetPosition(1, raycastHit.point);
            touchedLaser = true;
        }
        else
        {
            line.SetPosition(1, lineEndPoint.position);
        }

        yield return new WaitForSeconds(onTime);
        laserActive = false;
    }

    IEnumerator TurnOffLaser()
    {
        line.enabled = false;
        raycastHit = Physics2D.Raycast(transform.position, transform.position-transform.position, 100, 1<<9);

        yield return new WaitForSeconds(offTime);
        laserActive = true;
    }
}
