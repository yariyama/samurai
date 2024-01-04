using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animtor;
    //���W�b�h�{�f�B
    private Rigidbody _rbody;

    //�X�s�[�h
    public float _speed;
    //�ړ���x
    //private float _vx;
    //�ړ���z
    private float _vz;
    //��]��
    private float _angle;
    //��]�X�s�[�h
    public float _a_speed;
    //�W�����v�p���[
    public float _jump_p;

    //�X�e�[�^�X
    public int _st;
    //�J�E���g
    private int _count;
    //�^�C�}�[
    private float _timer;

    //�v�b�V���L��
    private bool _push_st;
    //�ڒn�L��
    private bool _ground_st;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�W�����v�O
    //_st=4-�W�����v
        //_count=0-�㏸
        //_count=1-�g�b�v
        //_count=2-���~
    //_st=5-�W�����v��
    //_st=6-�U��
    //_st=7-�U����

    void Awake()
    {
        //��`
        _animtor = this.GetComponent<Animator>();
        _rbody = this.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _ground_st = true;
        _animtor.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        //_vx = 0;
        _vz = 0;
        _angle = 0;

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            _vz = -_speed;
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            //_vx = _speed;
            _angle = _a_speed;
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            //_vx = -_speed;
            _angle = -_a_speed;
        }

        if (Input.GetKey("space"))
        {
            if ((_st==1||_st==2)&&_push_st==false && _ground_st==true)
            {
                _push_st = true;
                _st = 3;
                _timer =0;
                _animtor.Play("JumpSet");
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
            if (_angle!=0 || _vz!=0)
            {
                _st = 2;
                _animtor.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_angle == 0 && _vz == 0)
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
                _timer = 0;
                _st = 4;
                _count = 0;
                _rbody.AddForce(new Vector3(0,_jump_p,0),ForceMode.Impulse);
                _animtor.Play("JumpUp");
            }
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_count==0 && _timer>=0.2f)
            {
                _timer = 0;
                _count = 1;
                _animtor.Play("JumpTop");
            }
            else if (_count==1 && _timer>=0.2f)
            {
                _timer = 0;
                _count = 2;
                _animtor.Play("JumpDown");
            }
        }
        else if (_st == 5)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _timer = 0;
                _st = 1;
                _animtor.Play("Base");
            }
        }
        else if (_st==6)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                _timer = 0;
                _st = 7;
                _animtor.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        if (_st==2 || _st==4)
        {
            this.transform.Translate(0, 0, _vz / 50);
            this.transform.Rotate(0,_angle/50,0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Monkey" && _st==4)
        {
            _st = 6;
            _timer = 0;
            _animtor.Play("JumpAttack");
            _rbody.constraints = RigidbodyConstraints.FreezePosition;
        }
        else if (collision.gameObject.tag == "Cube" && (_st == 4||_st==7))
        {
            _st = 5;
            _timer = 0;
            _animtor.Play("JumpArrive");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Cube")
        {
            _ground_st = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            _ground_st = false;
        }
    }
}
