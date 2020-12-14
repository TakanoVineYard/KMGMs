using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement; //シーン切り替え

public class KMGMs_MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public void GoToOption()
    {
        Debug.Log("hogeOption");
        Invoke("DerayMoveKMGMs_Option", 1.0f);
    }

    public void GoToKMHH()
    {
        Debug.Log("hoge");
        Invoke("DerayMoveKMHH", 1.0f);
    }
    public void DerayMoveKMHH()
    {

            SceneManager.LoadScene("KMHH");
    }

    public void DerayMoveKMGMs_Option()
    {

            SceneManager.LoadScene("KMGMs_Option");
    }




}
