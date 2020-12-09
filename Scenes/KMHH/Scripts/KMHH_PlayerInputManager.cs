using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static KMHH_TimeManager; //時間管理スクリプトを使う

public class KMHH_PlayerInputManager : MonoBehaviour
{

    /// <summary>
    /// 左ボタン押されたときの成否判断処理
    /// </summary>
    /// <returns></returns>   
    public void JudgeL()   //外から左ボタンが押されたときに実行
    {
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

        Debug.Log("ひだり");
       // if (KMHH_TimeManager.questionStatus == false) //出題状態じゃない限り、何も実行しないで戻る
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
        Debug.Log("ひだりうえ");
    } 
    public void JudgeDL()   //外から左ボタンが押されたときに実行
    {
        Debug.Log("ひだりした");
    }  
    public void JudgeR()   //外から左ボタンが押されたときに実行
    {
        Debug.Log("みぎ");
    }  
    public void JudgeUR()   //外から左ボタンが押されたときに実行
    {
        Debug.Log("みぎうえ");
    }  
    public void JudgeDR()   //外から左ボタンが押されたときに実行
    {
        Debug.Log("みぎした");
    }  
    public void JudgeU()   //外から左ボタンが押されたときに実行
    {
        Debug.Log("うえ");
    }  
    public void JudgeD()   //外から左ボタンが押されたときに実行
    {
        Debug.Log("した");
    }  



}
