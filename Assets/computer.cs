using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computer : MonoBehaviour
{
    bool hasCollided = false;
    bool hasBeenCollected = false;
    GameObject ePrompt;
    GameObject ePromptSprite;
    GameObject thePlayer;
    player playerScript;
    float tCycle;

    FieldOfView fovScript, fovScript2, fovScript3, fovScript4;
    GameObject fovScriptGetter, fovScriptGetter2, fovScriptGetter3, fovScriptGetter4;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Collided");
            ePrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasCollided = true;
        }
    }

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
        ePrompt = GameObject.Find("e_prompt");
        ePromptSprite = GameObject.Find("ePromptSprite");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();

        GetFovOfAllGuards();

        ePrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
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
            ePrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            ePromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }

        
        //If player was spot by any of the guards' field of views, reset code if it has been collected
        if ((fovScript.lostGame && hasBeenCollected) || (fovScript2.lostGame && hasBeenCollected) || (fovScript3.lostGame && hasBeenCollected) || (fovScript4.lostGame && hasBeenCollected))
        {
                ePromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                hasBeenCollected = false;
        }

        if (playerScript.codeCounter == 0)
        {
            ePromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasBeenCollected = false;
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
}
