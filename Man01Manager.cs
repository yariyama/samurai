using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man01Manager : MonoBehaviour
{
    //�X�e�[�^�X
    private int _st;
    //�ړ��X�s�[�h
    public float _speed;
    //�ړ���
    private float _vx;
    private float _vz;

    //_st=1-��{�`
    //_st=2-�ړ�

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        //�c�ړ�
        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }

        //���ړ�
        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0 || _vz!=0)
            {
                _st = 2;
            }
        }
        else if (_st==2)
        {
            if (_vx == 0 && _vz == 0)
            {
                _st = 1;
            }

            this.transform.Translate(_vx/50,0,_vz/50);
        }
    }
}
