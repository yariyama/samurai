using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームマネージャー
    private GameManager _GameManager;

    //スケール
    private Vector2 _scale;

    //ステータス
    private int _st;

    //_st=1-基本形
    //_st=2-セット

    void Awake()
    {
        _GameMain = GameObject.Find("GameMain");
        _GameManager = _GameMain.GetComponent<GameManager>();

        _scale = transform.localScale;
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
    }
}
