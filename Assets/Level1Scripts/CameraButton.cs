using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraButton : MonoBehaviour
{
    GameObject cameraPromptSprite;
    bool hasCollided;
    GameObject thePlayer;
    player playerScript;
    CameraFOV cameraFOVScript;

    bool turnedOff = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            cameraPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasCollided = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            cameraPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            hasCollided = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraPromptSprite = GameObject.Find("camera_prompt");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();
        cameraFOVScript = GameObject.Find("cameraViewPoint").GetComponent<CameraFOV>();

        cameraPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided && Input.GetKeyDown(KeyCode.E) && !turnedOff)
        {
            cameraFOVScript.viewRadius = 0;
            turnedOff = true;
        }

        if (turnedOff)
        {
            cameraPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
