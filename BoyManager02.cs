using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyManager02 : MonoBehaviour
{
    //ボーイスプライトオブジェクト★
    private GameObject _BoySprite;

    //角度
    private Vector3 _angle;
    //カメラ座標
    private Vector3 _camera_position;
    //リジッドボディ★
    private Rigidbody _rbody;

    //ボーイスプライトアニメーター★
    private Animator _boy_sprite_animator;

    //ステータス
    private int _st;
    //移動スピード
    public float _speed;
    //移動量X
    private float _vx;
    //移動量Z
    private float _vz;
    //ジャンプパワー★
    public float _jump_p;
    //接地★
    private bool _ground_st;

    //タイマー★
    private float _timer;
    //カウント★
    private int _count;

    //向いてる方向★
    private int _dire;
    //向いてるx方向★
    private int _dire_x;
    //向いてるｙ方向★
    private int _dire_z;
    //向いてる前のx方向★
    private int _dire_x_b;
    //向いてる前のy方向★
    private int _dire_z_b;

    //★
    //_dire=1-下
    //_dire=2-左下
    //_dire=3-左
    //_dire=4-左上
    //_dire=5-上
    //_dire=6-右上
    //_dire=7-右
    //_dire=8-右下

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプセット
    //_st=4-ジャンプ

    void Awake()
    {
        //★
        _BoySprite = transform.Find("BoySprite").gameObject;
        _boy_sprite_animator = _BoySprite.GetComponent<Animator>();

        _angle = transform.localEulerAngles;
        _rbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _vx = 0;
        _vz = 0;

        //★
        _dire_x = 0;
        _dire_z = 0;
        _dire_x_b = 0;
        _dire_z_b = 0;

        _ground_st = true;

        BillSet();
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;
        //★
        _dire_x = 0;
        _dire_z = 0;

        //4方向のキー操作受付
        if (Input.GetKey("right"))
        {
            //★
            _dire_x = 1;

            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            //★
            _dire_x = 2;

            _vx = -_speed;
        }
        if (Input.GetKey("up"))
        {
            //★
            _dire_z = 1;

            _vz = _speed;

        }
        else if (Input.GetKey("down"))
        {
            //★
            _dire_z = 2;

            _vz = -_speed;
        }

        //スペースキー操作受付
        if (Input.GetKeyDown("space") && _ground_st)
        {
            _st = 3;
        }
    }

    void FixedUpdate()
    {
        //移動量がある場合
        if (_vx != 0 || _vz != 0)
        {
            //前と向きが違う場合★
            if (_dire_x != _dire_x_b || _dire_z != _dire_z_b)
            {
                _dire_x_b = _dire_x;
                _dire_z_b = _dire_z;

                //今の方向の設定
                //z方向なし
                if (_dire_z == 0)
                {
                    //x方向右
                    if (_dire_x == 1)
                    {
                        _dire = 7;
                    }
                    //x方向左
                    else if (_dire_x == 2)
                    {
                        _dire = 3;
                    }
                }
                //z方向上
                else if (_dire_z == 1)
                {
                    //x方向右
                    if (_dire_x == 1)
                    {
                        _dire = 6;
                    }
                    //x方向左
                    else if (_dire_x == 2)
                    {
                        _dire = 4;
                    }
                    //x方向なし
                    else
                    {
                        _dire = 5;
                    }
                }
                //z方向下
                else if (_dire_z == 2)
                {
                    //x方向右
                    if (_dire_x == 1)
                    {
                        _dire = 8;
                    }
                    //x方向左
                    else if (_dire_x == 2)
                    {
                        _dire = 2;
                    }
                    //x方向なし
                    else
                    {
                        _dire = 1;
                    }
                }

                //移動
                if (_st == 2)
                {
                    AnimeSet(2);
                }
                //ジャンプ
                else if (_st == 4)
                {
                    AnimeSet(1);
                }
            }

            //基本形
            if (_st == 1)
            {
                _st = 2;

                //★
                _count = 0;
                _timer = 0;

                //★
                AnimeSet(2);
            }
            //ジャンプ★
            else if (_st == 4)
            {
                AnimeSet(1);
            }

            //移動
            transform.Translate(_vx / 50, 0, _vz / 50, Space.World);

            BillSet();
        }
        else
        {
            if (_st == 2)
            {
                _st = 1;

                //★
                _dire_x = 0;
                _dire_z = 0;
                _dire_x_b = 0;
                _dire_z_b = 0;
                //★
                AnimeSet(1);
            }
        }

        //ジャンプセット★
        if (_st == 3)
        {
            //ジャンプ
            _st = 4;
            _rbody.AddForce(new Vector3(0, _jump_p, 0), ForceMode.Impulse);
            AnimeSet(1);
        }
    }

    //接地判断
    void OnTriggerEnter(Collider other)
    {
        if (_st == 4)
        {
            _st = 1;
            _ground_st = true;
        }
    }

    //離陸判断
    void OnTriggerExit(Collider other)
    {
        if (_st == 4)
        {
            _ground_st = false;
        }
    }


    //アニメーションのセット★
    void AnimeSet(int _no)
    {
        //基本形
        if (_no == 1)
        {
            if (_dire == 1 || _dire == 2 || _dire == 8)
            {
                _boy_sprite_animator.Play("front_base");
            }
            else if (_dire == 3)
            {
                _boy_sprite_animator.Play("left_base");
            }
            else if (_dire == 7)
            {
                _boy_sprite_animator.Play("right_base");
            }
            else if (_dire == 4 || _dire == 5 || _dire == 6)
            {
                _boy_sprite_animator.Play("back_base");
            }
        }
        //歩き
        else if (_no == 2)
        {
            if (_dire == 1 || _dire == 2 || _dire == 8)
            {
                _boy_sprite_animator.Play("front_walk");
            }
            else if (_dire == 3)
            {
                _boy_sprite_animator.Play("left_walk");
            }
            else if (_dire == 7)
            {
                _boy_sprite_animator.Play("right_walk");
            }
            else if (_dire == 4 || _dire == 5 || _dire == 6)
            {
                _boy_sprite_animator.Play("back_walk");
            }
        }
    }

    //ビルボードセット
    public void BillSet()
    {
        _camera_position = Camera.main.transform.position;
        _camera_position.y = transform.position.y;
        transform.LookAt(_camera_position);
        _angle = transform.localEulerAngles;
        _angle.y += 180;
        transform.localEulerAngles = _angle;
    }
}
