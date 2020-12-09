using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //テキスト使うなら必要？

//using static KMHH_ScoreManager; //スコアスクリプトを使う
//using static KMHH_CharaAnimationManager; //キャラのアニメーション管理を使う

using UnityEngine.SceneManagement; //シーン切り替え

/// <summary>
/// ゲーム内の時間にまつわる操作を管理する
/// </summary>
/// <returns></returns> 
public class KMHH_TimeManager : MonoBehaviour
{
    public float getDeltaTime = 0; //時間経過取得の大元
    public float gameCurrentTime = 0.0f; //現在の時間
    float gameStandbyTime = 1.0f; //スタンバイの時間
    public static float gameTimePast = 0.0f; //時間の経過
    public static int  gameTimePastInt = 0;  //時間の経過のInt化
    public static float gameFinishTime = 0.0f; //ゲーム終了時の時間を記録
    float kmhhQuestionSpan  = 5.0f;  //出題時間のスパン
    float kmhhIdleSpan  = 2.0f;  //　キャラクターの待機状態のスパン
    public float answerTime = 0.0f; //回答時のカレントタイム。timePastとの差分で答えるまでにかかった時間を取る

    public float stopWatchQuestionCurrentTime = 0.0f; //出題時とアイドル時の時間記録

    public static bool gameStart = false; //ゲームがスタートしているかの判定
    public static bool gameFinish = false; //ゲームが終了しているかの判定
    public bool questionStatus = false; //出題状態かどうか

    public bool giveQuestion = false; //出題フラグ
    public bool breakQuestion = true; //休憩フラグ

    public static int questionNumOfTimes = 0;  //出題回数

    public float gameSetTime = 100.0f;　//ゲームが終了するまでの時間
    
    GameObject upDateGameTimerObj; //ゲームタイマーオブジェクト入れ
    public Text upDateGameTimerText; //ゲームタイマー数値をいれるテキスト

    GameObject debugGameTimerObj; //デバッグ用タイマーオブジェクト入れ
    Text debugGameTimeText; //デバッグ用タイマーオブジェクトテキスト入れ

////////////////////////////////////////////////////////////////////
//カウントダウン関連変数
    public GameObject countDownEffObj_3; //カウントダウン3エフェクトプレハブ入れ
    public GameObject countDownEffObj_2; //カウントダウン2エフェクトプレハブ入れ
    public GameObject countDownEffObj_1; //カウントダウン1エフェクトプレハブ入れ
    public GameObject countDownEffObj_0; //カウントダウン0エフェクトプレハブ入れ
    public GameObject countDownEffObj_Fin; //カウントダウンフィニッシュエフェクトプレハブ入れ
    public GameObject GameSceneChangeWallObj; //カウントダウン画面遷移エフェクトプレハブ入れ
    bool countDownTrigger3 = false; //カウントダウンのトリガー
    bool countDownTrigger2 = false; //カウントダウンのトリガー
    bool countDownTrigger1 = false; //カウントダウンのトリガー

////////////////////////////////////////////////////////////////////

    //public GameObject kmhh_CAM_Obj;

////////////////////////////////////////////////////////////////////
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.25f;
        questionNumOfTimes = 0;

        upDateGameTimerObj = GameObject.Find("ui_GameTimer");　//ゲーム時間経過用オブジェクト拾ってくるぜ  
    //    upDateGameTimerText = upDateGameTimerObjKMHH_CharaObj(); //ゲーム時間経過用Textコンポーネント拾ってくるぜ  
      
        countDownEffObj_3 = GameObject.Find("eff_KMHH_CountDown_3");
        countDownEffObj_2 = GameObject.Find("eff_KMHH_CountDown_2");
        countDownEffObj_1 = GameObject.Find("eff_KMHH_CountDown_1");
        countDownEffObj_0 = GameObject.Find("eff_KMHH_CountDown_Start");
        countDownEffObj_Fin = GameObject.Find("eff_KMHH_CountDown_Finish");
        GameSceneChangeWallObj = GameObject.Find("eff_GameSceneChangeWall");

        getDeltaTime = 0.0f;
        
        upDateGameTimerText.text = gameSetTime.ToString(); //　時間制限をテキスト更新


        debugGameTimerObj　= GameObject.Find("debugCountTimer");

