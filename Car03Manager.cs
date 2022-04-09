using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car03Manager : MonoBehaviour
{
    //���W
    private Vector3 _position;

    //�X�e�[�^�X
    private int _st;
    //�X�s�[�h
    public float _speed;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�߂�

    void Awake()
    {
        _position = this.transform.position;
    }

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
        if (_st == 2)
        {
            transform.Translate(0, 0, _speed / 50);
        }
        else if (_st==3)
        {
            _position.z -= 0.5f;
            this.transform.position = _position;
            _st = 1;
        }
    }

    void OnMouseDown()
    {
        if (_st == 1)
        {
            _st = 2;
        }
        else if (_st == 2)
        {
            _st = 1;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name=="Cube")
        {
            _st = 3;
            _position = this.transform.position;
        }
    }
}
