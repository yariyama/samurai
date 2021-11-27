using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //スコア
    public static int _score;
    //ステージ
    public static int _stage;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _stage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
