using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2Manager : MonoBehaviour
{
    //�X�s�[�h
    public float _speed;
    //�X�e�[�^�X
    private int _st;

    //_st=1-�O�ړ�
    //_st=2-��ړ�


    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void FixedUpdate()
    {
        if (_st == 1)
        {
            this.transform.Translate(_speed / 50, 0, 0);
        }
        else if (_st == 2)
        {
            this.transform.Translate(-_speed / 50, 0, 0);
        }
    }

    void OnMouseDown()
    {
        if (_st==1)
        {
            _st = 2;
        }
        else
        {
            _st = 1;
        }
    }
}
