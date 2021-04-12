using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    GameObject elevatorPromptSprite;
    bool hasCollided;
    GameObject thePlayer;
    player playerScript;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            elevatorPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasCollided = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            elevatorPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            hasCollided = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        elevatorPromptSprite = GameObject.Find("elevatorPromptSprite");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();

        elevatorPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided && Input.GetKeyDown(KeyCode.E) && playerScript.hasWon)
        {
            print("Enter elevator");
            DestroyAllGameObjects();
            SceneManager.LoadScene("scene3");
        }
    }

    public void DestroyAllGameObjects()
    {
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }
    }
}
