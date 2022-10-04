using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PandaManager : MonoBehaviour
{
    //メインカメラオブジェクト
    private GameObject _MainCamera;
    //サルオブジェクト
    private GameObject _Monkey;
    //サルスクリプト
    private MonkeyManager _MonkeyManager;
    //スコアオブジェクト
    private GameObject _Score;
    //ゲームメインUIオブジェクト
    private GameObject _GameMainUI;
    //ゲームメインUIスクリプト
    private GameMainUIManager _GameMainUIManager;
    //チェックポイントオブジェクト
    private GameObject[] _CheckPoint = new GameObject[3];
    //ゲームオーバーオブジェクト
    private GameObject _GameOver;
    //ゲームオーバースクリプト
    private GameOverManager _GameOverManager;
    //マスクオブジェクト
    private GameObject _Mask;
    //マスクスクリプト
    private MaskManager _MaskManager;

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
    //コライダー
    private BoxCollider2D _collider;
    //表示
    private SpriteRenderer _renderer;
    //色
    private Color _color;
    //スコアテキスト
    private Text _score_text;
    //オーディオソース
    private AudioSource _audio;
    //効果音
    public AudioClip _se1;
    public AudioClip _se2;
    public AudioClip _se3;
    public AudioClip _se4;
    public AudioClip _se5;

    //ステータス
    public int _st;
    //カウント
    private int _count;
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
    //_st=7-攻撃
    //_st=8-攻撃戻し
    //_st=9-ダメージ
    //_st=10-死
    //_st=11-弱ダメージ
    //_st=12-落下
    //_st=13-勝利ポーズ
    //_st=14-勝利ポーズ後
    //_st=15-フェードアウト

    void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
        _position = transform.position;
        _collider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _color = _renderer.color;
        _audio = GetComponent<AudioSource>();

        _MainCamera = GameObject.Find("Main Camera");
        _camera_position = _MainCamera.transform.position;

        _Score = GameObject.Find("Score");
        _score_text = _Score.GetComponent<Text>();

        _GameMainUI = GameObject.Find("GameMainUI");
        _GameMainUIManager = _GameMainUI.GetComponent<GameMainUIManager>();

        for (int i=1;i<=3;i++)
        {
            _CheckPoint[i - 1] = GameObject.Find("CheckPoint"+i);
        }

        _GameOver = GameObject.Find("GameOver");
        _GameOverManager = _GameOver.GetComponent<GameOverManager>();

        _Mask = GameObject.Find("Mask");
        _MaskManager = _Mask.GetComponent<MaskManager>();
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

        _GameOver.SetActive(false);
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
                _audio.clip = _se1;
                _audio.Play();
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
        else if (_st==7)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _st = 8;
                _timer =0;
                _animator.Play("AttackAfter");
            }
        }
        else if (_st==9)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _st = 10;
                _count = 0;

                --GameManager._life;
                GameManager._life = Mathf.Ceil(GameManager._life);
                _GameMainUIManager.LifeGageSet();
            }
        }
        else if (_st==10)
        {
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

            _color.a -= 0.02f;
            if (_color.a<=0)
            {
                _color.a = 0;
                _st = 0;

                if (GameManager._life > 0)
                {
                    ReturnSet();
                }
                else
                {
                    _GameOver.SetActive(true);
                    _GameOverManager.ActiveSet();
                }
            }
            _renderer.color = _color;
        }
        else if (_st==11)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _timer = 0;

                if (GameManager._life==2||GameManager._life==1||GameManager._life==0)
                {
                    _st = 10;
                    _count = 0;
                    _GameMainUIManager.LifeGageSet();
                }
                else
                {
                    _st = 1;
                    _animator.Play("Base");
                }
            }
        }
        else if (_st==15)
        {
            _color.a -= 0.05f;
            if (_color.a<=0)
            {
                _color.a = 0;
                _st = 0;

                _MaskManager.OutSet();
                this.gameObject.SetActive(false);
            }
            _renderer.color = _color;
        }

        //移動
        if (_vx!=0 && (_st==2||_st==5))
        {
            transform.Translate(_vx/50,0,0);
        }

        //画面外判断
        if (_st!=0 && transform.position.y<=-8)
        {
            _st = 0;
            --GameManager._life;
            GameManager._life = Mathf.Ceil(GameManager._life);
            _GameMainUIManager.LifeGageSet();

            if (GameManager._life>0)
            {
                ReturnSet();
            }
            else
            {
                _GameOver.SetActive(true);
                _GameOverManager.ActiveSet();
            }
        }
    }

    void LateUpdate()
    {
        if (_st == 2 || _st == 5)
        {
            _position = transform.position;
            _camera_position.x = _position.x;
            if (_position.y >= 3.5f)
            {
                _camera_position.y = _position.y - 2f;
            }
            else
            {
                _camera_position.y = _camera_yb;
            }
            _MainCamera.transform.position = _camera_position;
        }
    }

    //攻撃判断
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_st==5 && _jump_down && collision.gameObject.tag=="Enemy")
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();

            if (_MonkeyManager._st==1 || _MonkeyManager._st==2 || _MonkeyManager._st==3)
            {
                _st = 7;
                _timer = 0;
                _animator.Play("Attack");
                _audio.clip = _se3;
                _audio.Play();

                _MonkeyManager.DameSet();

                GameManager._score += 100;
                _score_text.text = GameManager._score.ToString("0000");
            }
        }
    }

    //着地判断
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Cloud")
        {
            _ground_st = true;

            if ((_st==5 && _jump_down)||_st==8 ||_st==12)
            {
                _st = 6;
                _timer = 0;
                _animator.Play("JumpSet");
                _jump_down = false;
                _audio.clip = _se2;
                _audio.Play();
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

    //ダメージセット
    public void DameSet()
    {
        _st = 9;
        _timer =0;
        _animator.Play("Death");
        _audio.clip = _se4;
        _audio.Play();

        _collider.enabled = false;
        _rbody.gravityScale = 0;
        //_rbody.velocity = new Vector3(0,0,0);
    }

    //弱ダメージセット
    public void SDameSet()
    {
        _st = 11;
        _timer = 0;
        _animator.Play("Dame");

        GameManager._life -= 0.5f;
        _GameMainUIManager.LifeGageSet();
    }

    void JumpDown()
    {
        _jump_down = true;
    }

    //復活
    public void ReturnSet()
    {
        _color.r = 1;
        _color.g = 1;
        _color.b = 1;
        _color.a = 1;
        _renderer.color = _color;

        _st = 12;
        _timer = 0;
        _count = 0;
        _ground_st = false;
        _push_st = false;
        _jump_down = true;
        _animator.Play("AttackAfter");
        _collider.enabled = true;
        _rbody.gravityScale = 2;

        for (int i=2;i>=0;i--)
        {
            if (transform.position.x >= _CheckPoint[i].transform.position.x||i==0)
            {
                transform.position = new Vector2(_CheckPoint[i].transform.position.x, _CheckPoint[i].transform.position.y);
                break;
            }
        }
    }

    //勝利セット
    public void WinSet()
    {
        _st = 13;
        _timer = 0;
        _animator.Play("Win");
    }

    //勝利後
    void WinAfter()
    {
        _st = 14;
        _audio.clip = _se5;
        _audio.Play();
    }

    //フェードアウト
    public void FadeOut()
    {
        _st = 15;
    }
}
