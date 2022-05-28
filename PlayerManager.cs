using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //アニメーター
    private Animator _animator;

    //ステータス
    private int _st;
    //移動量
    private float _vz;
    //スピード
    public float _speed;
    //回転量
    private float _angle;
    //回転スピード
    public float _r_speed;

    //_st=1-基本形
    //_st=2-移動

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        _vz = 0;
        _angle = 0;
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
            _angle = _r_speed;
        }
        else if (Input.GetKey("left"))
        {
            _angle = -_r_speed;
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vz!=0 || _angle!=0)
            {
                _st = 2;
                _animator.Play("Run");
            }
        }
        else if (_st==2)
        {
            transform.Translate(0,0,_vz/50);
            transform.Rotate(0,_angle/50,0);

            if (_vz == 0 && _angle==0)
            {
                _st = 1;
                _animator.Play("Idle");
            }
        }
    }
}
