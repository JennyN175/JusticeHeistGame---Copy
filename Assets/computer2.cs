using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computer2 : MonoBehaviour
{
    bool hasCollided = false;
    GameObject ePrompt2;
    GameObject ePromptSprite2;
    GameObject thePlayer;
    player playerScript;
    float tCycle;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Collided");
            ePrompt2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasCollided = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ePrompt2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
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
            ePromptSprite2.transform.Rotate(0, 0, 180 * Time.deltaTime);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ePrompt2 = GameObject.Find("e_prompt2");
        ePromptSprite2 = GameObject.Find("ePromptSprite2");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = thePlayer.GetComponent<player>();
        ePrompt2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        AnimateSpriteSpinning();
        if (hasCollided && Input.GetKeyDown(KeyCode.E))
        {
            //Do some stuffs like health
            Debug.Log("collected");
            playerScript.codeCounter++;
            Destroy(gameObject); //Remove the item
        }
    }
}
