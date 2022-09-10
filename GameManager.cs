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

    // Start is called before the first frame update
    void Start()
    {
        _get_point = PlayerPrefs.GetInt("GetPoint",50);
        _sanity_max = PlayerPrefs.GetInt("SanityMax",50);
        _heart_max = PlayerPrefs.GetInt("HeartMax", 50);
        _mental = PlayerPrefs.GetFloat("Mental", 1);

        //PlayerPrefs.DeleteKey("GetPoint");
        //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
