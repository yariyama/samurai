using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMTitleManager : MonoBehaviour
{
    //テキスト
    private Text _text;

    //ステータス
    private int _st;
    //タイプ
    private int _tp;
    //タイマー
    private float _timer;

    //_st=1-基本形


    void Awake()
    {
        _text = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=2)
            {
                _timer = 0;
                _st = 0;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void ActiveSet(int _no)
    {
        _st = 1;
        _tp = _no;

        if (_tp==1)
        {
            _text.text = "おもちゃのマーチ♫";
        }
    }
}
