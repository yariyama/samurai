using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    //�ړ���x
    //private float _vx;
    //�X�s�[�h
    //public float _speed;
    //��]��y
    private float _angley;
    //��]�X�s�[�h
    public float _a_speed;

    //�X�e�[�^�X
    private int _st;

    //_st=1-��{�`
    //_st=2-��]


    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        //_vx = _speed;
        _angley = _a_speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //this.transform.Translate(_vx / 50, 0, 0);
        if (_st==2) {
            this.transform.Rotate(0, _angley / 50, 0);
        }
    }

    void OnMouseDown()
    {
        if (_st==1)
        {
            _st = 2;
        }
        else if (_st==2)
        {
            _st = 1;
        }
    }

    //void OnMouseUp()
    //{
    //    if (_st == 2)
    //    {
    //        _st = 1;
    //    }
    //}
}
