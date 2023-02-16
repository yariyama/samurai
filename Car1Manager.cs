using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car1Manager : MonoBehaviour
{
    //スピード
    public float _speed;
    //ステータス
    private int _st;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-移動

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            this.transform.Translate(0, 0, 0);
        }
        else if (_st==2)
        {
            this.transform.Translate(_speed / 50, 0, 0);
        }

        _timer += Time.deltaTime;
        if (_timer >= 1)
        {
            _timer = 0;

            if (_st==1)
            {
                _st = 2;
            }
            else
            {
                _st = 1;
            }
        }
    }
}
