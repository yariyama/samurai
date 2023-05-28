using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    //リジッドボディ
    private Rigidbody _rbody;
    //コライダー
    private CapsuleCollider _collider;

    //アニメーター
    private Animator _animator;
    //角度
    private Vector3 _angle;

    //ステータス
    public int _st;
    //方向
    private int _dire;
    //移動スピード
    public float _w_speed;
    //回転スピード
    public float _a_speed;
    //タイマー
    private float _timer;
    //ランダム値
    private int _ran;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-振り向き
    //_st=4-ダメージ

    void Awake()
    {
        _rbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        _angle = transform.localEulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire = 1;
        _timer = 0;
        _angle.y = 270;
        transform.localEulerAngles = _angle;
        _animator.Play("Base");
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _ran = Random.Range(0,5);
                if (_ran==1)
                {
                    _timer = 0;
                    _st = 2;
                    _animator.Play("Walk");
                }
            }
        }
        else if (_st==2)
        {
            transform.Translate(0,0,_w_speed/50);
        }
        else if (_st==3)
        {
            transform.Rotate(0,-_a_speed/50,0);
            _angle = transform.localEulerAngles;

            if (_angle.y>=360)
            {
                _angle.y -= 360;
            }
            if (_dire==1 && _angle.y<=90)
            {
                _st = 1;
                _angle.y = 90;
                _dire = 2;
                _animator.Play("Base");
            }
            else if (_dire == 2 && _angle.y >= 260 && _angle.y <= 270)
            {
                _st = 1;
                _angle.y = 270;
                _dire = 1;
                _animator.Play("Base");
            }
            transform.localEulerAngles = _angle;
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void TurnSet()
    {
        _st = 3;
    }

    public void DameSet()
    {
        _st = 4;
        _timer = 0;
        _animator.Play("Dame");
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
        _collider.enabled = false;
    }
}
