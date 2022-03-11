using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    //�p�x
    private Vector3 _angle;
    //�}�E�X�̑O�̍��W
    private Vector3 _last_mouse_position;

    //�X�e�[�^�X
    private int _st;
    //��]�X�s�[�h
    public float _r_speed;

    //_st=1-��{�`
    //_st=2-�ʒu�Z�b�g

    void Awake()
    {
        _angle = transform.localEulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _angle = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _st = 2;
            _last_mouse_position = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {

        }
        else
        {
            _st = 1;
        }
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            //�}�E�X�̈ړ��ʕ��v���C���[����]������
            _angle.y += (Input.mousePosition.x - _last_mouse_position.x)*_r_speed;
            _angle.x -= (Input.mousePosition.y - _last_mouse_position.y) * _r_speed;

            if (_angle.x<=-60)
            {
                _angle.x = -60;
            }
            else if (_angle.x>=30)
            {
                _angle.x = 30;
            }

            transform.localEulerAngles = _angle;

            _last_mouse_position = Input.mousePosition;
        }
    }

    public void ShootRot()
    {
        _angle.x += Random.Range(-0.2f,0.2f);
        _angle.y += Random.Range(-0.2f, 0.2f);
        transform.localEulerAngles = _angle;
    }
}
