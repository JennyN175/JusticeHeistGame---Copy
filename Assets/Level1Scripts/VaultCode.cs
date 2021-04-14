using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VaultCode : MonoBehaviour
{
    bool hasCollided = false;
    public bool codeIsCorrect = false;
    bool winAudioPlayed = false;
    player playerScript;
    GameObject player;
    GameObject vaultPromptSprite;
    InputField inputField;
    Image fieldImage;
    Text fieldText;
    string inputCode;
    menus menuScript;

    Image clueImage, clueImage2, clueImage3;
    public TMPro.TextMeshProUGUI letterClueText, letterClueText2, letterClueText3;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            vaultPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            hasCollided = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            vaultPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            hasCollided = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inputField = GameObject.Find("inputCode").GetComponent<InputField>();
        fieldImage = GameObject.Find("inputCode").GetComponent<Image>();
        fieldText = GameObject.Find("codeText").GetComponent<Text>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        player = GameObject.FindGameObjectWithTag("Player");
        vaultPromptSprite = GameObject.Find("vaultPrompt");
        menuScript = GameObject.Find("playButton").GetComponent<menus>();

        clueImage = GameObject.Find("clueImage").GetComponent<Image>();
        clueImage2 = GameObject.Find("clueImage2").GetComponent<Image>();
        clueImage3 = GameObject.Find("clueImage3").GetComponent<Image>();

        clueImage2.enabled = true;
        clueImage.enabled = true;
        clueImage3.enabled = true;

        HideTextField();
        vaultPromptSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided && Input.GetKeyDown(KeyCode.E))
        {
            ShowTextField();
        }

        //Store the user input from textfield inside inputCode string
        inputCode = inputField.text;
        if (!string.IsNullOrEmpty(inputCode))
        {
            //If player's input code is the same as the vault password, code is correct
            if (inputCode == Vault.vaultWord)
            {
                if (!winAudioPlayed)
                {
                    menuScript.cutsceneAudioSource.PlayOneShot(menuScript.cutsceneAudio[1]);
                    winAudioPlayed = true;
                }
                codeIsCorrect = true;
                player.transform.position = new Vector3(33.36f, 19.27f, 0);
                HideTextField();
            }
        }

        if (inputField.enabled)
        {
            player.transform.position = new Vector3(33.36f, 19.27f, 0);
        }

        //Show clues in top corner of screen depending on game's randomly generated vault code
        switch (Vault.vaultWord)
        {
            case "rob":
                letterClueText.text = "B";
                letterClueText2.text = "O";
                letterClueText3.text = "R";
                break;
            case "win":
                letterClueText.text = "N";
                letterClueText2.text = "I";
                letterClueText3.text = "W";
                break;
            case "hid":
                letterClueText.text = "D";
                letterClueText2.text = "I";
                letterClueText3.text = "H";
                break;
            case "gem":
                letterClueText.text = "M";
                letterClueText2.text = "E";
                letterClueText3.text = "G";
                break;
        }
    }

    void HideTextField()
    {
        inputField.enabled = false;
        fieldImage.enabled = false;
        fieldText.enabled = false;
    }

    void ShowTextField()
    {
        inputField.enabled = true;
        fieldImage.enabled = true;
        fieldText.enabled = true;
    }
}
