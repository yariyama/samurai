using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boy02Manager : MonoBehaviour
{
    //ボーイオブジェクト★
    private GameObject _Boy;
    //エネミーオブジェクト★
    private GameObject _Enemy;
    //エネミースクリプト★
    private EnemyManager _EnemyManager;
    //メインカメラオブジェクト▲
    private GameObject _MainCamera;
    //ゲームオーバーオブジェクト
    private GameObject _GameOver;
    //スコアオブジェクト
    private GameObject _Score;

    //リジッドボディ
    private Rigidbody _rbody;
    //アニメーター
    private Animator _animator;
    //スコアテキスト
    private Text _score_text;
    //オーディオソース
    private AudioSource _audio;
    //効果音
    public AudioClip _jump_se;
    public AudioClip _land_se;
    public AudioClip _attack_se;

    //ステータス
    public int _st;
    //移動スピード
    public float _speed;
    //移動量
    private float _vx;
    private float _vz;

    //接地有無
    private bool _ground_st;
    //押されている有無
    private bool _push_st;
    //ジャンプ力
    public float _jump_power;

    //タイマー★
    private float _timer;


    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプ溜
    //_st=4-ジャンプ
    //_st=5-ダメージ★

    void Awake()
    {
        _Boy = transform.Find("Boy").gameObject;
        _MainCamera = transform.Find("Main Camera").gameObject;
        _GameOver = GameObject.Find("GameOver");
        _GameOver.SetActive(false);
        _Score = GameObject.Find("Score");

        _rbody = this.GetComponent<Rigidbody>();
        _animator = this.GetComponent<Animator>();
        _score_text = _Score.GetComponent<Text>();
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Base");
        _ground_st = true;
        _push_st = false;
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        //縦移動
        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }
        //横移動
        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }

        //ジャンプ
        if (Input.GetKey("space"))
        {
            if (_ground_st && !_push_st)
            {
                _st = 3;
            }

            _push_st = true;
        }
        else
        {
            _push_st = false;
        }
    }

    void FixedUpdate()
    {
        if (_st == 1)
        {
            if (_vx != 0 || _vz != 0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st == 2)
        {
            if (_vx == 0 && _vz == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }

            transform.Translate(_vx / 50, 0, _vz / 50);
        }
        else if (_st == 3)
        {
            _st = 4;
            _rbody.AddForce(new Vector3(0, _jump_power, 0), ForceMode.Impulse);
            _animator.Play("BaseJump");

            _audio.clip = _jump_se;
            _audio.Play();
        }
        else if (_st == 4)
        {
            transform.Translate(_vx / 50, 0, _vz / 50);
        }
        //▲
        else if (_st==5)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _timer = 0;
                _st = 0;

                _GameOver.SetActive(true);
                _Boy.SetActive(false);
            }
        }

        //▲
        if (!_ground_st)
        {
            if (transform.position.y < 0 && _MainCamera.transform.parent)
            {
                _MainCamera.transform.parent = null;
            }

            if (transform.position.y < -20 && _st!=0)
            {
                _GameOver.SetActive(true);
                _st = 0;
                this.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && _st==4)
        {
            _Enemy = other.gameObject;
            _EnemyManager = _Enemy.GetComponent<EnemyManager>();

            if (_EnemyManager._st == 1 || _EnemyManager._st == 2)
            {
                _EnemyManager.DameSet();

                GameManager._score_c += 100;
                _score_text.text = GameManager._score_c.ToString("000");

                _audio.clip = _attack_se;
                _audio.Play();
            }
        }
        else if (other.gameObject.tag == "Cube")
        {
            _ground_st = true;

            if (_st == 4)
            {
                _st = 1;
                _animator.Play("JumpBase");

                _audio.clip = _land_se;
                _audio.Play();
            }
        }
        else if (other.gameObject.name == "Goal")
        {
            SceneManager.LoadScene("Lesson06b");
        }
    }

    void OnTriggerExit(Collider other)
    {
        _ground_st = false;
    }

    //★
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy" && _st!=5)
        {
            _Enemy = collision.gameObject;
            _EnemyManager = _Enemy.GetComponent<EnemyManager>();

            if (_EnemyManager._st == 1 || _EnemyManager._st == 2)
            {
                _st = 5;
                _timer = 0;
                _animator.Play("BaseDame");
            }
        }
    }
}
