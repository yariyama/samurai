using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : MonoBehaviour
{
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームマネージャー
    private GameManager _GameManager;

    //座標
    private Vector3 _position;
    //オーディオソース
    private AudioSource _audio;
    //効果音
    public AudioClip _se1;

    //ステータス
    private int _st;

    //_st=1-基本形
    //_st=2-スライド


    void Awake()
    {
        _GameMain = GameObject.Find("GameMain");
        _GameManager = _GameMain.GetComponent<GameManager>();

        _position = transform.localPosition;
        _audio = GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        if (_st==1)
        {

        }
        else if (_st==2)
        {
            _position.x -= 30f;
            if (_position.x<=0)
            {
                _position.x = 0;
                _st = 1;
            }
            transform.localPosition = _position;
        }
    }

    public void ActiveSet()
    {
        _st = 2;
        _position.x = 770;
        transform.localPosition = _position;

        _GameManager.BGMStop();
        _audio.clip = _se1;
        _audio.Play();
    }
}
