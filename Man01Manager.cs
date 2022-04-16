using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man01Manager : MonoBehaviour
{
    //ステータス
    private int _st;
    //移動スピード
    public float _speed;
    //移動量
    private float _vx;
    private float _vz;

    //_st=1-基本形
    //_st=2-移動

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        //縦移動
        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }

        //横移動
        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0 || _vz!=0)
            {
                _st = 2;
            }
        }
        else if (_st==2)
        {
            if (_vx == 0 && _vz == 0)
            {
                _st = 1;
            }

            this.transform.Translate(_vx/50,0,_vz/50);
        }
    }
}
