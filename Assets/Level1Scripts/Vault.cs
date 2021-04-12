using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vault : MonoBehaviour
{
    string[] vaultWords;
    public static string vaultWord;
    string sceneName;

    Image clueImage, clueImage2, clueImage3;
    Image clueNotification;
    MazeMinigame mazeScript;
    player playerScript;
    computer2 computer2Script;
    computer3 computer3Script;

    Coroutine cluePopup = null;
    Vector3 startPos, endPos;

    float timeOfTravel = 5; //time after object reach a target place 
    float currentTime = 0; // actual floting time 
    float currentTime2 = 0;
    float normalizedValue;

    public bool clueReceived = false;


    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        //clueImage = GameObject.Find("clueImage").GetComponent<Image>();
        //clueNotification = GameObject.Find("clueNotification").GetComponent<Image>();
        mazeScript = GameObject.Find("MazeGame").GetComponent<MazeMinigame>();
        playerScript = GameObject.Find("testPlayer").GetComponent<player>();

        if (sceneName == "scene2")
        {
            clueImage = GameObject.Find("clueImage").GetComponent<Image>();
            clueImage2 = GameObject.Find("clueImage2").GetComponent<Image>();
            clueImage3 = GameObject.Find("clueImage3").GetComponent<Image>();
            clueNotification = GameObject.Find("clueNotification").GetComponent<Image>();
            computer2Script = GameObject.Find("Computer2").GetComponent<computer2>();
            computer3Script = GameObject.Find("Computer3").GetComponent<computer3>();

            vaultWords = new string[] { "rob", "win", "hid", "gem" };
            vaultWord = vaultWords[Random.Range(0, vaultWords.Length)];

            clueImage2.enabled = false;
            clueImage.enabled = false;
            clueImage3.enabled = false;
            clueNotification.enabled = false;
        }

        //clueImage.enabled = false;
        //clueNotification.enabled = false;

        startPos = new Vector3(-205.5f, 758, 0);
        endPos = new Vector3(450, 758, 0);
    }

    IEnumerator ShowClue()
    {
        clueNotification.enabled = true;
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 

            clueNotification.transform.position = Vector3.Lerp(startPos, endPos, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(3);

        while (currentTime2 <= timeOfTravel)
        {
            currentTime2 += Time.deltaTime;
            normalizedValue = currentTime2 / timeOfTravel; // we normalize our time 

            clueNotification.transform.position = Vector3.Lerp(endPos, startPos, normalizedValue);
            yield return null;
        }

        clueNotification.enabled = false;
        clueReceived = true;
        yield return new WaitForSeconds(1);
        mazeScript.endGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(vaultWord);
        if (sceneName == "scene2")
        {
            if (mazeScript.endGame || computer2Script.hasBeenCollected || computer3Script.hasBeenCollected)
            {
                cluePopup = StartCoroutine("ShowClue");
            }

            if (clueReceived)
            {
                if (mazeScript.endGame)
                {
                    clueImage.enabled = true;
                }

                if (computer2Script.hasBeenCollected)
                {
                    clueImage2.enabled = true;
                }

                if (computer3Script.hasBeenCollected)
                {
                    clueImage3.enabled = true;
                }
            }
            
        }
    }

}
