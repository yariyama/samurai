using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face01Manager : MonoBehaviour
{
    //�X�e�[�^�X
    private int _st;
    //��]�X�s�[�h
    public float _speed;

    //_st=1-��{�`
    //_st=2-��]

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            transform.Rotate(0,_speed/50,0);
        }
    }

    void OnMouseDown()
    {
        if (_st==1)
        {
            _st = 2;
        }
    }

    void OnMouseUp()
    {
        if (_st == 2)
        {
            _st = 1;
        }
    }
}