        debugGameTimeText = debugGameTimerObj.GetComponentInChildren<Text>();

        //kmhh_CAM_Obj = GameObject.Find("KMHH_CharaAnimationManager_Obj");

        //KMHH_CharaAnimationManager kmhh_CAM = new KMHH_CharaAnimationManager();


        giveQuestion =true;
    }

////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ゲームの時間進行いろいろ。カウントダウンもやっちゃう 
    /// </summary>
    /// <returns></returns>   
    // Update is called once per frame
    public void FixedUpdate()
    {


        debugGameTimeText.text =
        "giveQuestion:"+ giveQuestion  + "\n" +
        "breakQuestion:"+ breakQuestion  + "\n" +
        "stopWatchQuestionCurrentTime:"+ stopWatchQuestionCurrentTime.ToString("f2")  + "\n" +
        "出題時間のスパン" + kmhhQuestionSpan  + "\n" +
        "アイドルのスパン" + kmhhIdleSpan  + "\n" +
        "getDeltaTime:"+ getDeltaTime.ToString("f2")  + "\n" +
        "gameTimePast:" + gameTimePast.ToString("f2") +  "\n" +
        "answerTime" + answerTime.ToString("f2")  + "\n" +
        "gameCurrentTime:ゲームの現在の時間"+((gameCurrentTime).ToString("f2") + "\n"+
        "questionStatus:"+ questionStatus  + "\n"+
        "gameStart:"+gameStart  + "\n"+
        "giveQuestion:"+giveQuestion  + "\n"+
        "questionNumOfTimes:出題回数:" +questionNumOfTimes   + "\n"
        );

       getDeltaTime += Time.deltaTime;

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
        //カウントダウン中
        if ((gameStart == false)&&(gameFinish == false))
        {
            if(getDeltaTime < 0.1f){
                if(breakQuestion == true){
                    setIdle();
                }
                countDownEffObj_3.SetActive (false);            
                countDownEffObj_2.SetActive (false);
                countDownEffObj_1.SetActive (false);
                countDownEffObj_0.SetActive (false);
                countDownEffObj_Fin.SetActive (false);
                GameSceneChangeWallObj.SetActive (false);
            }
            if((getDeltaTime >= gameStandbyTime)&&(getDeltaTime < gameStandbyTime+1.0f)){
                if(countDownTrigger3 == false){
                    if(getDeltaTime != gameTimePast){
                    countDownTrigger3 = true;
                    Debug.Log("3");
                countDownEffObj_3.SetActive (true);
                    }
                }
            }
            if((getDeltaTime >= gameStandbyTime+1.0f)&&(getDeltaTime < gameStandbyTime+2.0f)){
                if(countDownTrigger2 == false){  
                    if(getDeltaTime != gameTimePast){
                    countDownTrigger2 = true;
                    Debug.Log("2");
                countDownEffObj_3.SetActive (false);
                countDownEffObj_2.SetActive (true);
                    }
                }
            }            
            if((getDeltaTime >= gameStandbyTime+2.0f)&&(getDeltaTime < gameStandbyTime+3.0f)){
                if(countDownTrigger1 == false){  
                    if(getDeltaTime != gameTimePast){
                    countDownTrigger1 = true;
                    Debug.Log("1");
                countDownEffObj_2.SetActive (false);
                countDownEffObj_1.SetActive (true);
                    }
                }
            }            
            if(getDeltaTime >= gameStandbyTime+3.0f){
                giveQuestion = true; //出題フラグもたてる
                Debug.Log("Start!");
                countDownEffObj_1.SetActive (false);
                countDownEffObj_0.SetActive (true); 
                answerTime = 0.0f; 
                GameStartTrigger();
                return;    
            }
            gameTimePast = getDeltaTime; //経過時間として現在の時間を上書く
        }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
        //ゲーム開始。ゲームが終わってないとき,ゲーム中の時間を記録
        if ((gameStart == true)&&(gameFinish == false))
        {
            gameCurrentTime += Time.deltaTime; //ゲーム時間を更新

            gameTimePast = gameCurrentTime; //経過時間として現在の時間を上書く
            gameTimePastInt = (int)gameTimePast; //↑整数化
            upDateGameTimerText.text = (((gameSetTime - gameTimePastInt).ToString()).PadLeft(2, '0')); //時間文字列更新


            //↓制限時間を過ぎたらゲーム開始をfalse,ゲーム終了をtrue
            if (gameTimePast > gameSetTime)
            {
                Debug.Log("しゅうりょう");
                questionStatus = false;
                gameFinishTime = gameTimePast; //ゲーム終了時の時間を記録
                GameFinish();
                return;
            }

////////////////////////////////////////////////////////////////////
//出題状態に移行する
            if(giveQuestion == true){

                    giveQuestion = false; //出題状態OFF

                if(questionStatus == false){
                    
                    questionStatus = true;　//出題中判定ON

                    setQuestion();
                    
                }
            }

//出題待機状態に移行する
            if(breakQuestion == true){

                breakQuestion = false;
                questionStatus = false;　//出題中判定OFF


                setIdle();
            }

//出題状態のとき

            if ((questionStatus == true)&&(breakQuestion == false)){
                
                answerTime += Time.deltaTime; //答えるまでの時間を更新

                if(answerTime > kmhhQuestionSpan) //出題状態で、出題制限時間超過したら
                {
                    answerTime = 0.0f;
                    questionStatus = false; //出題状態OFF
                    breakQuestion = true; //出題状態を解除して、アイドルモーションに戻す
                    giveQuestion = false;
                    
                    Debug.Log(answerTime +" >" + kmhhQuestionSpan);
                }
            }


//出題状態じゃないとき
            if ((questionStatus == false)&&(giveQuestion == false)){
                
                answerTime += Time.deltaTime; //答えるまでの時間を更新

                if(answerTime > kmhhIdleSpan){//非出題状態で、非出題制限時間超過したら
                    answerTime = 0.0f;
                    giveQuestion = true; //出題状態ON
                    Debug.Log(answerTime +" >" + kmhhIdleSpan);
                }    
            }
        }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
        //ゲームが終わったとき,終了の処理        
        if ((gameStart == false)&&(gameFinish == true))
        {
                countDownEffObj_Fin.SetActive (true);

        }
    } //Update()ここまで
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ゲーム開始フラグ建てる
    /// </summary>
    /// <returns></returns> 
    public void GameStartTrigger()
    {
        Debug.Log("げーむすたーと");
        gameTimePast = 0;
        questionNumOfTimes = 0;
        gameStart = true;　//ゲーム開始状態にする
        Invoke("CountDownObjHide",1.5f);   // カウントダウン全部消す 

    }
