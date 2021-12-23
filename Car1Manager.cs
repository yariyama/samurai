using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car1Manager : MonoBehaviour
{
    //ステータス
    private int _st;
    //スピード
    public float _speed=5;
    //タイマー
    private float _timer;


    //_st=1-基本形
    //_st=2-移動
    //_st=3-衝突

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            transform.Translate(_speed/50,0,0);
        }
        else if (_st==3)
        {
            transform.Translate(-_speed / 50, 0, 0);

            _timer += Time.deltaTime;
            if (_timer>=0.1f)
            {
                _st = 1;
                _timer = 0;
            }
        }
    }

    void OnMouseDown()
    {
        _st = 2;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_st==2 && collision.gameObject.name=="Cube")
        {
            _st = 3;
        }
    }
}
