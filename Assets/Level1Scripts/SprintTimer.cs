using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintTimer : MonoBehaviour
{
    Image fillImg;
    player playerObj;
    GameObject playerScript;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        fillImg = this.GetComponent<Image>();
        playerScript = GameObject.Find("testPlayer");
        playerObj = playerScript.GetComponent<player>();
        //time = playerObj.sprintingDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj.sprinting)
        {
            fillImg.fillAmount -= 1.0f / playerObj.sprintingDuration * Time.deltaTime;
            
        }
        else
        {

            fillImg.fillAmount = 1;
           
        }
    }
}
