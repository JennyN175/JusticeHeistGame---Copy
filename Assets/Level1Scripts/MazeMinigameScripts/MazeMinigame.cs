using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeMinigame : MonoBehaviour
{
    computer computerScript;
    GameObject computerScriptGetter;
    GameObject mazeGame;
    GameObject mazeMap;
    GameObject[] mazeWalls;
    GameObject[] rigidbodyObjects;
    GameObject mainPlayer;
    player playerScript;
    GameObject mazePlayer;
    GameObject star;
    public Slider timer;
    public double countDownTime = 4;

    public bool mazeGameOngoing = false;
    public bool endGame = false;
    public bool gameOngoing = false;
    //bool startCountdown = false;

    public Transform player;

    float distanceFromCamera = -2f;

    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "scene2")
        {
            computerScriptGetter = GameObject.Find("Computer");
            computerScript = computerScriptGetter.GetComponent<computer>();
        }

        mazeGame = GameObject.Find("MazeGame");
        mazeMap = GameObject.Find("MazeGameMap");
        mazePlayer = GameObject.Find("mazePlayer");
        timer = GameObject.Find("timer").GetComponent<Slider>();
        mainPlayer = GameObject.Find("testPlayer");
        playerScript = mainPlayer.GetComponent<player>();
        star = GameObject.Find("mazeStar");
        mazeWalls = GameObject.FindGameObjectsWithTag("MazeWalls");
        rigidbodyObjects = GameObject.FindGameObjectsWithTag("disableDuringMinigame");
        HideMazeMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName == "scene2")
        {
            if (!computerScript.hasBeenCollected) 
            {
                endGame = false;
                mazeGameOngoing = false;
            }

            if (mazeGameOngoing)
            {
                ShowMazeMinigame();
            }

            if (!mazeGameOngoing)
            {
                HideMazeMinigame();
            }

            if (endGame)
            {
                mazeGameOngoing = false;
                HideMazeMinigame();
            }
        }

    }

    void ShowMazeMinigame()
    {
        mazeGame.transform.position = new Vector3(player.position.x, player.position.y, distanceFromCamera);
        star.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        mazeMap.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        mainPlayer.GetComponent<Rigidbody2D>().simulated = false;
        mazePlayer.GetComponent<Rigidbody2D>().simulated = true;
        star.GetComponent<Rigidbody2D>().simulated = true;

        //Show maze and enable rigidbodies
        for (int i = 0; i < mazeWalls.Length; i++)
        {
            mazeWalls[i].GetComponent<SpriteRenderer>().color = new Color(0.1887979f, 0.7930654f, 0.8113208f, 1f);
            mazeWalls[i].GetComponent<Rigidbody2D>().simulated = true;
        }

        //Disable rigidbodies of the main level so that nothing interferes with the minigame
        for(int i = 0; i < rigidbodyObjects.Length; i++)
        {
            rigidbodyObjects[i].GetComponent<Rigidbody2D>().simulated = false;
        }

        timer.gameObject.SetActive(true);
        timer.value -= Time.deltaTime / (float)countDownTime;

        if (timer.value == 0)
        {
            //If timer runs out, take back the point
            playerScript.codeCounter--;
            mazeGameOngoing = false;
        }

        gameOngoing = true;
    }

    void HideMazeMinigame()
    {
        mazeMap.GetComponent<SpriteRenderer>().color = new Color(0f, 0.08f, 0.3f, 0f);
        star.GetComponent<SpriteRenderer>().color = new Color(0f, 0.08f, 0.3f, 0f);
        mainPlayer.GetComponent<Rigidbody2D>().simulated = true;
        mazePlayer.GetComponent<Rigidbody2D>().simulated = false;
        star.GetComponent<Rigidbody2D>().simulated = false;

        //Hide and disable minigame rigidbodies
        for (int i = 0; i < mazeWalls.Length; i++)
        {
            mazeWalls[i].GetComponent<SpriteRenderer>().color = new Color(0f, 0.67f, 0.55f, 0f);
            mazeWalls[i].GetComponent<Rigidbody2D>().simulated = false;
        }

        //Enable rigidbodies of the main level
        for (int i = 0; i < rigidbodyObjects.Length; i++)
        {
            rigidbodyObjects[i].GetComponent<Rigidbody2D>().simulated = true;
        }

        timer.value = (float)countDownTime;
        timer.gameObject.SetActive(false);
    }
}
