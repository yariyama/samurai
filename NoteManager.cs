using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteManager : MonoBehaviour
{
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームメインスクリプト
    private GameMainManager _GameMainManager;
    //音符スコアオブジェクト
    private GameObject _NoteScore;
    //スコアオブジェクト
    private GameObject _Score;
    //テストNOオブジェクト
    private GameObject _TestNo;

    //座標
    private Vector2 _position;
    //アニメーター
    private Animator _animator;
    //スコアテキスト
    private Text _score_text;
    //テストNOテキスト
    private TextMeshPro _test_no_text;

    //ステータス
    public int _st;
    //バージョン
    public int _ver;
    //ラインNO
    private int _line_no;
    //色タイプ
    private int _color_tp;

    //ノーマルスピード
    public float _n_speed;
    //ダメスピード
    public float _d_speed;
    //グッドスピード
    public float _g_speed;

    //タイマー
    private float _timer;
    //カウント
    private int _count;

    //テスト有無
    public bool _test_st=true;


    //_st=1-基本移動
    //_st=2-ダメージ
    //_st=3-グッド

    void Awake()
    {
        _GameMain = GameObject.Find("GameMain");
        _GameMainManager = _GameMain.GetComponent<GameMainManager>();
        _Score = GameObject.Find("Score");

        _NoteScore = transform.Find("NoteScore").gameObject;

        _TestNo = transform.Find("TestNo").gameObject;
        _test_no_text = _TestNo.GetComponent<TextMeshPro>();

        _position = transform.position;
        _animator = GetComponent<Animator>();
        _score_text = _Score.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _timer =0;
        _count = 0;

        _NoteScore.SetActive(false);
    }

    void FixedUpdate()
    {
        //基本移動
        if (_st==1)
        {
            transform.Translate(-_n_speed/50,0,0);

            if (transform.position.x<=-4)
            {
                _st = 2;
                if (_color_tp == 1)
                {
                    _animator.Play("red_dame");
                }
                else if (_color_tp == 2)
                {
                    _animator.Play("blue_dame");
                }
                else if (_color_tp == 3)
                {
                    _animator.Play("yellow_dame");
                }

                _NoteScore.SetActive(true);
                _timer = 0;
                _count = 0;

                GameManager._score -= 10;
                _score_text.text = GameManager._score.ToString();
            }
        }
        //ダメージ
        else if (_st==2)
        {
            //移動
            transform.Translate(-_d_speed / 50, 0, 0);

            if (transform.position.x <= -7.7f)
            {
                _GameMainManager._note_st[_ver] = false;
                Destroy(this.gameObject);
            }

            //テキスト表示
            if (_count==0)
            {
                _timer += Time.deltaTime;
                if (_timer>=0.3f)
                {
                    _timer = 0;
                    _count = 1;
                    _NoteScore.SetActive(false);
                }
            }
        }
        //成功
        else if (_st==3)
        {
            transform.Translate(-_g_speed / 50, 0, 0);
        }
    }

    //アクティブ化
    public void ActiveSet(int _no1,int _no2)
    {
        _line_no = _no1;
        _st = 1;

        _color_tp = Random.Range(1,4);
        if (_color_tp==1)
        {
            _animator.Play("red_base");
        }
        else if (_color_tp == 2)
        {
            _animator.Play("blue_base");
        }
        else if (_color_tp == 3)
        {
            _animator.Play("yellow_base");
        }

        _position.x = 7.6f;
        _position.y = 3.5f-2*(_line_no-1);
        transform.position = _position;

        if (_test_st)
        {
            _test_no_text.text = (_no2 - 1).ToString("00");
        }
        else
        {
            _TestNo.SetActive(false);
        }
    }

    //成功セット
    public void GoodSet()
    {
        _st = 3;

        if (_color_tp == 1)
        {
            _animator.Play("red_good");
        }
        else if (_color_tp == 2)
        {
            _animator.Play("blue_good");
        }
        else if (_color_tp == 3)
        {
            _animator.Play("yellow_good");
        }
    }

    //消滅セット
    void DelSet()
    {
        _GameMainManager._note_st[_ver] = false;
        Destroy(this.gameObject);
    }
}
