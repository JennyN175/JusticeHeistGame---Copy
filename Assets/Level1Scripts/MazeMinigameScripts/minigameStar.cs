using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameStar : MonoBehaviour
{
    GameObject mazeScriptGetter;
    MazeMinigame mazeScript;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "minigamePlayer")
        {
            mazeScript.endGame = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        mazeScriptGetter = GameObject.Find("MazeGame");
        mazeScript = mazeScriptGetter.GetComponent<MazeMinigame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
