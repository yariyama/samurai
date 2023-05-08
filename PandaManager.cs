using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //アニメーター
    private Animator _animator;

    //ステータス
    private int _st;
    //移動量x
    private float _vx;
    //移動スピード
    public float _w_speed;

    //_st=1-基本形
    //_st=2-移動

    void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        if (Input.GetKey("right")|| Input.GetKey("d"))
        {
            _vx = _w_speed;
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            _vx = -_w_speed;
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vx == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }

        transform.Translate(_vx/50,0,0);
    }
}
