using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateButtonManager : MonoBehaviour
{
    //テキストオブジェクト
    private GameObject _Text;
    //マスクオブジェクト
    private GameObject _Mask;
    //マスクスクリプト
    private MaskManager _MaskManager;
    //トークマスクオブジェクト
    private GameObject _TalkMask;
    //トークメインスクリプト
    private TalkMainManager _TalkMainManager;

    //スケール
    private Vector2 _scale;
    //テキストテキスト
    private Text _text_text;

    //ステータス
    private int _st;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-プッシュ

    void Awake()
    {
        _scale = transform.localScale;

        _Text = transform.Find("Text").gameObject;
        _text_text = _Text.GetComponent<Text>();

        _Mask = GameObject.Find("Mask");
        _MaskManager = _Mask.GetComponent<MaskManager>();

        _TalkMask = GameObject.Find("TalkMask");
        _TalkMainManager = _TalkMask.GetComponent<TalkMainManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _timer = 0;
        DateSet();
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer = 0;
                _st = 1;
                _scale.x = 1;
                _scale.y = 1;
                transform.localScale = _scale;

                _MaskManager.OutSet();
            }
        }
    }

    public void ButtonPush()
    {
        if (_st==1 && _TalkMainManager._st==0)
        {
            _st = 2;
            _timer =0;
            _scale.x = 0.9f;
            _scale.y = 0.9f;
            transform.localScale = _scale;
        }
    }

    public void DateSet()
    {
        _text_text.text = GameManager._date.ToString()+"日経過";
    }
}
