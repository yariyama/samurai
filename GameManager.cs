using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //スコア
    public static int _score;
    //残機数
    public static int _zan_c;
    //ハイスコア
    public static int _hi_score;
    //あなたの名前
    //public static string _your_name;

    void Awake()
    {
        _hi_score = PlayerPrefs.GetInt("HiScore",0);
        //_your_name= PlayerPrefs.GetString("YourName", "");
    }

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _zan_c = 3;

        //すべて削除
        //PlayerPrefs.DeleteAll();
        //指定したキーだけ削除
        //PlayerPrefs.DeleteKey("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
