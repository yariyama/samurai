using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButtonManager : MonoBehaviour
{
    //トークマスクオブジェクト
    private GameObject _TalkMask;
    //トークメインスクリプト
    private TalkMainManager _TalkMainManager;

    //スケール
    private Vector2 _scale;

    //ステータス
    private int _st;
    //バージョン
    public int _ver;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-プッシュ


    void Awake()
    {
        _scale = transform.localScale;

        _TalkMask = transform.parent.gameObject;
        _TalkMainManager = _TalkMask.GetComponent<TalkMainManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _st = 1;
                _timer = 0;
                _scale.x = 1f;
                _scale.y = 1f;
                transform.localScale = _scale;

                _TalkMainManager.FukidashiSet(1,_ver);
            }
        }
    }

    public void ButtonPush()
    {
        if (_TalkMainManager._st==1) {
            _st = 2;
            _timer = 0;
            _scale.x = 0.9f;
            _scale.y = 0.9f;
            transform.localScale = _scale;
        }
    }
}
