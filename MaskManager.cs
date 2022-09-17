using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskManager : MonoBehaviour
{
    //日ボタンオブジェクト
    private GameObject _DateButton;
    //日ボタンスクリプト
    private DateButtonManager _DateButtonManager;
    //日メスオブジェクト
    private GameObject _DateMes;

    //イメージ
    private Image _image;
    //カラー
    private Color _color;
    //日メステキスト
    private Text _date_mes_text;

    //ステータス
    private int _st;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-フェードアウト
    //_st=3-フェードイン


    void Awake()
    {
        _image = GetComponent<Image>();
        _color = _image.color;

        _DateButton = GameObject.Find("DateButton");
        _DateButtonManager = _DateButton.GetComponent<DateButtonManager>();

        _DateMes = transform.Find("DateMes").gameObject;
        _date_mes_text = _DateMes.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 0;
        _DateMes.SetActive(false);
        _image.enabled = false;
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=2)
            {
                _timer = 0;
                _st = 3;
                _DateMes.SetActive(false);
            }
        }
        else if (_st==2)
        {
            _color.a += 0.02f;
            if (_color.a>=1)
            {
                _color.a = 1;
                _st = 1;
                _timer =0;

                ++GameManager._date;
                _DateButtonManager.DateSet();

                _DateMes.SetActive(true);
                _date_mes_text.text = GameManager._date.ToString() + "日経過";
            }
            _image.color = _color;
        }
        else if (_st==3)
        {
            _color.a -= 0.02f;
            if (_color.a <= 0)
            {
                _color.a = 0;
                _st = 0;
                _timer = 0;

                _image.enabled = false;
            }
            _image.color = _color;
        }
    }

    public void OutSet()
    {
        _st = 2;
        _image.enabled = true;
        _color.a = 0;
        _image.color = _color;
    }
}
