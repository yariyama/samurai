using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy01Manager : MonoBehaviour
{
    //���W�b�h�{�f�B
    private Rigidbody _rdody;
    //�A�j���[�^�[
    private Animator _animator;

    //�X�e�[�^�X
    private int _st;
    //�ړ��X�s�[�h
    public float _speed;
    //�ړ���
    private float _vx;
    private float _vz;

    //�ڒn�L��(true,false)
    private bool _ground_st;
    //������Ă���L��
    private bool _push_st;
    //�W�����v��
    public float _jump_power;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�W�����v����
    //_st=4-�W�����v

    void Awake()
    {
        _rdody = this.GetComponent<Rigidbody>();
        _animator = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Base");
        _ground_st = true;
        _push_st = false;
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

        //�W�����v
        if (Input.GetKey("space"))
        {
            if (_ground_st == true && !_push_st)
            {
                _st = 3;
            }

            _push_st = true;
        }
        else
        {
            _push_st = false;
        }
    }

    void FixedUpdate()
    {
        if (_st == 1)
        {
            if (_vx != 0 || _vz != 0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st == 2)
        {
            if (_vx == 0 && _vz == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }

            this.transform.Translate(_vx / 50, 0, _vz / 50);
        }
        else if (_st == 3)
        {
            _rdody.AddForce(new Vector3(0, _jump_power, 0), ForceMode.Impulse);
            _st = 4;
            _animator.Play("BaseJump");
        }
        else if (_st == 4)
        {
            this.transform.Translate(_vx / 50, 0, _vz / 50);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        _ground_st = true;

        if (_st == 4)
        {
            _st = 1;
            _animator.Play("JumpBase");
        }
    }

    void OnTriggerExit(Collider other)
    {
        _ground_st = false;
    }
}
