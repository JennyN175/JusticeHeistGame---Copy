using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch : MonoBehaviour
{
    GameObject laserPromptSprite;
    bool hasCollided;
    GameObject thePlayer;
    player playerScript;
    //CameraFOV cameraFOVScript;
    GameObject[] laserSet1;
    Laser[] laserSet1Scripts;

    bool turnedOff = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            laserPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasCollided = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            laserPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            hasCollided = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        laserSet1Scripts = new Laser[7];

        laserPromptSprite = GameObject.Find("laserSwitchPrompt");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();
        //cameraFOVScript = GameObject.Find("cameraViewPoint").GetComponent<CameraFOV>();

        laserSet1 = GameObject.FindGameObjectsWithTag("Lasers1");
        for (int i = 0; i < laserSet1.Length; i++)
        {
            laserSet1Scripts[i] = laserSet1[i].GetComponent<Laser>();
        }

        laserPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided && Input.GetKeyDown(KeyCode.E) && !turnedOff)
        {
            turnedOff = true;
        }

        if (turnedOff)
        {
            for (int i = 0; i < laserSet1.Length; i++)
            {
                laserSet1Scripts[i].laserSwitchOff = true;
                
            }

            laserPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
