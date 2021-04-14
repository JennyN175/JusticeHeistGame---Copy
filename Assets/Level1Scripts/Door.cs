using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    GameObject doorPromptSprite;
    Image passScreen;
    bool hasCollided;
    GameObject thePlayer;
    player playerScript;
    menus menuScript;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Collided");
            doorPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasCollided = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            doorPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            hasCollided = false;
        }
    }

    IEnumerator ShowWinScreen()
    {
        passScreen.enabled = true;
        menuScript.cutsceneAudioSource.clip = menuScript.cutsceneAudio[7];
        menuScript.cutsceneAudioSource.Play();
        yield return new WaitForSeconds(menuScript.cutsceneAudioSource.clip.length);
        SceneManager.LoadScene("scene2");
        DestroyAllGameObjects();
    }

    // Start is called before the first frame update
    void Start()
    {
        doorPromptSprite = GameObject.Find("doorPromptSprite");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();
        passScreen = GameObject.Find("passScreen").GetComponent<Image>();
        menuScript = GameObject.Find("playButton").GetComponent<menus>();

        doorPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        passScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided && Input.GetKeyDown(KeyCode.E) && playerScript.hasWon)
        {
            print("Enter door");
            StartCoroutine("ShowWinScreen");
            //DestroyAllGameObjects();
            
            //SceneManager.LoadScene("scene2");
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
