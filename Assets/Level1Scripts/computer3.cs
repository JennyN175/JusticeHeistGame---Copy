using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class computer3 : MonoBehaviour
{
    bool hasCollided = false;
    public bool hasBeenCollected = false;
    GameObject ePrompt3;
    GameObject ePromptSprite3;
    GameObject thePlayer;
    player playerScript;
    float tCycle;

    FieldOfView fovScript, fovScript2, fovScript3, fovScript4;
    GameObject fovScriptGetter, fovScriptGetter2, fovScriptGetter3, fovScriptGetter4;

    string sceneName;

    string letterClue;
    public TMPro.TextMeshProUGUI letterClueText;
    Vault vaultScript;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Collided");
            ePrompt3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasCollided = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ePrompt3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
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
            ePromptSprite3.transform.Rotate(0, 0, 180 * Time.deltaTime);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        ePrompt3 = GameObject.Find("e_prompt3");
        ePromptSprite3 = GameObject.Find("ePromptSprite3");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();
        vaultScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Vault>();

        if (sceneName == "scene1")
        {
            GetFovOfAllGuardsLevel1();
        }

        if (sceneName == "scene2")
        {
            GetFovOfAllGuards();
            letterClueText.text = "";
        }

        ePrompt3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        AnimateSpriteSpinning();
        if (hasCollided && Input.GetKeyDown(KeyCode.E) && !hasBeenCollected)
        {
            Debug.Log("collected");
            playerScript.codeCounter++;
            hasBeenCollected = true;
        }

        if (hasBeenCollected)
        {
            ePrompt3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            ePromptSprite3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }

        if (sceneName == "scene1")
        {
            if ((fovScript.lostGame && hasBeenCollected) || (fovScript3.lostGame && hasBeenCollected))
            {
                ePromptSprite3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                hasBeenCollected = false;
            }
        }

        if (sceneName == "scene2")
        {
            if ((fovScript.lostGame && hasBeenCollected) || (fovScript2.lostGame && hasBeenCollected) || (fovScript3.lostGame && hasBeenCollected) || (fovScript4.lostGame && hasBeenCollected))
            {
                ePromptSprite3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                hasBeenCollected = false;
            }
        }

        if (playerScript.codeCounter == 0)
        {
            ePromptSprite3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasBeenCollected = false;
        }

        if (sceneName == "scene2")
        {
            if (hasBeenCollected) //will change later when minigame is added
            {
                switch (Vault.vaultWord)
                {
                    case "rob":
                        letterClue = "R"; break;
                    case "win":
                        letterClue = "W"; break;
                    case "hid":
                        letterClue = "H"; break;
                    case "gem":
                        letterClue = "G"; break;
                }
            }

            if (vaultScript.clueReceived)
            {
                letterClueText.text = "" + letterClue;
            }
        }
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
