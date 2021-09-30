using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    //ステータス
    private int _st;

    //ノーマルスピード
    public float _n_speed;

    //_st=1-基本移動

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //基本移動
        if (_st==1)
        {
            transform.Translate(-_n_speed/50,0,0);
        }
    }
}
