using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStartManager : MonoBehaviour
{
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームメインスクリプト
    private GameMainManager _GameMainManager;

    //座標
    private Vector2 _position;
    //テキスト
    private Text _text;

    //ステータス
    private int _st;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-スライド
    //_st=3-掃ける
    //_st=4-ウェイト

    void Awake()
    {
        _position = transform.localPosition;
        _text = GetComponent<Text>();

        _GameMain = GameObject.Find("GameMain");
        _GameMainManager = _GameMain.GetComponent<GameMainManager>();
    }

    void Start()
    {
        _text.enabled = false;
        _st = 4;
        _timer =0;
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=1.5f)
            {
                _timer = 0;
                _st = 3;
            }
        }
        else if (_st==2)
        {
            _position.x -= 30;
            if (_position.x<=0)
            {
                _position.x = 0;
                _st = 1;
            }
            transform.localPosition = _position;
        }
        else if (_st == 3)
        {
            _position.x -= 30;
            if (_position.x <= -800)
            {
                _GameMainManager.BGMStrat();
                this.gameObject.SetActive(false);
            }
            transform.localPosition = _position;
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _timer = 0;
                ActiveSet();
            }
        }
    }

    public void ActiveSet()
    {
        _text.enabled = true;
        _text.text = GameManager._stage.ToString() + "STAGE";
        _st = 2;
        _position.x = 800;
        transform.localPosition = _position;
    }
}
