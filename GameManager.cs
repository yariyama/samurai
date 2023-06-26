using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //スコア
    public static int _score;
    //残機数
    public static int _zan_c;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _zan_c = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
