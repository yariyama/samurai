using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerManager : MonoBehaviour
{
    //アニメーター
    private Animator _animator;

    //ステータス
    private int _st;
    //移動量z
    private float _vz;
    //移動量x
    private float _vx;
    //移動スピード
    public float _w_speed;
    //回転量
    private float _angle;
    //回転スピード
    public float _a_speed;

    //マウス座標
    private Vector3 _mouse;
    private Vector3 _target;

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
        _vx = 0;

        if (Input.GetKey("up"))
        {
            _vz = _w_speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_w_speed;
        }
        if (Input.GetKey("right"))
        {
            _vx = _w_speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_w_speed;
        }

        _mouse = Input.mousePosition;
        _mouse.z = 10;
        _target = Camera.main.ScreenToWorldPoint(_mouse);
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vz!=0||_vx!=0)
            {
                _st = 2;
                _animator.Play("Walking");
            }
        }
        else if (_st==2)
        {
            if (_vz == 0 && _vx == 0)
            {
                _st = 1;
                _animator.Play("Idle");
            }
        }

        transform.Translate(_vx/50,0,_vz/50);

        if (_target.x<-1.5f)
        {
            _angle = -_a_speed;
            transform.Rotate(0,_angle/50,0);
        }
        else if (_target.x > 1.5f)
        {
            _angle = _a_speed;
            transform.Rotate(0, _angle / 50, 0);
        }
    }
}
