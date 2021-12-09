using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームマネージャー
    private GameManager _GameManager;
    //リプレイボタンオブジェクト
    private GameObject _ReplayButton;

    //スケール
    private Vector2 _scale;
    //オーディオソース
    private AudioSource _audio;
    //効果音
    public AudioClip _se1;

    //ステータス
    private int _st;

    //_st=1-基本形
    //_st=2-セット


    void Awake()
    {
        _GameMain = GameObject.Find("GameMain");
        _GameManager = _GameMain.GetComponent<GameManager>();
        _ReplayButton = GameObject.Find("ReplayButton");
        _ReplayButton.SetActive(false);

        _scale = transform.localScale;
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _scale.y += 0.1f;
            if (_scale.y>=1)
            {
                _scale.y = 1;
                _st = 1;
                _ReplayButton.SetActive(true);
            }
            transform.localScale = _scale;
        }
    }

    public void ActiveSet()
    {
        _st = 2;
        _scale.y = 0;
        transform.localScale = _scale;

        _GameManager.BGMStop();
        _audio.clip = _se1;
        _audio.Play();
    }
}
