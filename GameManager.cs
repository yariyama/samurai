using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�X�R�A
    public static int _score;
    //�c�@��
    public static int _zan_c;
    //�n�C�X�R�A
    public static int _hi_score;
    //���Ȃ��̖��O
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

        //���ׂč폜
        //PlayerPrefs.DeleteAll();
        //�w�肵���L�[�����폜
        //PlayerPrefs.DeleteKey("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
