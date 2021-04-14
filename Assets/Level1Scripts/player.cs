using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public GameObject playerChar;
    public GameObject wasdPrompt;
    public TMPro.TextMeshProUGUI counterText;
    public TMPro.TextMeshProUGUI objectiveText;
    float speed = 0.08f;
    bool showWASDInstructionsOnce;
    public bool hasWon = false;
    public int codeCounter = 0;
    float instructionDisappearDistance;
    Vector2 startPos;
    Vector2 pos;
    Vector2 restartPos;

    float sprintSpeed = 0.16f;
    float sprintStartTime;
    float coolDownDuration;
    public bool sprinting = false;
    public float sprintingDuration = 1f;

    //Getting sprites
    public Sprite upSprite, downSprite, leftSprite, rightSprite, upLeftSprite, downLeftSprite, upRightSprite, downRightSprite;
    SpriteRenderer spriteRenderer;
    public GameObject playerSprite;

    FieldOfView fovScript, fovScript2, fovScript3, fovScript4;
    GameObject fovScriptGetter, fovScriptGetter2, fovScriptGetter3, fovScriptGetter4;
    CameraFOV cameraFOV, cameraFOV2, cameraFOV3;
    Laser laserScript;
    MazeMinigame mazeScript;
    GameObject mazeScriptGetter;
    GameObject ventDoor;
    VaultCode vaultCode;
    menus menuScript;
    computer computer1;
    computer2 computer2;
    computer3 computer3;
    Vault vault;

    public AudioSource audioSource;

    Image sprintTimer;
    Image sprintImg;

    string sceneName;

    GameObject[] laserSet1;
    GameObject[] laserSet2;
    Laser[] laserSet1Scripts;
    Laser[] laserSet2Scripts;

    // Start is called before the first frame update
    void Start()
    {
        laserSet1Scripts = new Laser[7];
        laserSet2Scripts = new Laser[7];
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        playerSprite = GameObject.Find("PlayerChar_front");
        spriteRenderer = playerSprite.GetComponent<SpriteRenderer>();

        if (sceneName == "scene1")
        {
            showWASDInstructionsOnce = true; //show WASD instructions on start
        }


        startPos = transform.position;

        transform.position = startPos;
        playerChar = GameObject.FindGameObjectWithTag("Player");
        wasdPrompt = GameObject.Find("wasd_prompt");

        mazeScriptGetter = GameObject.Find("MazeGame");
        mazeScript = mazeScriptGetter.GetComponent<MazeMinigame>();
        menuScript = GameObject.Find("playButton").GetComponent<menus>();

        sprintTimer = GameObject.Find("sprintTimer").GetComponent<Image>();
        sprintImg = GameObject.Find("sprintImage").GetComponent<Image>();
        sprintTimer.enabled = false;
        sprintImg.enabled = false;

        //Get all guards' field of view scripts in order to access their variables (specifically the lostGame boolean)
        if (sceneName == "scene1")
        {
            GetFovOfAllGuardsLevel1();
            ventDoor = GameObject.Find("ventDoor");
            ventDoor.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            ventDoor.GetComponent<Rigidbody2D>().simulated = false;
        }

        if (sceneName == "scene2")
        {
            computer1 = GameObject.Find("Computer").GetComponent<computer>();
            computer2 = GameObject.Find("Computer2").GetComponent<computer2>();
            computer3 = GameObject.Find("Computer3").GetComponent<computer3>();
            vault = GetComponent<Vault>();
            GetFovOfAllGuards();
        }

        if (sceneName == "scene3")
        {
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

    }

    // Update is called once per frame
    void Update()
    {
        ///////////////CHEAT CODES//////////////////
        switch (sceneName)
        {
            case "scene1":
                if (Input.GetKey("="))
                {
                    codeCounter = 3;
                }
                break;
            case "scene2":
                if (Input.GetKey("="))
                {
                    codeCounter = 3;
                }
                break;
            case "scene3":
                if (Input.GetKey("="))
                {
                    transform.position = new Vector3(33.36f, 19.27f, 0);
                }
                break;
        }
        ////////////////////////////////////////////

        if (sceneName != "scene3")
        {
            counterText.text = "" + codeCounter;
        }

        pos = transform.position;
        restartPos = new Vector3(4.6f, -23.86f, -0.4f);

        
        if (codeCounter == 3)
        {
            hasWon = true;
        }
        

        if (sceneName == "scene1")
        {
            if (hasWon)
            {
                objectiveText.text = "Objective: Find the door that leads to the second floor";
            }

            if (menuScript.cutsceneOngoing)
            {
                pos = restartPos;
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
                ventDoor.GetComponent<SpriteRenderer>().color = new Color(0.1949f, 0.2141f, 0.2924f, 1f);
                ventDoor.GetComponent<Rigidbody2D>().simulated = true;
                wasdPrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            }
        }
        else
        {
            wasdPrompt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }

        if (sceneName == "scene2")
        {
            if (vault.mazeFinished && computer2.hasBeenCollected && computer3.hasBeenCollected)
            {
                objectiveText.text = "Objective: Find the elevator that leads to the third floor";
            }
        }

        if (sceneName == "scene1")
        {
            if ((fovScript.lostGame == true) || (fovScript3.lostGame == true))
            {
                sprintTimer.enabled = false;
                sprintImg.enabled = false;
                codeCounter = 0;
                pos = new Vector3(4.6f, -16.89f, -0.4f);
            }
        }

        if (sceneName == "scene2")
        {
            //If player lost the game, return to the start position
            if ((fovScript.lostGame == true) || (fovScript2.lostGame == true) || (fovScript3.lostGame == true) || (fovScript4.lostGame == true) || (cameraFOV.lostGame == true) || (cameraFOV2.lostGame == true) || (cameraFOV3.lostGame == true))
            {
                sprintTimer.enabled = false;
                sprintImg.enabled = false;
                codeCounter = 0;
                pos = restartPos;
            }
        }

        if(sceneName == "scene3")
        {
            for (int i = 0; i < laserSet1.Length; i++)
            {
                if (laserSet1Scripts[i].touchedLaser)
                {
                    sprintTimer.enabled = false;
                    sprintImg.enabled = false;
                    codeCounter = 0;
                    pos = restartPos;
                }
            }

            for (int i = 0; i < laserSet2.Length; i++)
            {
                if (laserSet2Scripts[i].touchedLaser)
                {
                    sprintTimer.enabled = false;
                    sprintImg.enabled = false;
                    codeCounter = 0;
                    pos = restartPos;
                }
            }
        }

        //If player won the game
        if (sceneName == "scene3") 
        {
            if (vaultCode.codeIsCorrect)
            {
                print("youve won!!");
                //pos = restartPos;
                hasWon = false;
            }
        }

        if (sceneName == "scene2")
        {
            if (!mazeScript.mazeGameOngoing)
            {
                PlayerWalk();
            }
        }
        else
        {
            PlayerWalk();
        }
    }

    //Player walking function
    void PlayerWalk()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            sprintTimer.enabled = false;
            sprintImg.enabled = false;
        }

        if (Input.GetKey("w"))
        {
            CheckIfSprinting();

            if (sprinting)
            {
                sprintTimer.enabled = true;
                sprintImg.enabled = true;
                pos += Vector2.up * sprintSpeed;
            }
            else
            {
                if (!sprinting || !Input.GetKey(KeyCode.LeftShift))
                {
                    sprintTimer.enabled = false;
                    sprintImg.enabled = false;
                    pos += Vector2.up * speed;
                }
            }
            
            spriteRenderer.sprite = upSprite; //Sprite
        }

        if (Input.GetKey("s"))
        {
            CheckIfSprinting();

            if (sprinting)
            {
                sprintTimer.enabled = true;
                sprintImg.enabled = true;
                pos += Vector2.down * sprintSpeed;
            }
            else
            {
                if (!sprinting || !Input.GetKey(KeyCode.LeftShift))
                {
                    sprintTimer.enabled = false;
                    sprintImg.enabled = false;
                    pos += Vector2.down * speed;
                }
            }

            spriteRenderer.sprite = downSprite;
        }

        if (Input.GetKey("d"))
        {
            CheckIfSprinting();

            if (sprinting)
            {
                sprintTimer.enabled = true;
                sprintImg.enabled = true;
                pos += Vector2.right * sprintSpeed;
            }
            else
            {
                if (!sprinting || !Input.GetKey(KeyCode.LeftShift))
                {
                    sprintTimer.enabled = false;
                    sprintImg.enabled = false;
                    pos += Vector2.right * speed;
                }
            }

            spriteRenderer.sprite = rightSprite;
        }

        if (Input.GetKey("a"))
        {
            CheckIfSprinting();

            if (sprinting)
            {
                sprintTimer.enabled = true;
                sprintImg.enabled = true;
                pos += Vector2.left * sprintSpeed;
            }
            else
            {
                if (!sprinting || !Input.GetKey(KeyCode.LeftShift))
                {
                    sprintTimer.enabled = false;
                    sprintImg.enabled = false;
                    pos += Vector2.left * speed;
                }
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

    void CheckIfSprinting()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprintStartTime = Time.time;
            print("sprinting");
            sprinting = true;
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftShift) || (sprintStartTime + sprintingDuration < Time.time))
            {
                print("walking");
                sprinting = false;
            }
        }
    }
}
