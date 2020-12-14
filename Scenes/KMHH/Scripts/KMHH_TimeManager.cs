using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //テキスト使うなら必要？

//using static KMHH_ScoreManager; //スコアスクリプトを使う
//using static KMHH_CharaAnimationManager; //キャラのアニメーション管理を使う
using static KMHH_PlayerInputManager;

using UnityEngine.SceneManagement; //シーン切り替え


/// <summary>
/// ゲーム内の時間にまつわる操作を管理する
/// </summary>
/// <returns></returns> 
public class KMHH_TimeManager : MonoBehaviour
{

    public bool switchStartMethod = true;
    public static float getDeltaTime = 0.0f; //時間経過取得の大元
    public static float gameCurrentTime = 0.0f; //現在の時間
    public float gameStandbyTime = 1.0f; //スタンバイの時間
     float gameTimePast = 0.0f; //時間の経過
     int  gameTimePastInt = 0;  //時間の経過のInt化
     float gameFinishTime = 0.0f; //ゲーム終了時の時間を記録
    public static float anserTime = 0.0f; //回答時のカレントタイム。
    public static float recordAnserTime = 0.0f; //回答時のカレントタイム。
    
   
     float idleTime = 0.0f; //待機時のカレントタイム。
   

    public static bool gameStart = false; //ゲームがスタートしているかの判定
    public static bool gameFinish = false; //ゲームが終了しているかの判定

    public  float editTimeScale = 1.0f;

    public static bool questionStatus = false; //出題状態かどうか
    public float kmhhQuestionSpan　= 5.0f;  //出題時間のスパン
    public float kmhhIdleSpan = 5.0f;  //　キャラクターの待機状態のスパン

    public static bool switchIdle = false; //条件 Idleのスイッチ
    public static bool switchQuestion = false; //条件 Questionのスイッチ



    public static int questionNumOfTimes = 0;  //出題回数

    public float gameSetTime = 100.0f;　//ゲームが終了するまでの時間
    
    GameObject upDateGameTimerObj; //ゲームタイマーオブジェクト入れ
    public Text upDateGameTimerText; //ゲームタイマー数値をいれるテキスト

    GameObject debugGameTimerObj; //デバッグ用タイマーオブジェクト入れ
    Text debugGameTimeText; //デバッグ用タイマーオブジェクトテキスト入れ

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
//カウントダウン関連変数
     GameObject countDownEffObj_3; //カウントダウン3エフェクトプレハブ入れ
     GameObject countDownEffObj_2; //カウントダウン2エフェクトプレハブ入れ
     GameObject countDownEffObj_1; //カウントダウン1エフェクトプレハブ入れ
     GameObject countDownEffObj_0; //カウントダウン0エフェクトプレハブ入れ
     GameObject countDownEffObj_Fin; //カウントダウンフィニッシュエフェクトプレハブ入れ
     GameObject GameSceneChangeWallObj; //カウントダウン画面遷移エフェクトプレハブ入れ
    bool countDownTrigger3 = false; //カウントダウンのトリガー
    bool countDownTrigger2 = false; //カウントダウンのトリガー
    bool countDownTrigger1 = false; //カウントダウンのトリガー

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////

