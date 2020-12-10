using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static KMHH_TimeManager; //時間管理スクリプトを使う
using static KMHH_CharaAnimationManager;

public class KMHH_PlayerInputManager : MonoBehaviour
{
    //ボディパーツ配列
    public static string[] setRandomBodyPart = {"eye","hand_LSide","hand_RSide","leg_LSide","leg_RSide"};
    public int numOfBodyPart = 0; //　↑の変数の番号

    public string suffixBodyPartsName;　//末尾にいれるパーツ

    public Dictionary<string, int> bodyPartDic = new Dictionary<string, int>();



    /// <summary>
    /// 左ボタン押されたときの成否判断処理
    /// </summary>
    /// <returns></returns>   
    public void JudgeL()   //外から左ボタンが押されたときに実行
    {
        //　セットされてるパーツがどれか
        suffixBodyPartsName = setRandomBodyPart[numOfBodyPart];
        
        //Random数値のポーズの、セットされてるボディパーツに該当する結果が　L　かどうか


        if (KMHH_CharaAnimationManager.kmhhCInfoAll.KmhhCharaPoseArray[1].hand_LSide == "L"){

            Debug.Log("せいかい");
        }

            if(KMHH_TimeManager.getQuestionStatus()){
                Debug.Log("ひだり");
            }
            else{
                Debug.Log("受け付けませーん");
            }


 /*/★       if(KMHH_TimeManager.giveQuestionTime)//出題状態だったら
       {
 //★
           //出題のパーツIDを取得

           //押した　L　の情報が、パーツ情報とあっているか

        }

 ★
        else{//出題状態じゃなかったら
            return;
        }
*/
        //もし出題状態だったら押せる


        //出題状態じゃなかったら
       // if (KMHH_TimeManager.getQuestionStatus() == false) //出題状態じゃない限り、何も実行しないで戻る
       // {
            //Debug.Log("りたーん");
       //     return;
       // }
        //if (押したボタンと == true)
       // {
            //Debug.Log("左！あたり！");
            //正解プレハブを再生
            //正解処理を実行
        //}
        //else
        //{
            //Debug.Log("左じゃないよハズレだよ！！");
            //不正解プレハブを再生
            //不正解処理を実行
        //}
        //出題状態をやめて、アニメーターの状態をIdleに戻す。
    }
    public void JudgeUL()   //外から左ボタンが押されたときに実行
    {
        

        if (KMHH_CharaAnimationManager.kmhhCInfoAll.KmhhCharaPoseArray[1].hand_LSide == "UL"){

        }
        else{
            Debug.Log("ふせいかい");
        }

            if(KMHH_TimeManager.getQuestionStatus()){
                Debug.Log("ひだりうえ");
            }
            else{
                Debug.Log("受け付けませーん");
            }
    } 
    public void JudgeDL()   //外から左ボタンが押されたときに実行
    {
            if(KMHH_TimeManager.getQuestionStatus()){
                Debug.Log("ひだりした");
            }
            else{
                Debug.Log("受け付けませーん");
            }
    }  
    public void JudgeR()   //外から左ボタンが押されたときに実行
    {
            if(KMHH_TimeManager.getQuestionStatus()){
                Debug.Log("みぎ");
            }
            else{
                Debug.Log("受け付けませーん");
            }
    }  
    public void JudgeUR()   //外から左ボタンが押されたときに実行
    {
            if(KMHH_TimeManager.getQuestionStatus()){
                Debug.Log("みぎうえ");
            }
            else{
                Debug.Log("受け付けませーん");
            }
    }  
    public void JudgeDR()   //外から左ボタンが押されたときに実行
    {
            if(KMHH_TimeManager.getQuestionStatus()){
                Debug.Log("みぎした");
            }
            else{
                Debug.Log("受け付けませーん");
            }
    }  
    public void JudgeU()   //外から左ボタンが押されたときに実行
    {
            if(KMHH_TimeManager.getQuestionStatus()){
                Debug.Log("うえ");
            }
            else{
                Debug.Log("受け付けませーん");
            }
    }  
    public void JudgeD()   //外から左ボタンが押されたときに実行
    {
            if(KMHH_TimeManager.getQuestionStatus()){
                Debug.Log("した");
            }
            else{
                Debug.Log("受け付けませーん");
            }
    }


    /// <summary>
    ///ボディパーツのランダム選択
    /// </summary>
    /// <returns>配列に入れる数字</returns> 

public static int SetRandomBodyPart(){
    return(UnityEngine.Random.Range(0, 4));
}



}
