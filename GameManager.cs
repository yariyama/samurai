using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //スコア
    public static int _score;
    //成功数
    public static float _success_c;
    //成功パーセント
    public static float _success_per;
    //ステージ
    public static int _stage;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _success_c = 0;
        _stage = 1;
    }

}
