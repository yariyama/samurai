using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    //スケール
    private Vector2 _scale;

    //ステータス
    private int _st;


    //_st=1-基本形
    //_st=2-セット

    void Awake()
    {
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
            _scale.x -= 0.1f;
            _scale.y -= 0.1f;
            if (_scale.x<=1)
            {
                _scale.x = 1;
                _scale.y = 1;
                _st = 1;
            }
            transform.localScale = _scale;
        }
    }

    public void ActiveSet()
    {
        _st = 2;
        _scale.x = 3;
        _scale.y = 3;
        transform.localScale = _scale;
    }
}
