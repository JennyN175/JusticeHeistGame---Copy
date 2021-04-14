using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazePlayer : MonoBehaviour
{
    GameObject player, mazeScriptGetter, playerSprite;
    MazeMinigame mazeScript;
    Vector3 pos;
    float speed = 0.006f;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        player = GameObject.Find("mazePlayer");
        playerSprite = GameObject.Find("mazePlayerSprite");
        mazeScriptGetter = GameObject.Find("MazeGame");
        mazeScript = mazeScriptGetter.GetComponent<MazeMinigame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName == "scene1")
        {
            playerSprite.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        }

        if (sceneName == "scene2" || sceneName == "scene3")
        {
            pos = transform.localPosition;
            if (!mazeScript.mazeGameOngoing)
            {
                transform.localPosition = new Vector3(0.202f, 0.27f, -0.4f);
                player.GetComponent<Rigidbody2D>().simulated = false;
                playerSprite.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
            }

            if (mazeScript.mazeGameOngoing)
            {
                player.GetComponent<Rigidbody2D>().simulated = true;
                playerSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0.9411916f, 0f, 1f);

                PlayerMove();
            }
        }
    }


    void PlayerMove()
    {
        if (Input.GetKey("w"))
        {
            pos += Vector3.up * speed; 
        }
            
        if (Input.GetKey("s"))
        {
            pos += Vector3.down * speed;
        }
        if (Input.GetKey("d"))
        {
            pos += Vector3.right * speed;
        }
        if (Input.GetKey("a"))
        {
            pos += Vector3.left * speed;
        }

        player.transform.localPosition = new Vector3(pos.x, pos.y, -0.4f);
    }
}
