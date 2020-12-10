using System.Collections;
using UnityEngine;
using System.Timers;
using UnityEngine.UI; //テキスト使うなら必要

using System.IO;
using System.Collections.Generic;
using System.Runtime;

public class KMHH_CharaAnimationManager : MonoBehaviour
{
    public GameObject KMHH_CharaObj;
    [SerializeField] static Animator KMHH_CharaAnimator; //キャラアニメーター
    public static int randomNum = 0; //ランダムでポーズ選択用 int
    public static string NumID;    //↑ランダム数値を文字変更

    public static KmhhCharacterInfoAll kmhhCInfoAll = new KmhhCharacterInfoAll();　//キャラ情報全部クラスから継承

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
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///開始時
    /// </summary>
    /// <returns></returns> 
    public void Start(){
        GetCharaJSON();
        
        KMHH_CharaObj = GameObject.Find("ThendBear");

        KMHH_CharaAnimator = KMHH_CharaObj.GetComponentInChildren<Animator>();
/*    
        Debug.Log("id:"+ (kmhhCInfoAll.KmhhCharaPoseArray[randomNum].id).PadLeft(3,'0'));
        Debug.Log("eye:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomNum].eye);
        Debug.Log("hand_LSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomNum].hand_LSide);
        Debug.Log("hand_RSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomNum].hand_RSide);
        Debug.Log("leg_LSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomNum].leg_LSide);
        Debug.Log("leg_RSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomNum].leg_RSide);
*/
    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///アニメーションステートを変える
    /// </summary>
    /// <returns></returns> 
    public static void ChangeCharaState()
    {
        randomNum = UnityEngine.Random.Range(0, 7);　　//0～用意した分でランダム数値取り出し

/*      //情報テキスト更新
        partsInfoText.text = ("PartsInfo"+'\n'+
            "id:"+ (kmhhCInfoAll.KmhhCharaPoseArray[randomNum].id).PadLeft(3,'0') +'\n'+
            "eye:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomNum].eye +'\n'+
            "hand_LSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomNum].hand_LSide +'\n'+
            "hand_RSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomNum].hand_RSide +'\n'+
            "leg_LSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomNum].leg_LSide +'\n'+
            "leg_RSide:"+ kmhhCInfoAll.KmhhCharaPoseArray[randomNum].leg_RSide
            );
*/
        NumID = randomNum.ToString();　//作ったランダム数値を文字列化していれる
//       Debug.Log("armature|anm_kmhh_" +(NumID.PadLeft(3,'0'))+"_in");
//        Debug.Log("armature|anm_kmhh_" +(NumID.PadLeft(3,'0'))+"_lp");
        //秒待つ
        //yield return new WaitForSeconds(0.5f);
        //Idle -> SampleAnimation1 に遷移
        KMHH_CharaAnimator.CrossFadeInFixedTime("armature|anm_kmhh_" +(NumID.PadLeft(3,'0'))+"_in", 0.125f);

        //秒待つ
        //yield return  new WaitForSeconds(0.5f);
        KMHH_CharaAnimator.CrossFadeInFixedTime("armature|anm_kmhh_" +(NumID.PadLeft(3,'0'))+"_lp", 0.25f);


        Debug.Log("出題 IDナンバー:"+NumID.PadLeft(3,'0'));
        //1秒待つ
        //yield return new WaitForSeconds(0f);

        //SampleAnimation1 -> SampleAnimation2　に遷移


        //_animator.CrossFadeInFixedTime("armature|anm_kmhh_000_lp", 0);
        
        //Time.timeScale += 0.5f;
    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///アニメーションステートを変える 待機
    /// </summary>
    /// <returns></returns> 
    public static void  ChangeCharaStateToIdle()
    {

        randomNum = UnityEngine.Random.Range(0, 2);

        NumID = randomNum.ToString();
        Debug.Log("アイドルIDナンバー:"+NumID.PadLeft(3,'0'));

        KMHH_CharaAnimator.CrossFadeInFixedTime("armature|anm_kmhh_idle_" +(NumID.PadLeft(3,'0')), 0.25f);

        //yield return new WaitForSeconds(0f);

        
    }
////////////////////////////////////////////////////////////////////   
////////////////////////////////////////////////////////////////////
    /// <summary>
    ///オンマウスで説明でる
    /// </summary>
    /// <returns></returns> 
/*    public void Update()
    {

        countTime += Time.deltaTime;

        //Debug.Log(countTime);

        if((countTime > idleSpan) && (idleflag == false)){
            idleflag = !idleflag;
            StartCoroutine("ChangeCharaState");
            countTime = 0f;
            
            }
        else if((countTime > span) && (idleflag == true)){
            idleflag = !idleflag;
            StartCoroutine("ChangeCharaStateToIdle");
            countTime = 0f;
        }
        
        countTimeText.text = countTime.ToString("f1");

    }*/
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
}