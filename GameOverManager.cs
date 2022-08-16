using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private GameObject _ReplayButton;

    private Vector2 _scale;

    private int _st;

    //_st=1-基本形
    //_st=2-セット

    void Awake()
    {
        _scale = transform.localScale;

        _ReplayButton = GameObject.Find("ReplayButton");
        _ReplayButton.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _scale.y += 0.1f;
            if (_scale.y>=1)
            {
                _scale.y = 1;
                _st = 1;
                _ReplayButton.SetActive(true);
            }
            transform.localScale = _scale;
        }
    }

    //アクティブセット
    public void ActiveSet()
    {
        _st = 2;
        _scale.y = 0;
        transform.localScale = _scale;
    }
}
