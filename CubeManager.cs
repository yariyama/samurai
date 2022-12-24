using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    //移動量x
    private float _vx;
    //移動量z
    private float _vz;
    //スピード
    public float _speed;
    ////回転量y
    //private float _angley;
    ////回転スピード
    //public float _a_speed;

    //ステータス
    private int _st;

    //_st=1-基本形
    //_st=2-回転


    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        //_vx = _speed;
        //_angley = _a_speed;
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }

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
        transform.Translate(_vx / 50, 0, _vz/50);
        //if (_st==2) {
        //    this.transform.Rotate(0, _angley / 50, 0);
        //}
    }

    //void OnMouseDown()
    //{
    //    if (_st==1)
    //    {
    //        _st = 2;
    //    }
    //    else if (_st==2)
    //    {
    //        _st = 1;
    //    }
    //}

    //void OnMouseUp()
    //{
    //    if (_st == 2)
    //    {
    //        _st = 1;
    //    }
    //}
}
