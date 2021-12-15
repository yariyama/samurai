using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy2Manager : MonoBehaviour
{
    //ポールオブジェクト
    private GameObject _Pole;

    private Vector3 _angle;

    //アニメーター
    private Animator _animator;

    public float _speed = 1;

    //ステータス
    private int _st;

    private float _vx;
    private float _vz;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-攻撃

    void Awake()
    {
        _Pole = transform.Find("Pole").gameObject;

        _angle = transform.localEulerAngles;
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Base");

        _Pole.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }

        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }

        if (Input.GetKey("a") && (_st==1||_st==2))
        {
            _st = 3;
            _animator.Play("Attack");
            _Pole.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (_vx != 0 || _vz != 0)
        {
            if (_st==1)
            {
                _st = 2;
                _animator.Play("Walk");
            }

            if (_vz > 0)
            {
                if (_vx > 0)
                {
                    _angle.y = 45;
                }
                else if (_vx < 0)
                {
                    _angle.y = 315;
                }
                else
                {
                    _angle.y = 0;
                }
            }
            else if (_vz < 0)
            {
                if (_vx > 0)
                {
                    _angle.y = 135;
                }
                else if (_vx < 0)
                {
                    _angle.y = 225;
                }
                else
                {
                    _angle.y = 180;
                }
            }
            else
            {
                if (_vx > 0)
                {
                    _angle.y = 90;
                }
                else if (_vx < 0)
                {
                    _angle.y = 270;
                }
            }
            transform.localEulerAngles = _angle;
            transform.Translate(_vx / 50, 0, _vz / 50, Space.World);
        }
        else
        {
            if (_st == 2)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
    }

    //攻撃後
    void AttackAfter()
    {
        _st = 1;
        _animator.Play("Base");
        _Pole.SetActive(false);
    }
}
