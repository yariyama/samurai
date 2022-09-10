using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterManager : MonoBehaviour
{
    //ゲットポイントオブジェクト
    private GameObject _GetPoint;

    //テキスト
    private Text _text;
    //ゲットポイントテキスト
    private Text _get_point_text;

    //ステータス
    private int _st;
    //バージョン
    public int _ver;
    //カウント
    private int _count;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-加算

    void Awake()
    {
        _text = GetComponent<Text>();

        _GetPoint = GameObject.Find("GetPoint");
        _get_point_text = _GetPoint.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (GameManager._get_point>=10) {
                _timer += Time.deltaTime;
                if (_timer >= 2)
                {
                    if (_count == 0) {
                        _count = 1;
                        _timer = 0;

                        if (_ver == 1)
                        {
                            _text.text = "メンタル " + GameManager._mental.ToString() + " + 1";
                        }
                        else if (_ver == 2)
                        {
                            _text.text = "最大正気度 " + GameManager._sanity_max.ToString() + " + 1";
                        }
                        else if (_ver == 3)
                        {
                            _text.text = "最大心拍数 " + GameManager._heart_max.ToString() + " + 1";
                        }
                    }
                    else if (_count == 1)
                    {
                        _st = 2;
                        _timer = 0;

                        if (_ver == 1)
                        {
                            GameManager._mental += 1;
                            GameManager._get_point -= 10;
                            _text.text = "メンタル " + GameManager._mental.ToString();
                        }
                        else if (_ver == 2)
                        {
                            GameManager._sanity_max += 1;
                            GameManager._get_point -= 10;
                            _text.text = "最大正気度 " + GameManager._sanity_max.ToString();
                        }
                        else if (_ver == 3)
                        {
                            GameManager._heart_max += 1;
                            GameManager._get_point -= 10;
                            _text.text = "最大心拍数 " + GameManager._heart_max.ToString();
                        }
                        _get_point_text.text = GameManager._get_point.ToString("0000");
                    }
                }
            }
        }
    }

    public void TextSet()
    {
        _st = 1;
        _count = 0;
        _timer = 0;

        if (_ver==1)
        {
            _text.text = "メンタル "+GameManager._mental.ToString();
        }
        else if (_ver == 2)
        {
            _text.text = "最大正気度 " + GameManager._sanity_max.ToString();
        }
        else if (_ver == 3)
        {
            _text.text = "最大心拍数 " + GameManager._heart_max.ToString();
        }
    }
}
