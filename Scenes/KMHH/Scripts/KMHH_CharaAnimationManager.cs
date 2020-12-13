using System.Collections;
using UnityEngine;
using System.Timers;
using UnityEngine.UI; //テキスト使うなら必要

using System.IO;
using System.Collections.Generic;
using System.Runtime;
using System;  //enumつかう
using static KMHH_TimeManager;
public class KMHH_CharaAnimationManager : MonoBehaviour
{
    public GameObject KMHH_CharaObj;
    [SerializeField] public static Animator KMHH_CharaAnimator; //キャラアニメーター
    public static int randomPoseNum = 0; //ランダムでポーズ選択用 int
    public static string NumID;    //↑ランダム数値を文字変更

    public static KmhhCharacterInfoAll kmhhCInfoAll = new KmhhCharacterInfoAll();　//キャラ情報全部クラスから継承

    //ボディパーツ配列
    public static string[] setRandomBodyPart = {"eye","hand_LSide","hand_RSide","leg_LSide","leg_RSide"};
    public static int randomBodyPartNum;
    public static string selectedBodyParts;
    public static GameObject bodyPartsObj_Debug;
    public static Text bodyPartsText_Debug;

    public static string resultCollectAnser; //正解パーツ入れ

    public static GameObject partsInfoTextObj;
    public static Text partsInfoText;

    public static GameObject indicateBodyPartsSpeechBubble;
    public static GameObject indicateBodyPartEye;
    public static GameObject indicateBodyPartLsideHand;
    public static GameObject indicateBodyPartRsideHand;
    public static GameObject indicateBodyPartLsideLeg;
    public static GameObject indicateBodyPartRsideLeg;



////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///開始時
    /// </summary>
    /// <returns></returns> 
    public void Start(){

        GetCharaJSON();
        indicateBodyPartsSpeechBubble = GameObject.Find("ui_KMHH_SpeechBubble");
        indicateBodyPartEye = GameObject.Find("ui_BodyPart_Eye");
        indicateBodyPartLsideHand = GameObject.Find("ui_BodyPart_LsideHand");
        indicateBodyPartRsideHand = GameObject.Find("ui_BodyPart_RsideHand");
        indicateBodyPartLsideLeg = GameObject.Find("ui_BodyPart_LsideLeg");
        indicateBodyPartRsideLeg = GameObject.Find("ui_BodyPart_RsideLeg");


        KMHH_CharaObj = GameObject.Find("ThendBear");
        KMHH_CharaAnimator = KMHH_CharaObj.GetComponentInChildren<Animator>();

        bodyPartsObj_Debug = GameObject.Find("Debug_BodyParts");
        bodyPartsText_Debug = bodyPartsObj_Debug.GetComponentInChildren<Text>();

        partsInfoTextObj = GameObject.Find("Debug_PartsInfo");
        partsInfoText = partsInfoTextObj.GetComponentInChildren<Text>();

    }

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///アニメーションステートを変える
    /// </summary>
    /// <returns></returns> 
    static void ChangeQuestionBodyParts()
    {
        randomBodyPartNum = UnityEngine.Random.Range(0, 4);　　//0～4用意した分でランダム数値取り出し
        indicateBodyPartsSpeechBubble.SetActive(true); //　吹き出し出す

        switch (randomBodyPartNum){
        case 0:
                    selectedBodyParts = "eye";
                    indicateBodyPartEye.SetActive(true);
        break;
        case 1:
                    selectedBodyParts = "hand_LSide";
                    indicateBodyPartLsideHand.SetActive(true);
        break;
        case 2:
                    selectedBodyParts = "hand_RSide";
                    indicateBodyPartRsideHand.SetActive(true);
        break;
        case 3:
                    selectedBodyParts = "leg_LSide";
                    indicateBodyPartLsideLeg.SetActive(true);
        break;
        case 4:
                    selectedBodyParts = "leg_RSide";
                    indicateBodyPartRsideLeg.SetActive(true);
        break;
        }


        bodyPartsText_Debug.text = "ぼでぃぱーつ\n選択待ち...";


    }

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///アニメーションステートを変える
    /// </summary>
    /// <returns></returns> 
    public static void hideBodyParts()
    {
        indicateBodyPartsSpeechBubble.SetActive(true); //　吹き出し出す
                    indicateBodyPartEye.SetActive(false);
                    indicateBodyPartLsideHand.SetActive(false);
                    indicateBodyPartRsideHand.SetActive(false);
                    indicateBodyPartLsideLeg.SetActive(false);
                    indicateBodyPartRsideLeg.SetActive(false);
    }




////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///アニメーションステートを変える
    /// </summary>
    /// <returns></returns> 
    public static void ChangeCharaState()
    {

         hideBodyParts();

        randomPoseNum = UnityEngine.Random.Range(0, 7);　　//0～用意した分でランダム数値取り出し

      //情報テキスト更新
        partsInfoText.text = ("PartsInfo"+'\n'+
            "id:"+ (kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].id).PadLeft(3,'0') +'\n'+
            "eye:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].eye +'\n'+
            "hand_LSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].hand_LSide +'\n'+
            "hand_RSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].hand_RSide +'\n'+
            "leg_LSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].leg_LSide +'\n'+
            "leg_RSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].leg_RSide
            );

