using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //アニメーター
    private Animator _animtor;

    //移動量x
    private float _vx;
    //移動量z
    private float _vz;
    //スピード
    public float _speed;

    //ステータス
    private int _st;

    //_st=1-基本形
    //_st=2-回転

    void Awake()
    {
        _animtor = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animtor.Play("Base");
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
        transform.Translate(_vx / 50, 0, _vz / 50);
    }
}
