using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animator;
    //���W�b�h�{�f�B
    private Rigidbody _rbody;

    //�X�e�[�^�X
    private int _st;
    //�J�E���g
    private int _count;
    //�^�C�}�[
    private float _timer;
    //�ړ���Z
    private float _vz;
    //�ړ��X�s�[�h
    public float _w_speed;
    //��]��
    private float _angle;
    //��]�X�s�[�h
    public float _a_speed;
    //�W�����v��
    public float _jump_p;
    //�v�b�V���L��
    private bool _push_st;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�W�����v�O
    //_st=4-�W�����v
    //_st=5-�W�����v���n

    void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _rbody = this.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _count = 0;
        _timer = 0;
        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vz = 0;
        _angle = 0;
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
            _angle = _a_speed;
        }
        else if (Input.GetKey("left"))
        {
            _angle = -_a_speed;
        }

        if (Input.GetKey("space"))
        {
            if (_push_st==false && _st<=2)
            {
                _push_st = true;
                _st = 3;
                _timer = 0;
                _animator.Play("JumpSet");
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
            if (_vz!=0||_angle!=0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vz == 0 && _angle == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st==3)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer = 0;
                _st = 4;
                _count = 0;
                _rbody.AddForce(new Vector3(0,_jump_p,0),ForceMode.Impulse);
                _animator.Play("JumpUp");
            }
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_count==0)
            {
                if (_timer>=0.3f)
                {
                    _timer =0;
                    _count = 1;
                    _animator.Play("JumpTop");
                }
            }
            else if (_count==1)
            {
                if (_timer >= 0.2f)
                {
                    _timer = 0;
                    _count = 2;
                    _animator.Play("JumpDown");
                }
            }
        }
        else if (_st==5)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _timer = 0;
                _st = 1;
                _animator.Play("Base");
            }
        }

        if (_st == 2 ||_st==4) 
        {
            if (_vz != 0)
            {
                transform.Translate(0, 0, _vz / 50);
            }
            if (_angle != 0)
            {
                transform.Rotate(0, _angle / 50, 0);
            }
        }
    }

        void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Cube" && _st==4)
        {
            _st = 5;
            _count = 0;
            _timer = 0;
            _animator.Play("JumpArrive");
        }
    }
}
