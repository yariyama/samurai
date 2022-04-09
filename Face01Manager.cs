using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face01Manager : MonoBehaviour
{
    //ステータス
    private int _st;
    //回転スピード
    public float _speed;

    //_st=1-基本形
    //_st=2-回転

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
        if (_st==2)
        {
            transform.Rotate(0,_speed/50,0);
        }
    }

    void OnMouseDown()
    {
        if (_st==1)
        {
            _st = 2;
        }
    }

    void OnMouseUp()
    {
        if (_st == 2)
        {
            _st = 1;
        }
    }
}
