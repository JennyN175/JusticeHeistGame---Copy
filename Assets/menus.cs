using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menus : MonoBehaviour
{
    Image mainMenu, bgImage, playButtonImg, gameOverMenu, restartButtonImg, winMenu, replayButtonImg;
    Button playButton, restartButton, replayButton;
    bool gameStarted = false;
    public bool gameRestarted = false;

    GameObject thePlayer;
    player playerScript;

    FieldOfView fovScript, fovScript2, fovScript3, fovScript4;
    GameObject fovScriptGetter, fovScriptGetter2, fovScriptGetter3, fovScriptGetter4;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();

        playButton = GameObject.Find("playButton").GetComponent<Button>();
        playButtonImg = GameObject.Find("playButton").GetComponent<Image>();
        mainMenu = GameObject.Find("mainMenu").GetComponent<Image>();
        bgImage = GameObject.Find("background").GetComponent<Image>();

        winMenu = GameObject.Find("winMenu").GetComponent<Image>();
        replayButton = GameObject.Find("replayButton").GetComponent<Button>();
        replayButtonImg = GameObject.Find("replayButton").GetComponent<Image>();

        gameOverMenu = GameObject.Find("gameOver").GetComponent<Image>();
        restartButton = GameObject.Find("restartButton").GetComponent<Button>();
        restartButtonImg = GameObject.Find("restartButton").GetComponent<Image>();

        GetFovOfAllGuards();

        playButton.onClick.AddListener(playClicked);
        restartButton.onClick.AddListener(restartClicked);
        replayButton.onClick.AddListener(replayClicked);

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
        if (gameStarted)
        {
            HideStartMenu();
        }

        if (gameRestarted)
        {
            HideGameOverMenu();
            HideWinMenu();
            playerScript.codeCounter = 0;
            fovScript.lostGame = false;
            fovScript2.lostGame = false;
            fovScript3.lostGame = false;
            fovScript4.lostGame = false;
            gameRestarted = false;
        }

        if (fovScript.lostGame || fovScript2.lostGame || fovScript3.lostGame || fovScript4.lostGame)
        {
            ShowGameOverMenu();
        }

        if(playerScript.codeCounter == 3)
        {
            ShowWinMenu();
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
}
