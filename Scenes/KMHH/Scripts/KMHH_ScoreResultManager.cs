﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //テキスト使うなら必要？
using static KMHH_ScoreManager;
using UnityEngine.SceneManagement; //シーン切り替え
using TMPro; //TextMeshPro用


public class KMHH_ScoreResultManager : MonoBehaviour
{

    public GameObject resultMaxComboObj;
    public GameObject resultScoreTextObj;
    public GameObject resultScoreExcellentObj;
    public GameObject resultScoreGreatObj;
    public GameObject resultScoreGoodObj;
    public GameObject resultScoreNotGoodObj;
    public GameObject resultScoreBadObj;
    public GameObject resultScoreMissObj;

    TextMeshProUGUI resultMaxComboText;
    TextMeshProUGUI resultScoreText;
    TextMeshProUGUI resultScoreExcellent;
    TextMeshProUGUI resultScoreGreat;
    TextMeshProUGUI resultScoreGood;
    TextMeshProUGUI resultScoreNotGood;
    TextMeshProUGUI resultScoreBad;
    TextMeshProUGUI resultScoreMiss;

    // Start is called before the first frame update
    public void Start()
    {
        resultMaxComboObj = GameObject.Find("ResultMaxCombo");
        resultScoreTextObj = GameObject.Find("ResultScore");
        resultScoreExcellentObj = GameObject.Find("ResultExcellent");
        resultScoreGreatObj = GameObject.Find("ResultGreat");
        resultScoreGoodObj = GameObject.Find("ResultGood");
        resultScoreNotGoodObj = GameObject.Find("ResultNotGood");
        resultScoreBadObj = GameObject.Find("ResultBad");
        resultScoreMissObj = GameObject.Find("ResultMiss");


        resultMaxComboText = resultMaxComboObj.GetComponent<TextMeshProUGUI>();
        resultScoreText = resultScoreTextObj.GetComponent<TextMeshProUGUI>();
        resultScoreExcellent = resultScoreExcellentObj.GetComponent<TextMeshProUGUI>();
        resultScoreGreat = resultScoreGreatObj.GetComponent<TextMeshProUGUI>();
        resultScoreGood = resultScoreGoodObj.GetComponent<TextMeshProUGUI>();
        resultScoreNotGood = resultScoreNotGoodObj.GetComponent<TextMeshProUGUI>();
        resultScoreBad = resultScoreBadObj.GetComponent<TextMeshProUGUI>();
        resultScoreMiss = resultScoreMissObj.GetComponent<TextMeshProUGUI>();

        resultMaxComboText.text =  "COMBO:"+ KMHH_ScoreManager.maxCombo.ToString();
        resultScoreText.text = "SCORE:" + KMHH_ScoreManager.totalScore.ToString();
        resultScoreExcellent.text = "Excellent:" + KMHH_ScoreManager.scoreExcellent.ToString();
        resultScoreGreat.text = "Great:" + KMHH_ScoreManager.scoreGreat.ToString();
        resultScoreGood.text = "Good:" + KMHH_ScoreManager.scoreGood.ToString();
        resultScoreNotGood.text = "NotGood:" + KMHH_ScoreManager.scoreNotGood.ToString();
        resultScoreBad.text = "Bad:" + KMHH_ScoreManager.scoreBad.ToString();
        resultScoreMiss.text = "Miss:" + KMHH_ScoreManager.scoreMiss.ToString();

    }

// Update is called once per frame
void Update()
    {
        
    }

    public void ShowResult()
    {
        
    }

    public void TapToBackTitleButton()
    {
        Invoke("DerayGameLoadRun", 1);
    }

    public void DerayGameLoadRun()
    {
        SceneManager.LoadScene("MainTitle");
    }

}