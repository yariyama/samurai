using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGageManager : MonoBehaviour
{
    //まるオブジェクト
    private GameObject _Maru;

    //イメージ
    private Image _image;
    //スプライト
    public Sprite _spt1;
    public Sprite _spt2;

    //ステータス
    private int _st;
    //バージョン
    public int _ver;

    //_st=1-基本形
    //_st=2-半分

    void Awake()
    {
        _Maru = transform.Find("Maru").gameObject;

        _image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _image.sprite = _spt1;

        if (_ver!=3)
        {
            _Maru.SetActive(false);
        }
    }

    public void DeSet(int _no)
    {
        if (_no==1)
        {
            _st = 2;
            _image.sprite = _spt2;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void MaruSet()
    {
        _Maru.SetActive(true);
    }
}
