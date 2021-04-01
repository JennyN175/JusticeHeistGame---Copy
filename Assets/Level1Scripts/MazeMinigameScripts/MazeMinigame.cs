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

        computerScriptGetter = GameObject.Find("Computer");
        computerScript = computerScriptGetter.GetComponent<computer>();

        mazeGame = GameObject.Find("MazeGame");
        mazeMap = GameObject.Find("MazeGameMap");
        mazePlayer = GameObject.Find("mazePlayer");
        timer = GameObject.Find("timer").GetComponent<Slider>();
        mainPlayer = GameObject.Find("testPlayer");
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
            if (!computerScript.hasBeenCollected) //Testing minigame with the computer for now
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
        mazeMap.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.16f, 0.34f, 1f);
        mainPlayer.GetComponent<Rigidbody2D>().simulated = false;
        mazePlayer.GetComponent<Rigidbody2D>().simulated = true;
        star.GetComponent<Rigidbody2D>().simulated = true;
        for (int i = 0; i < mazeWalls.Length; i++)
        {
            print(mazeWalls.Length);
            mazeWalls[i].GetComponent<SpriteRenderer>().color = new Color(0f, 0.67f, 0.55f, 1f);
            mazeWalls[i].GetComponent<Rigidbody2D>().simulated = true;
        }

        for(int i = 0; i < rigidbodyObjects.Length; i++)
        {
            rigidbodyObjects[i].GetComponent<Rigidbody2D>().simulated = false;
        }

        timer.gameObject.SetActive(true);
        timer.value -= Time.deltaTime / (float)countDownTime;

        if (timer.value == 0)
        {
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
        for (int i = 0; i < mazeWalls.Length; i++)
        {
            mazeWalls[i].GetComponent<SpriteRenderer>().color = new Color(0f, 0.67f, 0.55f, 0f);
            mazeWalls[i].GetComponent<Rigidbody2D>().simulated = false;
        }

        for (int i = 0; i < rigidbodyObjects.Length; i++)
        {
            rigidbodyObjects[i].GetComponent<Rigidbody2D>().simulated = true;
        }

        timer.value = (float)countDownTime;
        timer.gameObject.SetActive(false);
    }
}
