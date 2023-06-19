using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //モンキーオブジェクト
    private GameObject _Monkey;
    //モンキースクリプト
    private MonkeyManager _MonkeyManager;

    //メインカメラ
    private GameObject _MainCamera;
    //メインカメラ座標
    private Vector3 _camera_position;

    //アニメーター
    private Animator _animator;
    //リジッドボディ
    private Rigidbody2D _rbody;
    //コライダー
    private BoxCollider2D _collider;
    //レンダラー
    private SpriteRenderer _renderer;
    //カラー
    private Color _color;

    //座標
    private Vector2 _position;
    //スケール
    private Vector3 _scale;

    //ステータス
    public int _st;
    //カウント
    public int _count;
    //移動量x
    private float _vx;
    //移動スピード
    public float _w_speed;
    //方向
    private int _dire;
    //タイマー
    private float _timer;
    //ジャンプ力
    public float _jump_p;
    //プッシュ有無
    private bool _push_st;
    //ジャンプ回数
    private int _jump_c;
    //メインカメラyベース
    private float _camera_yb;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-振り向き
    //_st=4-ジャンプ前
    //_st=5-ジャンプ
    //_st=6-ジャンプ後
    //_st=7-攻撃
    //_st=8-攻撃戻し
    //_st=9-ダメージ
    //_st=10-死

    void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody2D>();
        _scale = this.transform.localScale;
        _position = this.transform.position;

        _collider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _color = _renderer.color;

        _MainCamera = GameObject.Find("Main Camera");
        _camera_position = _MainCamera.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire = 1;
        _timer =0;
        _count = 0;
        _jump_c = 0;
        _animator.Play("Base");

        _camera_position.x = _position.x;
        _camera_position.y = _position.y+2;
        _camera_position.z = -10;
        _MainCamera.transform.position =_camera_position;
        _camera_yb = _camera_position.y;
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        if (Input.GetKey("right")|| Input.GetKey("d"))
        {
            if (_st<=2||_st==5) {
                _vx = _w_speed;
                if (_dire == 2 && _st <= 2)
                {
                    TurnSet();
                }
            }
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            if (_st <= 2 || _st == 5)
            {
                _vx = -_w_speed;
                if (_dire == 1 && _st <= 2)
                {
                    TurnSet();
                }
            }
        }

        if (Input.GetKey("space"))
        {
            if ( _push_st==false && ( _st==1 ||_st==2 || (_st==5 && _count<=1) ) && _jump_c<2 )
            {
                _push_st = true;
                _st = 4;
                _timer =0;
                _animator.Play("JumpSet");
                ++_jump_c;
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
            _timer += Time.deltaTime;
            if (_timer>=0.1f)
            {
                _timer = 0;
                if (_dire==1)
                {
                    _dire = 2;
                }
                else
                {
                    _dire = 1;
                }
                _scale.x *= -1;
                transform.localScale = _scale;
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer = 0;
                _st = 5;
                _count = 0;
                _animator.Play("JumpUp");
                _rbody.AddForce(new Vector3(0,_jump_p,0),ForceMode2D.Impulse);
            }
        }
        else if (_st == 5)
        {
            _timer += Time.deltaTime;
            if (_count==0)
            {
                if (_timer>=0.3f)
                {
                    _timer = 0;
                    _count = 1;
                    _animator.Play("JumpTop");
                }
            }
            else if (_count==1)
            {
                if (_timer >= 0.2f)
                {
                    _timer = 0;
                    _count = 2;
                    _animator.Play("JumpDown");
                    _jump_c = 0;
                }
            }
        }
        else if (_st == 6)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.2f)
            {
                _timer = 0;
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st==7)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.3f)
            {
                _timer = 0;
                _st = 8;
                _animator.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else if (_st==9)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _st = 10;
                _count =0;
                _collider.enabled = false;
                _rbody.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
        else if (_st==10)
        {
            //点滅
            if (_count==0)
            {
                _color.g -= 0.1f;
                _color.b -= 0.1f;
                if (_color.g<=0)
                {
                    _color.g = 0;
                    _color.b = 0;
                    _count = 1;
                }
            }
            else if (_count==1)
            {
                _color.g += 0.1f;
                _color.b += 0.1f;
                if (_color.g >= 1)
                {
                    _color.g = 1;
                    _color.b = 1;
                    _count = 0;
                }
            }

            //半透明
            _color.a -= 0.02f;
            if (_color.a<=0)
            {
                _color.a = 0;
                _st = 0;
                this.gameObject.SetActive(false);
            }

            _renderer.color = _color;
        }

        if (_st==2||_st==5) {
            transform.Translate(_vx / 50, 0, 0);
        }
    }

    void LateUpdate()
    {
        if (_st==2||_st==5)
        {
            _position = transform.position;
            _camera_position.x = _position.x;
            if (_position.y>=2.5f)
            {
                _camera_position.y = _position.y-1.5f;
            }
            else
            {
                _camera_position.y = _camera_yb;
            }
            _MainCamera.transform.position = _camera_position;
        }
    }

    private void TurnSet()
    {
        _st = 3;
        _timer = 0;
        _animator.Play("Turn");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Cloud" && (_st==5||_st==8))
        {
            _st = 6;
            _timer = 0;
            _animator.Play("JumpSet");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Monkey" && _st==5 && _count==2)
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
            if (_MonkeyManager._st<=3)
            {
                _st = 7;
                _count = 0;
                _timer =0;
                _animator.Play("JumpAttack");
                _rbody.constraints = RigidbodyConstraints2D.FreezePosition;
                _MonkeyManager.DameSet();
            }
        }
    }

    public void DameSet()
    {
        _st = 9;
        _timer =0;
        _animator.Play("Death");
    }
}
