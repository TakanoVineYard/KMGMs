using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //テキスト使うなら必要
using static KMHH_TimeManager; //時間管理スクリプトを使う
using static KMHH_PlayerInputManager; //インプット管理スクリプトを使う

/// <summary>
///スコア関係の管理
/// </summary>
/// <returns></returns> 
public class KMHH_ScoreManager : MonoBehaviour
{

    public static int comboCountNum = 0; //コンボを数える
    public static float baseScore = 100.0f; //ベースの計算スコア
    public static float totalScore = 0.0f;　//合計スコア


    public static  Text comboText;　//コンボの表示
    public static  Text scoreText;　//スコアの表示

    public static float[] timeJudgeRange = new float[5]; //回答までの時間区分け
    public static float[] timeJudgeValue = new float[6];　//回答までの時間による係数
    public static float coefficient = 0.0f;//係数をいれる

    public static int maxCombo = 0;
    public static int scoreExcellent = 0;
    public static int scoreGreat = 0;
    public static int scoreGood = 0;
    public static int scoreNotGood = 0;
    public static int scoreBad = 0;
    public static int scoreMiss = 0;
    public static float anserTimeRange;


    //デバッグ
    public  GameObject debugLookScoreValObj;
    Text debug_LookScoreValText;


////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    // Start is called before the first frame update
    /// <summary>
    ///スコア関連
    /// </summary>
    /// <returns></returns> 
    void Start()
    {

    debugLookScoreValObj = GameObject.Find("Debug_LookScoreVal");
    debug_LookScoreValText = debugLookScoreValObj.GetComponentInChildren<Text>();


        timeJudgeRange[0] = 1.0f; // 正解までの秒数
        timeJudgeRange[1] = 1.5f; //
        timeJudgeRange[2] = 2.0f; //
        timeJudgeRange[3] = 3.0f; //
        timeJudgeRange[4] = 4.0f; //

        timeJudgeValue[0] = 1.5f; //　正解したときの数値補正
        timeJudgeValue[1] = 1.2f; //
        timeJudgeValue[2] = 1.1f; //
        timeJudgeValue[3] = 1.0f; //
        timeJudgeValue[4] = 0.5f; //
        timeJudgeValue[5] = 0.0f; //


        //コンボカウントしてるオブジェクトと、スコア表示のオブジェクトの取得
        GameObject comboTextObj = GameObject.Find("ui_ComboText");
        GameObject scoreTextObj = GameObject.Find("ui_TotalScore");

        //テキスト乗っ取り
        comboText = comboTextObj.GetComponentInChildren<Text>();
        scoreText = scoreTextObj.GetComponentInChildren<Text>();   
        
        scoreText.text = "Score:";
        comboText.text = "Combo";

        debug_LookScoreValText.text =
            "recordAnserTime :" + anserTimeRange.ToString("f2") + "\n" +
            "totalScore :" + totalScore + "\n" +
            "comboCountNum :" + comboCountNum + "\n" +
            "maxCombo :" + maxCombo + "\n" +
            "scoreExcellent :" + scoreExcellent + "\n" +
            "scoreGreat :" + scoreGreat + "\n" +
            "scoreGood :" + scoreGood + "\n" +
            "scoreNotGood :" + scoreNotGood + "\n" +
            "scoreBad :" + scoreBad + "\n" +
            "scoreMiss :" + scoreMiss + "\n";
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
        debug_LookScoreValText.text =
            "recordAnserTime :" + anserTimeRange.ToString("f2") + "\n" +
            "totalScore :" + totalScore + "\n" +
            "comboCountNum :" + comboCountNum + "\n" +
            "maxCombo :" + maxCombo + "\n" +
            "scoreExcellent :" + scoreExcellent + "\n" +
            "scoreGreat :" + scoreGreat + "\n" +
            "scoreGood :" + scoreGood + "\n" +
            "scoreNotGood :" + scoreNotGood + "\n" +
            "scoreBad :" + scoreBad + "\n" +
            "scoreMiss :" + scoreMiss + "\n";

    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 他から呼び出される、　正解かどうかの引数を受け取って、
    ///コンボの加減算やマックスコンボの更新、
    ///スコアとコンボ数の表示更新などを行う
    /// </summary>
    /// <returns>trueかfalseを受け取る</returns>
    public static void AddAnserResult(bool anserResult)
     {
            anserTimeRange = (KMHH_PlayerInputManager.RecordAnserTime());

            if(anserResult){
                if ((anserTimeRange <= timeJudgeRange[3])){  //成功だが3秒以上かかっててもだめ
                comboCountNum++;　　//3秒以内はコンボ加算
                }
                else{
                comboCountNum = 1;
                }
            }else　//回答が不正解だったらコンボリセット
            {
                comboCountNum = 0;
            }

            if(comboCountNum >= maxCombo)　　//もしコンボが最大だったら
            {
                maxCombo = comboCountNum; //マックスコンボの更新
            }

            GetScore(anserResult);

        scoreText.text = "Score:" + totalScore;
        comboText.text = comboCountNum.ToString() + " Combo";
    }

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 現状でのスコアをゲッツ
    /// </summary>
    /// <returns>コンボによる倍率をかけた値を返す</returns>
    public static void GetScore(bool anserResult)
    {

       if ((anserTimeRange <= timeJudgeRange[0]) && (anserResult))
        {
            coefficient = timeJudgeValue[0];
            Debug.Log("Excellent!" + timeJudgeValue[0]);
            scoreExcellent += 1;

        }
       else if ((anserTimeRange > timeJudgeRange[0]) && (anserTimeRange <= timeJudgeRange[1]) && (anserResult))
        {
            coefficient = timeJudgeValue[1];
            Debug.Log("Great" + timeJudgeValue[1]);
            scoreGreat += 1;

        }
        else if ((anserTimeRange > timeJudgeRange[1]) && (anserTimeRange <= timeJudgeRange[2]) && (anserResult))
        {
            coefficient = timeJudgeValue[2];
            Debug.Log("Good" + timeJudgeValue[2]);
            scoreGood += 1;

        }
        else if ((anserTimeRange > timeJudgeRange[2]) && (anserTimeRange <= timeJudgeRange[3]) && (anserResult))
        {
            coefficient = timeJudgeValue[3];
            Debug.Log("NotGood");
            scoreNotGood += 1;

        }
        else if ((anserTimeRange > timeJudgeRange[3]) && (anserTimeRange <= timeJudgeRange[4]) && (anserResult))
        {
            coefficient = timeJudgeValue[4];
            Debug.Log("Bad");
            scoreBad += 1;

        }

        //回答時間が指定時間より長かったら
        else if (anserTimeRange > timeJudgeRange[4])
        {
            coefficient = timeJudgeValue[5];

            Debug.Log("miss");

            comboCountNum = 0;

            scoreMiss += 1;

        }

        if (comboCountNum == 0){
        }
        else
        {
            totalScore += baseScore * coefficient * comboCountNum;

        }
    }




}
