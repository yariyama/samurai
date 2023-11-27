using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //スピード
    public float _speed;
    //移動量x
    private float _vx;
    //移動量z
    private float _vz;

    //ステータス
    private int _st;

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

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            _vz = -_speed;
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
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
            this.transform.Translate(_vx/50,0,_vz/50);

            if (_vx == 0 && _vz == 0)
            {
                _st = 1;
            }
        }
    }
}
