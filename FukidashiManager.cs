using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FukidashiManager : MonoBehaviour
{
    //トークマスクオブジェクト
    private GameObject _TalkMask;
    //トークメインスクリプト
    private TalkMainManager _TalkMainManager;
    //吹き出しオブジェクト
    private GameObject _Fukidashi;
    //吹き出しスクリプト
    private FukidashiManager _FukidashiManager;
    //テキストオブジェクト
    private GameObject _Text;

    //イメージ
    private Image _image;
    //スプライト
    public Sprite _spt1;
    public Sprite _spt2;
    //座標
    private Vector2 _position;
    //テキストテキスト
    private Text _text_text;

    //ステータス
    public int _st;
    //バージョン
    public int _ver;
    //位置
    public int _pos_st;
    //トーク
    private int _talk_st;
    //アウトy位置1
    public float _out_y1;
    //アウトy位置2
    public float _out_y2;
    //インy位置1
    public float _in_y1;
    //インy位置2
    public float _in_y2;
    //x位置1
    public float _set_x1;
    //x位置2
    public float _set_x2;
    //タイマー
    private float _timer;
    //質問NO
    private int _qes_no;

    //_st=1-基本形
    //_st=2-スライド

    void Awake()
    {
        _TalkMask = GameObject.Find("TalkMask");
        _TalkMainManager = _TalkMask.GetComponent<TalkMainManager>();

        _image = GetComponent<Image>();
        _position = transform.localPosition;

        _Text = transform.Find("Text").gameObject;
        _text_text = _Text.GetComponent<Text>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _st = 0;
        _pos_st = 0;
        _talk_st = 0;
        _image.enabled = false;
        _text_text.enabled = false;
        _position.y = _out_y1;
        transform.localPosition = _position;
        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_talk_st==1 && _pos_st==1)
            {
                _timer += Time.deltaTime;
                if (_timer>=1)
                {
                    _timer = 0;
                    _TalkMainManager.FukidashiSet(2,_qes_no);
                }
            }
        }
        else if (_st==2)
        {
            _position.y += 10;
            if (_pos_st==0 && _position.y>=_in_y1)
            {
                _position.y = _in_y1;
                _pos_st = 1;
                _st = 1;
                _timer = 0;
            }
            else if (_pos_st == 1 && _position.y >= _in_y2)
            {
                _position.y = _in_y2;
                _pos_st = 2;
                _st = 1;
                _timer = 0;
            }
            else if (_pos_st == 2 && _position.y >= _out_y1)
            {
                _pos_st = 0;
                _st = 0;
                _timer = 0;
                _image.enabled = false;
                _text_text.enabled = false;
            }
            transform.localPosition = _position;
        }
    }

    public void ActiveSet(int _no1,int _no2)
    {
        _image.enabled = true;
        _text_text.enabled = true;

        if (_no1==1)
        {
            _talk_st = 1;
            _image.sprite = _spt2;
            _position.x = _set_x1;

            if (_no2==1)
            {
                _text_text.text = "調子どう？";
            }
            _qes_no = _no2;
        }
        else if (_no1==2)
        {
            _talk_st = 2;
            _image.sprite = _spt1;
            _position.x = _set_x2;

            if (_no2 == 1)
            {
                _text_text.text = "まぁまぁ";
            }
        }
        _position.y = _out_y1;
        transform.localPosition = _position;

        _pos_st = 0;

        SlideSet();
    }

    public void SlideSet()
    {
        _st = 2;

        if (_ver == 1)
        {
            _Fukidashi = GameObject.Find("Fukidashi3");
        }
        else
        {
            _Fukidashi = GameObject.Find("Fukidashi" + (_ver - 1));
        }
        _FukidashiManager = _Fukidashi.GetComponent<FukidashiManager>();

        if (_FukidashiManager._st==1 && _FukidashiManager._pos_st==2)
        {
            _FukidashiManager.SlideSet();
        }
    }
}
