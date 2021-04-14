using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
    GameObject elevatorPromptSprite;
    bool hasCollided;
    GameObject thePlayer;
    player playerScript;
    menus menuScript;
    Image passScreen;

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

    IEnumerator ShowWinScreen()
    {
        passScreen.enabled = true;
        menuScript.cutsceneAudioSource.clip = menuScript.cutsceneAudio[1];
        menuScript.cutsceneAudioSource.Play();
        yield return new WaitForSeconds(menuScript.cutsceneAudioSource.clip.length);
        SceneManager.LoadScene("scene3");
        DestroyAllGameObjects();
    }

    // Start is called before the first frame update
    void Start()
    {
        elevatorPromptSprite = GameObject.Find("elevatorPromptSprite");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();
        menuScript = GameObject.Find("playButton").GetComponent<menus>();
        passScreen = GameObject.Find("passScreen").GetComponent<Image>();

        elevatorPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        passScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided && Input.GetKeyDown(KeyCode.E) && playerScript.hasWon)
        {
            print("Enter elevator");
            StartCoroutine("ShowWinScreen");
            //DestroyAllGameObjects();
            //SceneManager.LoadScene("scene3");
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