////////////////////////////////////////////////////////////////////   
    /// <summary>
    /// カウントダウン全部消す 
    /// </summary>
    /// <returns></returns> 
    public void CountDownObjHide()
    {
        countDownEffObj_3.SetActive (false);            
        countDownEffObj_2.SetActive (false);
        countDownEffObj_1.SetActive (false);
        countDownEffObj_0.SetActive (false);
    }
////////////////////////////////////////////////////////////////////
    public void MoveToResult()
    {
        //SceneManager.LoadScene("KMHH_Result");
    }
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ゲームが終わったとき,終了の処理 
    /// </summary>
    /// <returns></returns>   
    public void GameFinish(){
    
            gameStart = false;
            gameFinish = true;

            Debug.Log("ゲームとまった");

            GameSceneChangeWallObj.SetActive (true);
            
        Invoke("DerayGameTitleLoadRun",5.0f);   // カウントダウン全部消す 
    }
    public void DerayGameTitleLoadRun()
    {
        SceneManager.LoadScene("KMHH_Result");
    }
////////////////////////////////////////////////////////////////////

    public void setQuestion()
    {
        questionStatus = true;　//出題中判定ON
        breakQuestion = false; //待機判定OFF
        questionNumOfTimes = questionNumOfTimes + 1;　//出題回数増やす

        Debug.Log("すてーと選びにいこかな"+questionStatus);
        
        KMHH_CharaAnimationManager.ChangeCharaState(); //キャラのステート変えるやつ
    }

    public void setIdle()
    {                
        questionStatus = false;　//出題中判定OFF
        breakQuestion = false; //待機判定OFF
        Debug.Log("あいどるすてーと選びにいこかな");

        KMHH_CharaAnimationManager.ChangeCharaStateToIdle(); //キャラのステート変えるやつ　待機

    }

}
