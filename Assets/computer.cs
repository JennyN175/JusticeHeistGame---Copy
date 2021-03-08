using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computer : MonoBehaviour
{
    bool hasCollided = false;
    GameObject ePrompt;

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

    // Start is called before the first frame update
    void Start()
    {
        ePrompt = GameObject.Find("e_prompt");
        ePrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided && Input.GetKeyDown(KeyCode.E))
        {
            //Do some stuffs like health
            Debug.Log("collected");
            Destroy(gameObject); //Remove the item
        }
    }
}
