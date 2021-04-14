using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingGame : MonoBehaviour
{
    Image background, frontCard, backCard;
    public TMPro.TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.Find("MatchingGameMap").GetComponent<Image>();
        frontCard = GameObject.Find("MainCard").GetComponent<Image>();
        backCard = GameObject.Find("Card_Back").GetComponent<Image>();

        HideMatchingGame();
    }

    // Update is called once per frame
    void Update()
    {
        HideMatchingGame();
    }

    void HideMatchingGame()
    {
        background.enabled = false;
        frontCard.enabled = false;
        backCard.enabled = false;
        score.enabled = false;
    }
}
