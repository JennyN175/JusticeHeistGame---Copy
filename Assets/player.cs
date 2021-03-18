using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject playerChar;
    public GameObject wasdPrompt;
    public TMPro.TextMeshProUGUI counterText;
    float speed = 0.08f;
    bool showWASDInstructionsOnce;
    public bool hasWon = false;
    public int codeCounter = 0;
    float instructionDisappearDistance;
    Vector3 startPos;
    Vector3 pos;
    Vector3 restartPos;

    float sprintSpeed = 0.16f;

    //Getting sprites
    public Sprite upSprite, downSprite, leftSprite, rightSprite, upLeftSprite, downLeftSprite, upRightSprite, downRightSprite;
    SpriteRenderer spriteRenderer;
    public GameObject playerSprite;

    FieldOfView fovScript, fovScript2, fovScript3, fovScript4;
    GameObject fovScriptGetter, fovScriptGetter2, fovScriptGetter3, fovScriptGetter4;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GameObject.Find("PlayerChar_front");
        spriteRenderer = playerSprite.GetComponent<SpriteRenderer>();

        showWASDInstructionsOnce = true; //show WASD instructions on start
        startPos = transform.position;
        playerChar = GameObject.FindGameObjectWithTag("Player");
        wasdPrompt = GameObject.Find("wasd_prompt");

        //Get all guards' field of view scripts in order to access their variables (specifically the lostGame boolean)
        GetFovOfAllGuards();
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = "" + codeCounter;
        pos = transform.position;
        restartPos = new Vector3(4.6f, -23.86f, -0.4f);

        if (codeCounter == 3)
        {
            hasWon = true;
        }

        //If the player has just started the game, show WASD instructions
        if (showWASDInstructionsOnce)
        {
            instructionDisappearDistance = Vector3.Distance(startPos, pos);
            if (instructionDisappearDistance < 5f)
            {
                audioSource.Play(); //Plays audio when player moves past certain point (will modify and fix this later)
                
            }
        }


        //WASD instructions disappear after player travels 5f distance
        if (instructionDisappearDistance > 5f)
        {
            wasdPrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }



        //If player lost the game, return to the start position
        if ((fovScript.lostGame == true) || (fovScript2.lostGame == true) || (fovScript3.lostGame == true) || (fovScript4.lostGame == true))
        {
            codeCounter = 0;
            pos = restartPos;
        }

        //If player won the game, return to the start position
        if (hasWon)
        {
            pos = restartPos;
            hasWon = false;
        }

        PlayerWalk();
    }

    //Player walking function
    void PlayerWalk()
    {
        if (Input.GetKey("w"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                pos += Vector3.up * sprintSpeed; //If left shift is pressed, player sprints
            }
            else
            {
                pos += Vector3.up * speed;
            }
            spriteRenderer.sprite = upSprite; //Sprite
        }
        if (Input.GetKey("s"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                pos += Vector3.down * sprintSpeed;
            }
            else
            {
                pos += Vector3.down * speed;
            }
            spriteRenderer.sprite = downSprite;
        }
        if (Input.GetKey("d"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                pos += Vector3.right * sprintSpeed;
            }
            else
            {
                pos += Vector3.right * speed;
            }
            spriteRenderer.sprite = rightSprite;
        }
        if (Input.GetKey("a"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                pos += Vector3.left * sprintSpeed;
            }
            else
            {
                pos += Vector3.left * speed;
            }
            spriteRenderer.sprite = leftSprite;
        }

        if (Input.GetKey("w") && Input.GetKey("a"))
        {
            spriteRenderer.sprite = upLeftSprite;
        }
        if (Input.GetKey("s") && Input.GetKey("a"))
        {
            spriteRenderer.sprite = downLeftSprite;
        }
        if (Input.GetKey("w") && Input.GetKey("d"))
        {
            spriteRenderer.sprite = upRightSprite;
        }
        if (Input.GetKey("s") && Input.GetKey("d"))
        {
            spriteRenderer.sprite = downRightSprite;
        }

        transform.position = pos;
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
