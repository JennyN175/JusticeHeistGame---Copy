using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch2 : MonoBehaviour
{
    GameObject laserPromptSprite2;
    bool hasCollided;
    GameObject thePlayer;
    player playerScript;
    //CameraFOV cameraFOVScript;
    GameObject[] laserSet2;
    Laser[] laserSet2Scripts;

    bool turnedOff = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            laserPromptSprite2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasCollided = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            laserPromptSprite2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            hasCollided = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        laserSet2Scripts = new Laser[7];

        laserPromptSprite2 = GameObject.Find("laserSwitchPrompt2");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();
        //cameraFOVScript = GameObject.Find("cameraViewPoint").GetComponent<CameraFOV>();

        laserSet2 = GameObject.FindGameObjectsWithTag("Lasers2");
        for (int i = 0; i < laserSet2.Length; i++)
        {
            laserSet2Scripts[i] = laserSet2[i].GetComponent<Laser>();
        }

        laserPromptSprite2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
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
            for (int i = 0; i < laserSet2.Length; i++)
            {
                laserSet2Scripts[i].laserSwitchOff = true;

            }

            laserPromptSprite2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
