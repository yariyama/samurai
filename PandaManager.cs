using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //メインカメラオブジェクト
    private GameObject _MainCamera;

    //リジッドボディ
    private Rigidbody2D _rbody;
    //アニメーター
    private Animator _animator;
    //スケール
    private Vector2 _scale;
    //座標
    private Vector2 _position;
    //カメラ座標
    private Vector3 _camera_position;

    //ステータス
    public int _st;
    //移動スピード
    public float _w_speed;
    //ジャンプ量
    public float _j_speed;
    //x移動量
    private float _vx;
    //x方向
    private int _dire_x;
    //タイマー
    private float _timer;
    //接地
    private bool _ground_st;
    //入力
    private bool _push_st;
    //ジャンプダウン
    public bool _jump_down;
    //メインカメラベースy
    private float _camera_yb;


    //_st=1-基本形
    //_st=2-移動
    //_st=3-振り向き
    //_st=4-ジャンプ前
    //_st=5-ジャンプ
    //_st=6-ジャンプ後

    void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
        _position = transform.position;

        _MainCamera = GameObject.Find("Main Camera");
        _camera_position = _MainCamera.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire_x = 1;
        _timer = 0;
        _animator.Play("Base");

        _camera_position.x = _position.x;
        _camera_position.y = _position.y+2;
        _camera_position.z = -10;
        _MainCamera.transform.position = _camera_position;
        _camera_yb = _camera_position.y;
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        if (Input.GetKey("right"))
        {
            _vx = _w_speed;
            if (_dire_x==2)
            {
                if (_st==1||_st==2) {
                    _st = 3;
                    _timer = 0;
                }
            }
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_w_speed;
            if (_dire_x == 1)
            {
                if (_st == 1 || _st == 2)
                {
                    _st = 3;
                    _timer = 0;
                }
            }
        }

        if (Input.GetKey("space"))
        {
            if (!_push_st && _ground_st)
            {
                _push_st = true;
                _st = 4;
                _timer = 0;
                _jump_down = false;
                _animator.Play("JumpSet");
            }
        }
        else
        {
            _push_st = false;
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
        else if (_st==3)
        {
            if (_timer==0)
            {
                _animator.Play("Turn");
            }
            _timer += Time.deltaTime;
            if (_timer>=0.1f)
            {
                _timer =0;
                _st = 1;
                if (_dire_x==1)
                {
                    _dire_x = 2;
                    _scale.x = -1.3f;
                }
                else
                {
                    _dire_x = 1;
                    _scale.x = 1.3f;
                }
                _animator.Play("Base");
                transform.localScale = _scale;
            }
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer = 0;
                _st = 5;
                _rbody.AddForce(new Vector2(0,_j_speed),ForceMode2D.Impulse);
                _animator.Play("Jump");
            }
        }
        else if (_st==6)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.2f)
            {
                _timer = 0;
                _st = 1;
                _animator.Play("Base");
            }
            
        }

        if (_vx!=0 && (_st==2||_st==5))
        {
            transform.Translate(_vx/50,0,0);
        }
    }

    void LateUpdate()
    {
        _position = transform.position;
        _camera_position.x = _position.x;
        if (_position.y >= 3.5f)
        {
            _camera_position.y = _position.y-2f;
        }
        else
        {
            _camera_position.y = _camera_yb;
        }
        _MainCamera.transform.position = _camera_position;
    }

    //着地判断
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Cloud")
        {
            _ground_st = true;

            if (_st==5 && _jump_down)
            {
                _st = 6;
                _timer = 0;
                _animator.Play("JumpSet");
            }
        }
    }

    //離陸判断
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cloud")
        {
            _ground_st = false;
        }
    }

    void JumpDown()
    {
        _jump_down = true;
    }
}
