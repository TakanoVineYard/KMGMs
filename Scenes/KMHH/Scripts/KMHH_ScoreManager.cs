using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //テキスト使うなら必要
using static KMHH_TimeManager; //時間管理スクリプトを使う

/// <summary>
///スコア関係の管理
/// </summary>
/// <returns></returns> 
public class KMHH_ScoreManager : MonoBehaviour
{

    public  int comboCountNum = 0; //コンボを数える
    const float baseScore = 100.0f; //ベースの計算スコア
    public static float totalScore = 0.0f;　//合計スコア


    public Text comboText;　//コンボの表示
    public Text scoreText;　//スコアの表示

    public float[] timeJudgeRange = new float[5]; //回答までの時間区分け
    public float[] timeJudgeValue = new float[6];　//回答までの時間による係数

    public CharacterTest at;

    public bool successResult;

    //テキストを上書いて、Great、Goodなどやってたが、本番ではPrefabごとに作るので不要
//    private GameObject ctj;


    public static int maxCombo = 0;
    public static int scoreExcellent = 0;
    public static int scoreGreat = 0;
    public static int scoreGood = 0;
    public static int scoreNotGood = 0;
    public static int scoreBad = 0;
    public static int scoreMiss = 0;



////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    // Start is called before the first frame update
    /// <summary>
    ///スコア関連
    /// </summary>
    /// <returns></returns> 
    void Start()
    {

        //コンボカウントしてるオブジェクトと、スコア表示のオブジェクトの取得
        GameObject comboTextObj = GameObject.Find("ui_ComboText");
        GameObject scoreTextObj = GameObject.Find("ui_TotalScore");

        //テキスト乗っ取り
        comboText = comboTextObj.GetComponentInChildren<Text>();
        scoreText = scoreTextObj.GetComponentInChildren<Text>();

        Debug.Log(comboText);
        Debug.Log(scoreText);        
        
        scoreText.text = "Score:0000";
        comboText.text = "00Combo";
    }

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    // Update is called once per frame
    /// <summary>
    ///スコアのリセット
    /// </summary>
    /// <returns></returns> 
    void Update()
    {
         //ゲーム始まってないし、終わってない間　リセット
        if ((KMHH_TimeManager.gameStart == false)&&(KMHH_TimeManager.gameFinish == false )){

            totalScore = 0;
            comboCountNum = 0;
            maxCombo = 0;
            scoreExcellent = 0;
            scoreGreat = 0;
            scoreGood = 0;
            scoreNotGood = 0;
            scoreBad = 0;
            scoreMiss = 0;  

        }       
    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 他から呼び出される、　正解かどうかの引数を受け取って、
    ///コンボの加減算やマックスコンボの更新、
    ///スコアとコンボ数の表示更新などを行う
    /// </summary>
    /// <returns>trueかfalseを受け取る</returns>
    public void AddAnserResult(bool anser)
     {
        //★　float anserTimeRange =  //回答までにかかった時間
/*
            if (anser) //回答が正解だったら
            {
                comboCountNum++;　　//コンボ加算

                if(comboCountNum > maxCombo)　　//もしコンボが最大だったら
                {
                    maxCombo = comboCountNum; //マックスコンボの更新
                }

            }
            else if ((anser)&&(anserTimeRange > timeJudgeRange[3]))  //成功だが3秒以上かかっててもだめ
            {
            comboCountNum = 0;

            }
            else　//回答が不正解だったらコンボリセット
            {
                comboCountNum = 0;

            }
           
        scoreText.text = "Score:" + GetScore(anserTimeRange);
        comboText.text = comboCountNum.ToString() + "Combo";
        //Debug.Log(comboCountNum + "コンボ");
        //Debug.Log("スコア"+GetScore());
        */ 
    }

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 現状でのスコアをゲッツ
    /// </summary>
    /// <returns>コンボによる倍率をかけた値を返す</returns>
    public float GetScore(float pastAnserTime)
    {

        //係数をいれる
        float coefficient = 0.0f;

       if ((pastAnserTime <= timeJudgeRange[0]) && (successResult))
        {
            coefficient = timeJudgeValue[0];
            Debug.Log("Excellent!" + timeJudgeValue[0]);
            scoreExcellent += 1;

        }
       else if ((pastAnserTime > timeJudgeRange[0]) && (pastAnserTime <= timeJudgeRange[1]) && (successResult))
        {
            coefficient = timeJudgeValue[1];
            Debug.Log("Great" + timeJudgeValue[1]);
            scoreGreat += 1;

        }
        else if ((pastAnserTime > timeJudgeRange[1]) && (pastAnserTime <= timeJudgeRange[2]) && (successResult))
        {
            coefficient = timeJudgeValue[2];
            Debug.Log("Good" + timeJudgeValue[2]);
            scoreGood += 1;

        }
        else if ((pastAnserTime > timeJudgeRange[2]) && (pastAnserTime <= timeJudgeRange[3]) && (successResult))
        {
            coefficient = timeJudgeValue[3];
            Debug.Log("NotGood");
            scoreNotGood += 1;

        }
        else if ((pastAnserTime > timeJudgeRange[3]) && (pastAnserTime <= timeJudgeRange[4]) && (successResult))
        {
            coefficient = timeJudgeValue[4];
            Debug.Log("Bad");
            scoreBad += 1;

        }

        //回答時間が指定時間より長かったら
        else if (pastAnserTime > timeJudgeRange[4])
        {
            coefficient = timeJudgeValue[5];

            Debug.Log("miss");
            // // // // ctj.GetComponent<TextMesh>().text = "miss";
            //コンボ数も降り出し
            comboCountNum = 0;

            scoreMiss += 1;

        }
        at.endTime = 0;

        if (comboCountNum == 0){
            return totalScore;
        }
        else
        {
            totalScore += baseScore * coefficient * comboCountNum;
            return totalScore;

        }
    }


}
