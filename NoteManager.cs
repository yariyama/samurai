using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームメインスクリプト
    private GameMainManager _GameMainManager;

    //座標
    private Vector2 _position;
    //アニメーター
    private Animator _animator;

    //ステータス
    private int _st;
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

    //_st=1-基本移動
    //_st=2-ダメージ

    void Awake()
    {
        _GameMain = GameObject.Find("GameMain");
        _GameMainManager = _GameMain.GetComponent<GameMainManager>();

        _position = transform.position;
        _animator = GetComponent<Animator>();
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
            }
        }
        //ダメージ
        else if (_st==2)
        {
            transform.Translate(-_d_speed / 50, 0, 0);

            if (transform.position.x <= -7.7f)
            {
                _GameMainManager._note_st[_ver] = false;
                Destroy(this.gameObject);
            }
        }
    }

    //アクティブ化
    public void ActiveSet(int _no)
    {
        _line_no = _no;
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
    }
}
