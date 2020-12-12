using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //テキスト使うなら必要
using TMPro; //TextMeshPro用

using static KMHH_TimeManager; //時間管理スクリプトを使う
using static KMHH_CharaAnimationManager;

public class KMHH_PlayerInputManager : MonoBehaviour
{
    public int numOfBodyPart = 0; //　↑の変数の番号

    public string suffixBodyPartsName;　//末尾にいれるパーツ

    public Dictionary<string, int> bodyPartDic = new Dictionary<string, int>();


    //デバッグ　正否文字表示
    public static GameObject Debug_AnserResultObj;
    public TextMeshProUGUI Debug_AnserResultText;

    void Start()
    {

        Debug_AnserResultObj = GameObject.Find("Debug_AnserResult");
        Debug_AnserResultText = Debug_AnserResultObj.GetComponentInChildren<TextMeshProUGUI>();
        Debug_AnserResultText = Debug_AnserResultObj.GetComponentInChildren<TextMeshProUGUI>();
    }
    
public void AnserResultCollect(){
    Debug_AnserResultText.text ="SEIKAI";
    Invoke("ResetAnserResultText",1.0f);

    KMHH_TimeManager.switchQuestion = false;
}

public void AnserResultIncorrect(){
    Debug_AnserResultText.text ="CHIGAU";

    Invoke("ResetAnserResultText",1.0f);
    KMHH_TimeManager.switchQuestion = false;
    
}
public void ResetAnserResultText(){

    Debug_AnserResultText.text ="";
    KMHH_TimeManager.setIdle();
}


    /// <summary>
    /// てすとてすとてすとボタン押されたときの成否判断処理
    /// </summary>
    /// <returns></returns>   
    public void JudgeTest()   //外から左ボタンが押されたときに実行
    {
        
            if(KMHH_TimeManager.questionStatus){
            Debug.Log("てすと");

            if(KMHH_TimeManager.switchQuestion){
            KMHH_TimeManager.setIdle();
            }

        }

    }

    /// <summary>
    /// 左ボタン押されたときの成否判断処理
    /// </summary>
    /// <returns></returns>   
    public void JudgeL()   //外から左ボタンが押されたときに実行
    {

        //Random数値のポーズの、セットされてるボディパーツに該当する結果が　L　かどうか

        if(KMHH_TimeManager.questionStatus){
            if (KMHH_CharaAnimationManager.resultCollectAnser == "L"){


                Debug.Log("せいかい");
                AnserResultCollect();
            }
            else{
                Debug.Log("ふせいかい");
                AnserResultIncorrect();
            }
        
        }
        else{
        }
    }
    public void JudgeUL()   //外から左ボタンが押されたときに実行
    {
        //Random数値のポーズの、セットされてるボディパーツに該当する結果が　UL　かどうか

        if(KMHH_TimeManager.questionStatus){
            if (KMHH_CharaAnimationManager.resultCollectAnser == "UL"){


                Debug.Log("せいかい");
                AnserResultCollect();
            }
            else{
                Debug.Log("ふせいかい");
                AnserResultIncorrect();
            }
        
        }
        else{
        }
    }
    public void JudgeDL()   //外から左ボタンが押されたときに実行
    {
        //Random数値のポーズの、セットされてるボディパーツに該当する結果が　DL　かどうか

        if(KMHH_TimeManager.questionStatus){
            if (KMHH_CharaAnimationManager.resultCollectAnser == "DL"){


                Debug.Log("せいかい");
                AnserResultCollect();
            }
            else{
                Debug.Log("ふせいかい");
                AnserResultIncorrect();
            }
        
        }
        else{
        }
    }  
    public void JudgeR()   //外から左ボタンが押されたときに実行
    {
        //Random数値のポーズの、セットされてるボディパーツに該当する結果が　R　かどうか

        if(KMHH_TimeManager.questionStatus){
            if (KMHH_CharaAnimationManager.resultCollectAnser == "R"){


                Debug.Log("せいかい");
                AnserResultCollect();
            }
            else{
                Debug.Log("ふせいかい");
                AnserResultIncorrect();
            }
        
        }
        else{
        }
    } 
    public void JudgeUR()   //外から左ボタンが押されたときに実行
    {
        //Random数値のポーズの、セットされてるボディパーツに該当する結果が　UR　かどうか

        if(KMHH_TimeManager.questionStatus){
            if (KMHH_CharaAnimationManager.resultCollectAnser == "UR"){


                Debug.Log("せいかい");
                AnserResultCollect();
            }
            else{
                Debug.Log("ふせいかい");
                AnserResultIncorrect();
            }
        
        }
        else{
        }
    }
    public void JudgeDR()   //外から左ボタンが押されたときに実行
    {
        //Random数値のポーズの、セットされてるボディパーツに該当する結果が　DR　かどうか

        if(KMHH_TimeManager.questionStatus){
            if (KMHH_CharaAnimationManager.resultCollectAnser == "DR"){


                Debug.Log("せいかい");
                AnserResultCollect();
            }
            else{
                Debug.Log("ふせいかい");
                AnserResultIncorrect();
            }
        
        }
        else{
        }
    } 
    public void JudgeU()   //外から左ボタンが押されたときに実行
    {
        //Random数値のポーズの、セットされてるボディパーツに該当する結果が　U　かどうか

        if(KMHH_TimeManager.questionStatus){
            if (KMHH_CharaAnimationManager.resultCollectAnser == "U"){


                Debug.Log("せいかい");
                AnserResultCollect();
            }
            else{
                Debug.Log("ふせいかい");
                AnserResultIncorrect();
            }
        
        }
        else{
        }
    }  
    public void JudgeD()   //外から左ボタンが押されたときに実行
    {
        //Random数値のポーズの、セットされてるボディパーツに該当する結果が　D　かどうか

        if(KMHH_TimeManager.questionStatus){
            if (KMHH_CharaAnimationManager.resultCollectAnser == "D"){


                Debug.Log("せいかい");
                AnserResultCollect();
            }
            else{
                Debug.Log("ふせいかい");
                AnserResultIncorrect();
            }
        
        }
        else{
        }
    } 


}
