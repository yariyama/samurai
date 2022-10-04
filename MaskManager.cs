using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MaskManager : MonoBehaviour
{
    //イメージ
    private Image _image;
    //カラー
    private Color _color;

    //ステータス
    private int _st;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-フェードアウト
    //_st=3-フェードアウト後
    //_st=4-フェードイン

    void Awake()
    {
        _image = GetComponent<Image>();
        _color = _image.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager._scene_change) {
            _image.enabled = true;
            _st = 4;
            GameManager._scene_change = false;
        }
        else
        {
            _image.enabled = false;
            _st = 0;
            
        }
        _timer = 0;
    }

    void FixedUpdate()
    {
        
        if (_st==2)
        {
            _color.a += 0.05f;
            if (_color.a>=1)
            {
                _color.a = 1;
                _st = 3;
                _timer = 0;
            }
            _image.color = _color;
        }
        else if (_st == 3)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1)
            {
                GameManager._scene_change = true;
                SceneManager.LoadScene("Test02");
            }
        }
        else if (_st == 4)
        {
            _color.a -= 0.05f;
            if (_color.a <= 0)
            {
                _color.a = 0;
                _st = 0;
                _image.enabled = false;
            }
            _image.color = _color;
        }
    }

    public void OutSet()
    {
        _image.enabled = true;
        _st = 2;
        _color.a = 0;
        _image.color = _color;
    }
}
