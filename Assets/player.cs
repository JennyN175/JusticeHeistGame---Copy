using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject playerChar;
    public GameObject wasdPrompt;
    public TMPro.TextMeshProUGUI counterText;
    float speed = 0.05f;
    bool showWASDInstructionsOnce;
    public int codeCounter = 0;
    float instructionDisappearDistance;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        showWASDInstructionsOnce = true;
        startPos = transform.position;
        playerChar = GameObject.FindGameObjectWithTag("Player");
        wasdPrompt = GameObject.Find("wasd_prompt");
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = "" + codeCounter;
        Vector3 pos = transform.position;

        //If the player has just started the game, show WASD instructions
        if (showWASDInstructionsOnce)
        {
            instructionDisappearDistance = Vector3.Distance(startPos, pos);
        }

        if (instructionDisappearDistance > 5f)
        {
            wasdPrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }

        if (Input.GetKey("w"))
        {
            pos += Vector3.up * speed; 
        }
        if (Input.GetKey("s"))
        {
            pos += Vector3.down * speed;
        }
        if (Input.GetKey("d"))
        {
            pos += Vector3.right * speed;
        }
        if (Input.GetKey("a"))
        {
            pos += Vector3.left * speed;
        }

        transform.position = pos;
    }
}
