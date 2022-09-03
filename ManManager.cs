using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManManager : MonoBehaviour
{
    //マンオブジェクト
    private GameObject _Man;
    //マンスクリプト
    private ManManager _ManManager;
    //バックボタンオブジェクト
    private GameObject _BackButton;
    //バックボタンスクリプト
    private BackButtonManager _BackButtonManager;

    //デモメインUIオブジェクト
    private GameObject _DemoMainUI;
    //デモメインUIスクリプト
    private DemoMainUIManager _DemoMainUIManager;

    //座標
    private Vector2 _position;

    //ステータス
    private int _st;
    //バージョン
    public int _ver;
    //セットx座標
    public float _set_x;
    //セットy座標
    public float _set_y;
    //アウトx座標
    public float _out_x;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-アウトスライド
    //_st=3-アウト
    //_st=4-ウェイト
    //_st=5-インスライド

    void Awake()
    {
        _position = transform.localPosition;

        _BackButton = GameObject.Find("BackButton");
        _BackButtonManager = _BackButton.GetComponent<BackButtonManager>();

        _DemoMainUI = transform.parent.gameObject;
        _DemoMainUIManager = _DemoMainUI.GetComponent<DemoMainUIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_ver==1)
        {
            _st = 1;
            _position.x = _set_x;

            _BackButton.SetActive(false);
        }
        else
        {
            _st = 3;
            _position.x = _out_x;
        }
        _position.y = _set_y;
        transform.localPosition = _position;

        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _position.x += 40;
            if (_position.x>=_out_x)
            {
                _st = 4;
                _timer = 0;
            }
            transform.localPosition = _position;
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _st = 3;
                _timer = 0;
                _ManManager.InSet();
            }
        }
        else if (_st==5)
        {
            _position.x -= 40;
            if (_position.x <= _set_x)
            {
                _st = 1;
                _position.x = _set_x;

                if (_DemoMainUIManager._mode_st!=0)
                {
                    _BackButton.SetActive(true);
                    _BackButtonManager.ActiveSet();
                }
            }
            transform.localPosition = _position;
        }
    }

    public void OutSet(int _no)
    {
        if (_st==1)
        {
            _st = 2;
            _Man = GameObject.Find("Man"+(_no+1));
            _ManManager = _Man.GetComponent<ManManager>();
        }
    }

    public void InSet()
    {
        if (_st==3)
        {
            _st = 5;
        }
    }
}
