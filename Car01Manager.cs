using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car01Manager : MonoBehaviour
{
    //�X�e�[�^�X
    private int _st;
    //�X�s�[�h
    public float _speed;

    //_st=1-��{�`
    //_st=2-�ړ�

    // Start is called before the first frame update
    void Start()
    {
        _st = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            transform.Translate(0,0,_speed/50);
        }
    }
}