    //public GameObject kmhh_CAM_Obj;

////////////////////////////////////////////////////////////////////
    // Start is called before the first frame update
    void Start()
    {
        getDeltaTime = 0.0f;
        Time.timeScale = 1.0f;
        questionNumOfTimes = 0;

  
        countDownEffObj_3 = GameObject.Find("eff_KMHH_CountDown_3");
        countDownEffObj_2 = GameObject.Find("eff_KMHH_CountDown_2");
        countDownEffObj_1 = GameObject.Find("eff_KMHH_CountDown_1");
        countDownEffObj_0 = GameObject.Find("eff_KMHH_CountDown_Start");
        countDownEffObj_Fin = GameObject.Find("eff_KMHH_CountDown_Finish");
        GameSceneChangeWallObj = GameObject.Find("eff_GameSceneChangeWall");


        upDateGameTimerObj = GameObject.Find("ui_GameTimer");　//ゲーム時間経過用オブジェクト拾ってくるぜ  
           
        upDateGameTimerText.text = gameSetTime.ToString(); //　時間制限をテキスト更新


        debugGameTimerObj　= GameObject.Find("Debug_CountTimer");

        debugGameTimeText = debugGameTimerObj.GetComponentInChildren<Text>();

        //kmhh_CAM_Obj = GameObject.Find("KMHH_CharaAnimationManager_Obj");

        //KMHH_CharaAnimationManager kmhh_CAM = new KMHH_CharaAnimationManager();

    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ゲームの時間進行いろいろ。カウントダウンもやっちゃう 
    /// </summary>
    /// <returns></returns>   
    // Update is called once per frame
    public void Update()
    {
        debugGameTimeText.text = "でばっぐ情報" +"\n" +
        "ゲーム経過時間:" + gameTimePast.ToString("f2") +  "\n" +
        "出題回数:" + questionNumOfTimes + "\n" +
        "出題状態:"+ questionStatus  + "\n"+
        "-----------------------\n" +
        "出題時間のスパン" + kmhhQuestionSpan  + "\n" +
        "出題スイッチ:"+ switchQuestion + "\n"+
        "回答時間" + anserTime.ToString("f2")  + "\n" +
        "-----------------------\n" +
        "アイドルのスパン" + kmhhIdleSpan  + "\n" +
        "待機スイッチ:"+ switchIdle + "\n"+
        "待機時間" + idleTime.ToString("f2")  + "\n";
///////////////////////////////////////////////////////////////////

        if((switchStartMethod==true)&&(gameFinish==false)){
            getDeltaTime = 0.0f;
            switchStartMethod = false;
            
            Time.timeScale = 1.0f;
            questionNumOfTimes = 0;


            countDownEffObj_3 = GameObject.Find("eff_KMHH_CountDown_3");
            countDownEffObj_2 = GameObject.Find("eff_KMHH_CountDown_2");
            countDownEffObj_1 = GameObject.Find("eff_KMHH_CountDown_1");
            countDownEffObj_0 = GameObject.Find("eff_KMHH_CountDown_Start");
            countDownEffObj_Fin = GameObject.Find("eff_KMHH_CountDown_Finish");
            GameSceneChangeWallObj = GameObject.Find("eff_GameSceneChangeWall");


            upDateGameTimerObj = GameObject.Find("ui_GameTimer");　//ゲーム時間経過用オブジェクト拾ってくるぜ  
                
            upDateGameTimerText.text = gameSetTime.ToString(); //　時間制限をテキスト更新


            debugGameTimerObj　= GameObject.Find("Debug_CountTimer");

            debugGameTimeText = debugGameTimerObj.GetComponentInChildren<Text>();

            //kmhh_CAM_Obj = GameObject.Find("KMHH_CharaAnimationManager_Obj");

            //KMHH_CharaAnimationManager kmhh_CAM = new KMHH_CharaAnimationManager();
        }

        getDeltaTime += Time.deltaTime;


////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
        //カウントダウン中
        if ((gameStart == false)&&(gameFinish == false))
        {
            if(getDeltaTime < 0.1f){
                gameParamReset();
                if(KMHH_CharaAnimationManager.KMHH_CharaAnimator.GetBool("goToIdle") == true){
                    setIdle();
                    KMHH_CharaAnimationManager.hideBodyParts();
                    KMHH_CharaAnimationManager.ChangeQuestionBodyParts();
                    KMHH_CharaAnimationManager.KMHH_CharaAnimator.SetBool("goToIdle",false);
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
                gameStart = true;　//ゲーム開始状態にする
                Debug.Log("げーむすたーと");
                gameTimePast = 0;
                questionNumOfTimes = 0;

                switchQuestion = true;

                countDownEffObj_1.SetActive (false);
                countDownEffObj_0.SetActive (true); 
                
                
                Invoke("CountDownObjHide",1.5f);   // カウントダウン全部消す
                setQuestion();

                Time.timeScale = editTimeScale; 
            }
            gameTimePast = getDeltaTime; //経過時間として現在の時間を上書く
        }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
        //ゲーム開始。ゲームが終わってないとき,ゲーム中の時間を記録
        if ((gameStart == true)&&(gameFinish == false))
        {
            gameCurrentTime += Time.deltaTime; //ゲーム時間を更新

            gameTimePast = gameCurrentTime; //経過時間として現在の時間を上書く
            gameTimePastInt = (int)gameTimePast; //↑整数化
            upDateGameTimerText.text = (((gameSetTime - gameTimePastInt).ToString()).PadLeft(2, '0')); //時間文字列更新

////////////////////////////////////////////////////////////////////
            //待機スイッチ入ってたら待機の秒数計測
        if(switchIdle){
            idleTime += Time.deltaTime;
        }
        
            //回答スイッチ入ってたら回答の秒数計測
        if(switchQuestion)
        {
            anserTime += Time.deltaTime;
        }
////////////////////////////////////////////////////////////////////

            //↓制限時間を過ぎたらゲーム開始をfalse,ゲーム終了をtrue
            if (gameTimePast > gameSetTime)
            {
                Debug.Log("しゅうりょう");
                questionStatus = false;
                gameFinishTime = gameTimePast; //ゲーム終了時の時間を記録
                GameFinish();
                return;
            }

            if(idleTime > kmhhIdleSpan){
                Debug.Log("待機スパンすぎた");
                setQuestion();

            }
            if(anserTime > kmhhQuestionSpan){
                Debug.Log("回答スパンすぎた");
                if(questionStatus){
                    setTimeOver();
                }
                //setIdle();

            }

        }
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
    /// <summary>
    /// ゲーム開始フラグ建てる
    /// </summary>
    /// <returns></returns> 
    public void GameStartTrigger()
    {   anserTime = 0.0f; 
        Debug.Log("げーむすたーと");
        gameTimePast = 0;
        questionNumOfTimes = 0;
        gameStart = true;　//ゲーム開始状態にする
        Invoke("CountDownObjHide",1.5f);   // カウントダウン全部消す
        KMHH_CharaAnimationManager.KMHH_CharaAnimator.SetBool("questionState",true); 

    }
////////////////////////////////////////////////////////////////////
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
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ゲームが終わったとき,終了の処理 
    /// </summary>
    /// <returns></returns>   
    public void GameFinish(){
    
            gameStart = false;
            gameFinish = true;
            switchStartMethod = true;
            Debug.Log("ゲームとまった");

            GameSceneChangeWallObj.SetActive (true);
            setFinish();
        
        Invoke("DerayGameTitleLoadRun",4.0f);   // カウントダウン全部消す 
    }
    public void DerayGameTitleLoadRun()
    {
        SceneManager.LoadScene("KMHH_Result");
    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// タイムオーバー処理
    /// </summary>
    /// <returns></returns> 
    public void setTimeOver(){
        KMHH_ScoreManager.comboCountNum = 0; //コンボカウントのリセット　時間経過によるもの
        KMHH_PlayerInputManager.AnserResultIncorrect();
        anserTime = 0.0f;
        questionStatus = false;
        switchQuestion = false;
        switchIdle = false;
    
    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// クエスチョンステートにいく
    /// </summary>
    /// <returns></returns> 
    public void setQuestion()
    {
        idleTime = 0.0f;
        questionStatus = true;

        switchIdle = false;
        switchQuestion = true;


        Debug.Log("すてーと選びにいこかな"+questionStatus);
        KMHH_CharaAnimationManager.ChangeCharaState(); //キャラのステート変えるやつ
        questionNumOfTimes = questionNumOfTimes + 1;　//出題回数増やす
    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 待機にいく
    /// </summary>
    /// <returns></returns> 
    public static void setIdle()
    {   
        anserTime = 0.0f;
        questionStatus = false;
        switchIdle = true;
        switchQuestion = false;

        Debug.Log("あいどるすてーと選びにいこかな");
        KMHH_CharaAnimationManager.ChangeCharaStateToIdle(); //キャラのステート変えるやつ　待機

    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// エモーションステートにいく
    /// </summary>
    /// <returns>成否で分岐</returns> 
    public static void setEmotion(bool anserResult)
    {
        questionStatus = false;
        switchIdle = false;
        switchQuestion = false;
        
        KMHH_CharaAnimationManager.ChangeCharaStateToEmotion(anserResult); //キャラのステート変えるやつ　待機

    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// フィニッシュステートにいく
    /// </summary>
    /// <returns></returns> 
    public  void setFinish()
    {   
        anserTime = 0.0f;
        questionStatus = false;
        switchIdle = false;
        switchQuestion = false;
        KMHH_CharaAnimationManager.ChangeCharaStateToFinish(); //キャラのステート変えるやつ　待機

    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ゲーム開始時にリセット
    /// </summary>
    /// <returns></returns> 
    public void gameParamReset()
    {
        gameCurrentTime = 0.0f;
        questionNumOfTimes = 0;
        getDeltaTime = 0.0f;
        gameStart = false;
        gameFinish = false;
        anserTime = 0.0f;
        questionStatus = false;
        switchIdle = false;
        switchQuestion = false;

    }
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////

public void reRoadKMHH()
{
    SceneManager.LoadScene (SceneManager.GetActiveScene().name);

}


}
