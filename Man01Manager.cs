using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man01Manager : MonoBehaviour
{
    //リジッドボディ
    private Rigidbody _rdody;

    //ステータス
    private int _st;
    //移動スピード
    public float _speed;
    //移動量
    private float _vx;
    private float _vz;

    //接地有無(true,false)
    private bool _ground_st;
    //ジャンプ力
    public float _jump_power;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプ溜め
    //_st=4-ジャンプ

    void Awake()
    {
        _rdody = this.GetComponent<Rigidbody>();
    }

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

        //ジャンプ
        if (Input.GetKey("space"))
        {
            if (_ground_st==true) {
                _st = 3;
            }
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
        else if (_st==3)
        {
            _rdody.AddForce(new Vector3(0,_jump_power,0),ForceMode.Impulse);
            _st = 4;
        }
        else if (_st==4)
        {
            this.transform.Translate(_vx / 50, 0, _vz / 50);
        }
    }

    void OnTriggerStay(Collider other)
    {
        _ground_st = true;

        if (_st==4)
        {
            _st = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        _ground_st = false;
    }
}