        NumID = randomPoseNum.ToString();　//作ったランダム数値を文字列化していれる

        KMHH_CharaAnimator.CrossFadeInFixedTime("armature|anm_kmhh_" +(NumID.PadLeft(3,'0'))+"_in", 0.125f);

        /*
        inからlpに
        */

        KMHH_CharaAnimator.CrossFadeInFixedTime("armature|anm_kmhh_" +(NumID.PadLeft(3,'0'))+"_lp", 0.25f);


        //Debug.Log("出題 IDナンバー:"+NumID.PadLeft(3,'0'));
        
        switch (randomBodyPartNum){
        case 0:
                    selectedBodyParts = "eye";
                    resultCollectAnser = kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].eye;
        break;
        case 1:
                    selectedBodyParts = "hand_LSide";
                    resultCollectAnser = kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].hand_LSide;
        break;
        case 2:
                    selectedBodyParts = "hand_RSide";
                    resultCollectAnser = kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].hand_RSide;
        break;
        case 3:
                    selectedBodyParts = "leg_LSide";
                    resultCollectAnser = kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].leg_LSide;
        break;
        case 4:
                    selectedBodyParts = "leg_RSide";
                    resultCollectAnser = kmhhCInfoAll.KmhhCharaPoseArray[randomPoseNum].leg_RSide;
        break;
        }

        bodyPartsText_Debug.text = "ぼでぃぱーつ"+"\n"+
                                    randomBodyPartNum +"="+ selectedBodyParts +"\n"+
                                    "正解方角は："+resultCollectAnser+"!!";


    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///アニメーションステートを変える 待機
    /// </summary>
    /// <returns></returns> 
    public static void  ChangeCharaStateToIdle()
    {
        ChangeQuestionBodyParts();　//お題の体パーツを決める
        
        randomPoseNum = UnityEngine.Random.Range(0, 5);

        NumID = randomPoseNum.ToString();
        //Debug.Log("アイドルIDナンバー:"+NumID.PadLeft(3,'0'));

        KMHH_CharaAnimator.CrossFadeInFixedTime("armature|anm_kmhh_idle_" +(NumID.PadLeft(3,'0')), 0.25f);

        
    }

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///アニメーションステートを変える 待機
    /// </summary>
    /// <returns></returns> 
    public static void ChangeCharaStateToEmotion(bool anserResultEmo)
    {
        //Debug.Log("えも");

        if(anserResultEmo){
        KMHH_CharaAnimator.CrossFadeInFixedTime("armature|anm_kmhh_Emo_Excellent", 0.25f);
        }
        else{
        KMHH_CharaAnimator.CrossFadeInFixedTime("armature|anm_kmhh_Emo_No", 0.25f);

        }

    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///アニメーションステートを変える 待機
    /// </summary>
    /// <returns></returns> 
    public static void ChangeCharaStateToFinish()
    {
        //Debug.Log("ばいばい");
        KMHH_CharaAnimator.CrossFadeInFixedTime("armature|anm_kmhh_Emo_Bye", 0.25f);
   

    }
////////////////////////////////////////////////////////////////////   
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///オンマウスで説明でる
    /// </summary>
    /// <returns></returns> 
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///キャラ情報のJSONの内容と揃えたやつクラス
    /// </summary>
    /// <returns></returns>  
[System.Serializable]
    public class KmhhCharaPoseInfo{
        public string id;
        public string eye;
        public string hand_LSide;
        public string hand_RSide;
        public string leg_LSide;
        public string leg_RSide;

    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// キャラ情報の配列変数。ここのメンバ名がJSONの名前と揃ってること
    /// </summary>
    /// <returns></returns>   
[System.Serializable]
public class KmhhCharacterInfoAll
{
    public KmhhCharaPoseInfo[] KmhhCharaPoseArray;
}


////////////////////////////////////////////////////////////////////
    /// <summary>
    ///JSONデータ　引っ張ってくる
    /// </summary>
    /// <returns></returns> 
    public void GetCharaJSON() {

        //ファイルのロード（ポーズ情報）
        string jsonCharaData = File.ReadAllText(Application.dataPath + "/Resources/JSONData/KMHH_CharaPoseData.json");

        //配列メンバ持ってるクラスの継承　
        kmhhCInfoAll = JsonUtility.FromJson<KmhhCharacterInfoAll>(jsonCharaData);
    }
}