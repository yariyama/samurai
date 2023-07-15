using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    //スケール
    private Vector2 _scale;

    //オーディオソース
    private AudioSource _audio;
    //セット音
    public AudioClip _set_se;

    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームメインスクリプト
    private GameMainManager _GameMainManager;

    //ステータス
    private int _st;

    //_st=1-基本形
    //_st=2-セット

    void Awake()
    {
        _scale = transform.localScale;
        _audio = GetComponent<AudioSource>();

        _GameMain = GameObject.Find("GameMain");
        _GameMainManager = _GameMain.GetComponent<GameMainManager>();
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _scale.x -= 0.1f;
            _scale.y -= 0.1f;
            if (_scale.x<=1)
            {
                _scale.x = 1;
                _scale.y = 1;
                _st = 1;

                _GameMainManager.BGMStop();

                _audio.clip = _set_se;
                _audio.Play();
            }
            transform.localScale = _scale;
        }
    }

    public void ActiveSet()
    {
        _st = 2;
        _scale.x = 3;
        _scale.y = 3;
        transform.localScale = _scale;
    }
}
