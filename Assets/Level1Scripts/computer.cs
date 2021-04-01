using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class computer : MonoBehaviour
{
    bool hasCollided = false;
    public bool hasBeenCollected = false;
    GameObject ePrompt;
    GameObject ePromptSprite;
    GameObject thePlayer;
    player playerScript;
    float tCycle;

    FieldOfView fovScript, fovScript2, fovScript3, fovScript4;
    GameObject fovScriptGetter, fovScriptGetter2, fovScriptGetter3, fovScriptGetter4;

    MazeMinigame mazeScript;
    GameObject mazeScriptGetter;

    string sceneName;

    //If player is near computer, ePrompt shows
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Collided");
            ePrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasCollided = true;
        }
    }

    //If player is not near computer, ePrompt is hidden
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ePrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            hasCollided = false;
        }
    }

    void AnimateSpriteSpinning()
    {
        float t = Time.time;
        if (t > tCycle)
        {
            tCycle = t + 2;
        }

        if (tCycle - t <= 1)
        { // Spins during the last second
            ePromptSprite.transform.Rotate(0, 0, 180 * Time.deltaTime);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        ePrompt = GameObject.Find("e_prompt");
        ePromptSprite = GameObject.Find("ePromptSprite");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();

        //Test minigame scripts will be put on actual minigame prmopts later
        mazeScriptGetter = GameObject.Find("MazeGame");
        mazeScript = mazeScriptGetter.GetComponent<MazeMinigame>();

        if (sceneName == "scene1")
        {
            GetFovOfAllGuardsLevel1();
        }

        if (sceneName == "scene2")
        {
            GetFovOfAllGuards();
        }

        ePrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        AnimateSpriteSpinning();

        //Player collects computer code if collided with object while pressing "E"
        if (hasCollided && Input.GetKeyDown(KeyCode.E) && !hasBeenCollected)
        {
            Debug.Log("collected");
            playerScript.codeCounter++;

            //Test timer- will put on actual minigame prompt later
            mazeScript.timer.maxValue = (float)mazeScript.countDownTime;
            mazeScript.timer.value = (float)mazeScript.countDownTime;
            mazeScript.mazeGameOngoing = true;
            hasBeenCollected = true;
            
            if (mazeScript.gameOngoing)
            {
                print("maze game happening right now");
            }
        }

        //If the computer has been collected, the computer sprite and ePrompt will not show
        if (hasBeenCollected)
        {
            ePrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            ePromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }

        if (sceneName == "scene1")
        {
            if ((fovScript.lostGame && hasBeenCollected) || (fovScript3.lostGame && hasBeenCollected))
            {
                ePromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                hasBeenCollected = false;
            }
        }

        //If player was spot by any of the guards' field of views, reset code if it has been collected
        if (sceneName == "scene2")
        {
            if ((fovScript.lostGame && hasBeenCollected) || (fovScript2.lostGame && hasBeenCollected) || (fovScript3.lostGame && hasBeenCollected) || (fovScript4.lostGame && hasBeenCollected))
            {
                ePromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                hasBeenCollected = false;
            }
        }

        if (playerScript.codeCounter == 0)
        {
            ePromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasBeenCollected = false;
        }
        
    }

    //Get all guards' field of view scripts in order to access their variables (specifically the lostGame boolean)
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
