using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animator;

    //�X�e�[�^�X
    private int _st;
    //�ړ���z
    private float _vz;
    //�ړ���x
    private float _vx;
    //�ړ��X�s�[�h
    public float _w_speed;
    //��]��
    private float _angle;
    //��]�X�s�[�h
    public float _a_speed;

    //�}�E�X���W
    private Vector3 _mouse;
    private Vector3 _target;

    //_st=1-��{�`
    //_st=2-�ړ�

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        _vz = 0;
        _vx = 0;

        if (Input.GetKey("up"))
        {
            _vz = _w_speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_w_speed;
        }
        if (Input.GetKey("right"))
        {
            _vx = _w_speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_w_speed;
        }

        _mouse = Input.mousePosition;
        _mouse.z = 10;
        _target = Camera.main.ScreenToWorldPoint(_mouse);
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vz!=0||_vx!=0)
            {
                _st = 2;
                _animator.Play("Walking");
            }
        }
        else if (_st==2)
        {
            if (_vz == 0 && _vx == 0)
            {
                _st = 1;
                _animator.Play("Idle");
            }
        }

        transform.Translate(_vx/50,0,_vz/50);

        if (_target.x<-1.5f)
        {
            _angle = -_a_speed;
            transform.Rotate(0,_angle/50,0);
        }
        else if (_target.x > 1.5f)
        {
            _angle = _a_speed;
            transform.Rotate(0, _angle / 50, 0);
        }
    }
}
