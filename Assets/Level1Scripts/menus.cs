using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menus : MonoBehaviour
{
    public float starttime;

    Image mainMenu, bgImage, playButtonImg, gameOverMenu, restartButtonImg, winMenu, replayButtonImg;
    Button playButton, restartButton, replayButton;
    public bool gameStarted = false;
    public bool gameRestarted = false;

    GameObject thePlayer;
    player playerScript;

    FieldOfView fovScript, fovScript2, fovScript3, fovScript4;
    GameObject fovScriptGetter, fovScriptGetter2, fovScriptGetter3, fovScriptGetter4;

    string sceneName;

    //public AudioSource caughtAudio, winAudio;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();

        //Get game objects associated with main menu
        playButton = GameObject.Find("playButton").GetComponent<Button>();
        playButtonImg = GameObject.Find("playButton").GetComponent<Image>();
        mainMenu = GameObject.Find("mainMenu").GetComponent<Image>();
        bgImage = GameObject.Find("background").GetComponent<Image>();

        //Get game objects associated with win menu
        winMenu = GameObject.Find("winMenu").GetComponent<Image>();
        replayButton = GameObject.Find("replayButton").GetComponent<Button>();
        replayButtonImg = GameObject.Find("replayButton").GetComponent<Image>();

        //Get game objects associated with game over menu
        gameOverMenu = GameObject.Find("gameOver").GetComponent<Image>();
        restartButton = GameObject.Find("restartButton").GetComponent<Button>();
        restartButtonImg = GameObject.Find("restartButton").GetComponent<Image>();

        //Get all guards' field of view scripts in order to access their variables (specifically the lostGame boolean)
        if (sceneName == "scene1")
        {
            GetFovOfAllGuardsLevel1();
        }

        if (sceneName == "scene2")
        {
            GetFovOfAllGuards();
        }

        //Add button listeners
        playButton.onClick.AddListener(playClicked);
        restartButton.onClick.AddListener(restartClicked);
        replayButton.onClick.AddListener(replayClicked);

        //On starting, hide the game over menu and win menu
        HideGameOverMenu();
        HideWinMenu();
        bgImage.enabled = true;
    }

    public void playClicked()
    {
        print("play clicked!");
        gameStarted = true;
    }

    public void restartClicked()
    {
        print("restart clicked!");
        gameRestarted = true;
    }

    public void replayClicked()
    {
        print("replay clicked!");
        gameRestarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If the player clicks the play button, hide the start menu
        if (gameStarted)
        {
            HideStartMenu();
        }

        //If the player restarts the game after losing, hide game over menu and reset the game
        if (sceneName == "scene1")
        {
            if (gameRestarted)
            {
                HideGameOverMenu();
                HideWinMenu();
                playerScript.codeCounter = 0;
                fovScript.lostGame = false;
                fovScript3.lostGame = false;
                gameRestarted = false;
            }
        }

        if (sceneName == "scene2")
        {
            if (gameRestarted)
            {
                HideGameOverMenu();
                HideWinMenu();
                SceneManager.LoadScene("scene1");
                playerScript.codeCounter = 0;
                fovScript.lostGame = false;
                fovScript2.lostGame = false;
                fovScript3.lostGame = false;
                fovScript4.lostGame = false;
                gameRestarted = false;
            }
        }

        //If the player walks into any of the guards' field of views, show the gameover menu
        if (sceneName == "scene1")
        {
            if (fovScript.lostGame || fovScript3.lostGame)
            {
                ShowGameOverMenu();
            }
        }

        if (sceneName == "scene2")
        {
            if (fovScript.lostGame || fovScript2.lostGame || fovScript3.lostGame || fovScript4.lostGame)
            {
                ShowGameOverMenu();
            }
        }

        if (sceneName == "scene2")
        {
            //If the player gets all 3 computer codes, show the win menu
            if(playerScript.codeCounter == 3)
            {
                ShowWinMenu();
            }
        }

        if (sceneName == "scene2")
        {
            HideStartMenu();
        }
    }

    void HideGameOverMenu()
    {
        restartButton.enabled = false;
        restartButtonImg.enabled = false;
        gameOverMenu.enabled = false;
        bgImage.enabled = false;
    }

    void ShowGameOverMenu()
    {
        restartButton.enabled = true;
        restartButtonImg.enabled = true;
        gameOverMenu.enabled = true;
        bgImage.enabled = true;
    }

    void HideStartMenu()
    {
        playButton.enabled = false;
        playButtonImg.enabled = false;
        mainMenu.enabled = false;
        bgImage.enabled = false;
    }

    void ShowWinMenu()
    {
        replayButton.enabled = true;
        replayButtonImg.enabled = true;
        winMenu.enabled = true;
        bgImage.enabled = true;
    }
    
    void HideWinMenu()
    {
        replayButton.enabled = false;
        replayButtonImg.enabled = false;
        bgImage.enabled = false;
        winMenu.enabled = false;
    }

    void GetFovOfAllGuards()
    {
        fovScriptGetter = GameObject.Find("pivotviewpoint");
        fovScriptGetter2 = GameObject.Find("pivotviewpoint2");
        fovScriptGetter3 = GameObject.Find("pivotviewpoint (1)");
        fovScriptGetter4 = GameObject.Find("pivotviewpoint (2)");

        fovScript = fovScriptGetter.GetComponent<FieldOfView>();
        fovScript2 = fovScriptGetter2.GetComponent<FieldOfView>();
        fovScript3 = fovScriptGetter3.GetComponent<FieldOfView>();
        fovScript4 = fovScriptGetter4.GetComponent<FieldOfView>();
    }

    void GetFovOfAllGuardsLevel1()
    {
        fovScriptGetter = GameObject.Find("pivotviewpoint");
        fovScriptGetter3 = GameObject.Find("pivotviewpoint (1)");

        fovScript = fovScriptGetter.GetComponent<FieldOfView>();
        fovScript3 = fovScriptGetter3.GetComponent<FieldOfView>();
    }
}
