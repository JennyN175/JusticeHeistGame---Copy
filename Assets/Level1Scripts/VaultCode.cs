using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VaultCode : MonoBehaviour
{
    bool hasCollided = false;
    public bool codeIsCorrect = false;
    player playerScript;
    GameObject vaultPromptSprite;
    InputField inputField;
    Image fieldImage;
    Text fieldText;
    string inputCode;

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
        vaultPromptSprite = GameObject.Find("vaultPrompt");

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

        inputCode = inputField.text;
        if (!string.IsNullOrEmpty(inputCode))
        {
            if (inputCode == Vault.vaultWord)
            {
                codeIsCorrect = true;
                HideTextField();
            }
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
