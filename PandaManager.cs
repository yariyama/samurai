using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //ステータス
    private int _st;
    //移動量x
    private float _vx;
    //移動スピード
    public float _w_speed;

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
        if (Input.GetKey("right"))
        {
            _vx = _w_speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_w_speed;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(_vx/50,_vy/50,0);
    }
}
