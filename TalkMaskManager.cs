using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkMaskManager : MonoBehaviour
{
    //���W
    private Vector2 _position;

    //�X�e�[�^�X
    public int _st;
    //�A�E�g�Z�b�gx
    public float _out_set_x;
    //�C���Z�b�gx
    public float _in_set_x;
    //�X�s�[�h
    public float _speed;

    //_st=1-��{�`
    //_st=2-�ҋ@
    //_st=3-�X���C�h�C��
    //_st=4-�X���C�h�A�E�g

    void Awake()
    {
        _position = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 2;
        _position.x = _out_set_x;
        transform.localPosition = _position;
    }

    void FixedUpdate()
    {
        if (_st==3)
        {
            _position.x += _speed;
            if (_position.x>=_in_set_x)
            {
                _position.x = _in_set_x;
                _st = 1;
            }
            transform.localPosition = _position;
        }
        else if (_st==4)
        {
            _position.x -= _speed;
            if (_position.x <= _out_set_x)
            {
                _position.x = _out_set_x;
                _st = 2;
            }
            transform.localPosition = _position;
        }
    }

    //�X���C�h�Z�b�g
    public void SlideSet(int _no)
    {
        if (_no==1)
        {
            _st = 3;
        }
        else if (_no==2)
        {
            _st = 4;
        }
        _position = transform.localPosition;
    }
}
