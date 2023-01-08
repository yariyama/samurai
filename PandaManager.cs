using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animtor;
    //���W�b�h�{�f�B
    private Rigidbody _rbody;

    //�ړ���x
    private float _vx;
    //�ړ���z
    private float _vz;
    //�X�s�[�h
    public float _speed;
    //�W�����v��
    public float _jump_power;
    //�v�b�V���L��
    private bool _push_st;
    //�^�C�}�[
    private float _timer;

    //�X�e�[�^�X
    private int _st;
    //�J�E���g
    private int _count;

    //_st=1-��{�`
    //_st=2-��]
    //_st=3-�W�����v�O
    //_st=4-�W�����v
    //_st=5-���n

    void Awake()
    {
        _animtor = GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animtor.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        if (Input.GetKey("up"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }

        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }

        if (Input.GetKey("space"))
        {
            if (_push_st==false) {
                _push_st = true;
                _st = 3;
                _timer = 0;
                _animtor.Play("JumpSet");
                //_rbody.AddForce(new Vector3(0, _jump_power, 0), ForceMode.Impulse);
            }
        }
        else
        {
            _push_st = false;
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0 || _vz!=0)
            {
                _st = 2;
                _animtor.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vx == 0 && _vz == 0)
            {
                _st = 1;
                _animtor.Play("Base");
            }
        }
        else if (_st==3)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer =0;
                _st = 4;
                _count =0;
                _animtor.Play("JumpUp");
                _rbody.AddForce(new Vector3(0, _jump_power, 0), ForceMode.Impulse);
            }
        }
        else if (_st==4)
        {

        }
        else if (_st==5)
        {

        }

        transform.Translate(_vx / 50, 0, _vz / 50);
    }
}
