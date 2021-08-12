using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGageManager : MonoBehaviour
{
    //イメージ
    private Image _image;
    //カラー
    private Color _color;

    //HP最大
    public float _MaxHP;
    //HP
    public float _Hp;
    //ステータス
    int _st;
    //カウント
    int _count;
    //ゲージ長さ
    float _gage_l;
    //ターゲットゲージ長さ
    float _gage_t_l;
    //アラーム
    bool _alert_st;

    //_st=1-基本形
    //_st=2-減少
    //_st=3-増加

    void Awake()
    {
        _image = GetComponent<Image>();
        _color = _image.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _MaxHP = 100;
        _Hp = _MaxHP;
        _gage_l = _Hp / 100;
        _alert_st = false;

        _image.fillAmount = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_st==2)
        {
            _gage_l += 0.01f;
            if (_alert_st == true && _gage_l > 0.2)
            {
                _alert_st = false;
                _color.g = 1;
                _color.b = 1;
                _image.color = _color;
            }
            if (_gage_l>=_gage_t_l)
            {
                _gage_l = _gage_t_l;
                _st = 1;
            }
            _image.fillAmount= _gage_l;

        }
        else if (_st == 3)
        {
            _gage_l -= 0.01f;
            if (_alert_st == false && _gage_l <= 0.2)
            {
                _alert_st = true;
                _count = 0;
            }
            if (_gage_l <= _gage_t_l)
            {
                _gage_l = _gage_t_l;
                _st = 1;
            }
            _image.fillAmount = _gage_l;
        }

        //警告
        if (_alert_st)
        {
            if (_count==0)
            {
                _color.g -= 0.05f;
                _color.b -= 0.05f;
                if (_color.g<=0)
                {
                    _color.g = 0;
                    _color.b = 0;
                    _count = 1;
                }
            }
            else
            {
                _color.g += 0.05f;
                _color.b += 0.05f;
                if (_color.g >= 1)
                {
                    _color.g = 1;
                    _color.b = 1;
                    _count = 0;
                }
            }
            _image.color = _color;
        }
    }

    //ゲージ増減セット
    public void GageSet(int _no1, int _no2)
    {
        //_no1=1-増
        //_no2=2-減

        //_no2-増減量

        if (_no1==1) {
            _st = 2;
            _Hp += _no2;

            if (_Hp>=100)
            {
                _Hp = 100;
            }
        }
        else if(_no1==2)
        {
            _st = 3;
            _Hp -= _no2;

            if (_Hp <= 0)
            {
                _Hp = 0;
            }
        }
        _gage_t_l = _Hp / 100;
    }
}
