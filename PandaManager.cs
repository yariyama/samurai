using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //���W�b�h�{�f�B
    private Rigidbody2D _rbody;
    //�A�j���[�^�[
    private Animator _animator;
    //�X�P�[��
    private Vector2 _scale;

    //�X�e�[�^�X
    public int _st;
    //�ړ��X�s�[�h
    public float _w_speed;
    //�W�����v��
    public float _j_speed;
    //x�ړ���
    private float _vx;
    //x����
    private int _dire_x;
    //�^�C�}�[
    private float _timer;
    //�ڒn
    private bool _ground_st;
    //����
    private bool _push_st;
    //�W�����v�_�E��
    public bool _jump_down;


    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�U�����
    //_st=4-�W�����v�O
    //_st=5-�W�����v
    //_st=6-�W�����v��

    void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire_x = 1;
        _timer = 0;
        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        if (Input.GetKey("right"))
        {
            _vx = _w_speed;
            if (_dire_x==2)
            {
                if (_st==1||_st==2) {
                    _st = 3;
                    _timer = 0;
                }
            }
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_w_speed;
            if (_dire_x == 1)
            {
                if (_st == 1 || _st == 2)
                {
                    _st = 3;
                    _timer = 0;
                }
            }
        }

        if (Input.GetKey("space"))
        {
            if (!_push_st && _ground_st)
            {
                _push_st = true;
                _st = 4;
                _timer = 0;
                _jump_down = false;
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
            if (_vx!=0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vx == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st==3)
        {
            if (_timer==0)
            {
                _animator.Play("Turn");
            }
            _timer += Time.deltaTime;
            if (_timer>=0.1f)
            {
                _timer =0;
                _st = 1;
                if (_dire_x==1)
                {
                    _dire_x = 2;
                    _scale.x = -1.3f;
                }
                else
                {
                    _dire_x = 1;
                    _scale.x = 1.3f;
                }
                _animator.Play("Base");
                transform.localScale = _scale;
            }
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer = 0;
                _st = 5;
                _rbody.AddForce(new Vector2(0,_j_speed),ForceMode2D.Impulse);
                _animator.Play("Jump");
            }
        }
        else if (_st==6)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.2f)
            {
                _timer = 0;
                _st = 1;
                _animator.Play("Base");
            }
            
        }

        if (_vx!=0 && (_st==2||_st==5))
        {
            transform.Translate(_vx/50,0,0);
        }
    }

    //���n���f
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Cloud")
        {
            _ground_st = true;

            if (_st==5 && _jump_down)
            {
                _st = 6;
                _timer = 0;
                _animator.Play("JumpSet");
            }
        }
    }

    //�������f
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cloud")
        {
            _ground_st = false;
        }
    }

    void JumpDown()
    {
        _jump_down = true;
    }
}
