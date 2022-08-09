using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private Vector2 _scale;

    private int _st;

    //_st=1-��{�`
    //_st=2-�Z�b�g

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
            _scale.y += 0.1f;
            if (_scale.y>=1)
            {
                _scale.y = 1;
                _st = 1;
            }
            transform.localScale = _scale;
        }
    }

    //�A�N�e�B�u�Z�b�g
    public void ActiveSet()
    {
        _st = 2;
        _scale.y = 0;
        transform.localScale = _scale;
    }
}
