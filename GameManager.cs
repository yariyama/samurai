using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //獲得ポイント
    public static int _get_point=50;
    //正気度MAX
    public static int _sanity_max = 50;
    //心拍数MAX
    public static int _heart_max = 50;
    //正気度
    public static float _sanity = 100 * (_sanity_max*0.01f);
    //心拍数
    public static float _heart = 0;
    //メンタル
    public static float _mental=1;
    //日
    public static int _date = 0;
    //日イベントステータス
    public static int _day_event_st;

    // Start is called before the first frame update
    void Start()
    {
        _get_point = PlayerPrefs.GetInt("GetPoint",50);
        _sanity_max = PlayerPrefs.GetInt("SanityMax",50);
        _heart_max = PlayerPrefs.GetInt("HeartMax", 50);
        _mental = PlayerPrefs.GetFloat("Mental", 1);
        _date = PlayerPrefs.GetInt("Date",0);

        //PlayerPrefs.DeleteKey("GetPoint");
        //PlayerPrefs.DeleteAll();

        if (_date>0 && _date%5==0)
        {
            _day_event_st = 1;
        }
        else
        {
            _day_event_st = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
