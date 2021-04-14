using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menus : MonoBehaviour
{
    public float starttime;

    public Image cutscene;
    public Sprite cutscene1, cutscene2, cutscene3, cutscene4, cutscene5, cutscene6;
    public AudioClip[] cutsceneAudio;
    public AudioSource cutsceneAudioSource;
    public AudioSource winAudio;

    Image mainMenu, bgImage, playButtonImg, gameOverMenu, restartButtonImg, winMenu, replayButtonImg;
    Button playButton, restartButton, replayButton;
    public bool gameStarted = false;
    public bool gameRestarted = false;
    public bool cutsceneOngoing = true;
    static bool level2AudioPlayed = false;
    static bool level3AudioPlayed = false;
    //bool endCutscene = false;

    GameObject thePlayer;
    player playerScript;

    FieldOfView fovScript, fovScript2, fovScript3, fovScript4;
    GameObject fovScriptGetter, fovScriptGetter2, fovScriptGetter3, fovScriptGetter4;
    CameraFOV cameraFOV, cameraFOV2, cameraFOV3;
    Laser laserScript;
    VaultCode vaultCode;

    string sceneName;

    GameObject[] laserSet1;
    Laser[] laserSet1Scripts;
    GameObject[] laserSet2;
    Laser[] laserSet2Scripts;

    //public AudioSource caughtAudio, winAudio;
    // Start is called before the first frame update
    void Start()
    {
        cutscene.enabled = false;
        
        laserSet1Scripts = new Laser[7];
        laserSet2Scripts = new Laser[7];

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();

        //Get game objects associated with main menu
        playButton = GameObject.Find("playButton").GetComponent<Button>();
        playButtonImg = GameObject.Find("playButton").GetComponent<Image>();
        mainMenu = GameObject.Find("mainMenu").GetComponent<Image>();
        bgImage = GameObject.Find("cutscene1").GetComponent<Image>();

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
            if (!level2AudioPlayed)
            {
                cutsceneAudioSource.clip = cutsceneAudio[0];
                cutsceneAudioSource.Play();
                level2AudioPlayed = true;
            }

            GetFovOfAllGuards();
        }

        if (sceneName == "scene3")
        {
            if (!level3AudioPlayed)
            {
                cutsceneAudioSource.clip = cutsceneAudio[0];
                cutsceneAudioSource.Play();
                level3AudioPlayed = true;
            }

            vaultCode = GameObject.Find("Vault").GetComponent<VaultCode>();
            laserSet1 = GameObject.FindGameObjectsWithTag("Lasers1");
            laserSet2 = GameObject.FindGameObjectsWithTag("Lasers2");
            for (int i = 0; i < laserSet1.Length; i++)
            {
                laserSet1Scripts[i] = laserSet1[i].GetComponent<Laser>();
            }

            for (int i = 0; i < laserSet2.Length; i++)
            {
                laserSet2Scripts[i] = laserSet2[i].GetComponent<Laser>();
            }

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
        if (cutsceneOngoing)
        {
            //Play beginning cutscene when play button clicked
            StartCoroutine("ShowCutscene");
        }
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

    //Beginning cutscene
    IEnumerator ShowCutscene()
    {
        cutscene.enabled = true;
        cutscene.sprite = cutscene1;
        cutsceneAudioSource.clip = cutsceneAudio[0];
        cutsceneAudioSource.Play();
        yield return new WaitForSeconds(cutsceneAudioSource.clip.length);

        cutscene.sprite = cutscene2;
        cutsceneAudioSource.clip = cutsceneAudio[1];
        cutsceneAudioSource.Play();
        yield return new WaitForSeconds(cutsceneAudioSource.clip.length);

        cutscene.sprite = cutscene3;
        cutsceneAudioSource.clip = cutsceneAudio[2];
        cutsceneAudioSource.Play();
        yield return new WaitForSeconds(cutsceneAudioSource.clip.length);

        cutscene.sprite = cutscene4;
        cutsceneAudioSource.clip = cutsceneAudio[3];
        cutsceneAudioSource.Play();
        yield return new WaitForSeconds(cutsceneAudioSource.clip.length);

        cutscene.sprite = cutscene5;
        cutsceneAudioSource.clip = cutsceneAudio[4];
        cutsceneAudioSource.Play();
        yield return new WaitForSeconds(cutsceneAudioSource.clip.length);

        cutscene.sprite = cutscene6;
        cutsceneAudioSource.clip = cutsceneAudio[5];
        cutsceneAudioSource.Play();
        yield return new WaitForSeconds(cutsceneAudioSource.clip.length);
        cutscene.enabled = false;
        cutsceneAudioSource.clip = cutsceneAudio[6];
        cutsceneAudioSource.Play();
        yield return new WaitForSeconds(cutsceneAudioSource.clip.length);
        
        cutsceneOngoing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If the player clicks the play button, hide the start menu
        if (gameStarted)
        {
            HideStartMenu();
        }

        if (sceneName == "scene1")
        {
            if (Input.GetKey("space"))
            {
                StopCoroutine("ShowCutscene");
                cutscene.enabled = false;
                cutsceneOngoing = false;
            }
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
                level2AudioPlayed = false;
                level3AudioPlayed = false;
            }
        }

        if (sceneName == "scene2")
        {
            if (gameRestarted)
            {
                HideGameOverMenu();
                HideWinMenu();
                SceneManager.LoadScene("scene2");
                playerScript.codeCounter = 0;
                fovScript.lostGame = false;
                fovScript2.lostGame = false;
                fovScript3.lostGame = false;
                fovScript4.lostGame = false;
                gameRestarted = false;
            }
        }

        if (sceneName == "scene3")
        {
            if (gameRestarted)
            {
                HideGameOverMenu();
                HideWinMenu();
                playerScript.codeCounter = 0;
                if (vaultCode.codeIsCorrect)
                {
                    SceneManager.LoadScene("scene1");
                }
                else
                {
                    SceneManager.LoadScene("scene3");
                }
                
                /*fovScript.lostGame = false;
                fovScript2.lostGame = false;
                fovScript3.lostGame = false;
                fovScript4.lostGame = false;*/
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
            if (fovScript.lostGame || fovScript2.lostGame || fovScript3.lostGame || fovScript4.lostGame || cameraFOV.lostGame || cameraFOV2.lostGame || cameraFOV3.lostGame)
            {
                ShowGameOverMenu();
            }
        }

        if (sceneName == "scene3")
        {
            for (int i = 0; i < laserSet1.Length; i++)
            {
                if (laserSet1Scripts[i].touchedLaser)
                {
                    ShowGameOverMenu();
                }
            }

            for (int i = 0; i < laserSet2.Length; i++)
            {
                if (laserSet2Scripts[i].touchedLaser)
                {
                    ShowGameOverMenu();
                }
            }
        }

        //If player has guessed vault password correctly, show the winning screen
        if (sceneName == "scene3") 
        {
            if (vaultCode.codeIsCorrect) 
            {
                ShowWinMenu();
            }
        }

        if (sceneName == "scene2" || sceneName == "scene3")
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

        cameraFOV = GameObject.Find("cameraViewPoint").GetComponent<CameraFOV>();
        cameraFOV2 = GameObject.Find("cameraViewPoint2").GetComponent<CameraFOV>();
        cameraFOV3 = GameObject.Find("cameraViewPoint3").GetComponent<CameraFOV>();
    }

    void GetFovOfAllGuardsLevel1()
    {
        fovScriptGetter = GameObject.Find("pivotviewpoint");
        fovScriptGetter3 = GameObject.Find("pivotviewpoint (1)");

        fovScript = fovScriptGetter.GetComponent<FieldOfView>();
        fovScript3 = fovScriptGetter3.GetComponent<FieldOfView>();
    }
}
