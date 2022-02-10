using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームメインスクリプト
    private GameMainManager _GameMainManager;
    //スコアオブジェクト
    private GameObject _Score;

    //座標
    private Vector2 _position;
    //アニメーション
    private Animator _animator;
    //スコアテキスト
    private Text _score_text;

    //バージョン
    public int _ver;
    //ステータス
    public int _st;
    //ラインNO
    private int _line_no;
    //色タイプ
    private int _color_tp;

    //ノーマルスピード
    public float _n_speed=5;
    //ダメスピード
    public float _d_speed = 2;
    //グッドスピード
    public float _g_speed = 1.5f;

    //_st=1-基本移動
    //_st=2-ダメージ
    //_st=3-成功

    void Awake()
    {
        _GameMain = GameObject.Find("GameMain");
        _GameMainManager = _GameMain.GetComponent<GameMainManager>();
        _Score = GameObject.Find("Score");

        _position = transform.position;
        _animator = GetComponent<Animator>();
        _score_text = _Score.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //基本移動
        if (_st==1)
        {
            transform.Translate(-_n_speed/50,0,0);

            if (transform.position.x<=-4f)
            {
                _st = 2;
                if (_color_tp==1)
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

                GameManager._score -= 10;
                _score_text.text = GameManager._score.ToString();
            }
        }
        //ダメージ
        else if (_st==2)
        {
            transform.Translate(-_d_speed/50,0,0);
            if (transform.position.x<=-7.7f)
            {
                _GameMainManager._note_st[_ver] = false;
                Destroy(this.gameObject);
            }
        }
        //成功
        else if (_st==3)
        {
            transform.Translate(-_g_speed / 50, 0, 0);
        }
    }

    //アクティブ化
    public void ActiveSet(int _no1)
    {
        _line_no = _no1;
        _st = 1;
        _color_tp = Random.Range(1,4);
        if (_color_tp==1) {
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
