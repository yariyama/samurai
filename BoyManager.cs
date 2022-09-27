using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyManager : MonoBehaviour
{
    //�G�l�~�[�I�u�W�F�N�g
    private GameObject _Enemy;
    //�G�l�~�[�X�N���v�g
    private EnemyManager _EnemyManager;
    //�{�[�C���b�V��
    private GameObject _BoyMesh;
    //���C���J�����I�u�W�F�N�g
    private GameObject _MainCamera;


    //�\��
    private SkinnedMeshRenderer _renderer;
    //�}�e���A��
    private Material _material;
    //�F
    private Color _color;

    //���W�b�h�{�f�B
    private Rigidbody _rbody;
    //�A�j���[�^�[
    private Animator _animator;

    //�X�e�[�^�X
    public int _st;
    //�ړ��X�s�[�h
    public float _speed;
    //�ړ���
    private float _vx;
    private float _vz;

    //�W�����v��
    public float _jump_power;
    //������Ă���L��
    private bool _push_st;
    //�ڒn�L��
    private bool _ground_st;
    //�^�C�}�[
    private float _timer;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�W�����v����
    //_st=4-�W�����v
    //_st=5-�_���[�W

    void Awake()
    {
        _rbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _BoyMesh = transform.Find("BoyMesh").gameObject;
        _renderer = _BoyMesh.GetComponent<SkinnedMeshRenderer>();
        _material = _renderer.material;
        _color = _material.color;

        _MainCamera = transform.Find("Main Camera").gameObject;
    }


    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _push_st = false;
        _ground_st = true;
        _animator.Play("Base");
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
            if (!_push_st && _ground_st)
            {
                _push_st = true;
                _st = 3;
            }
        }
        else
        {
            _push_st = false;
        }
    }

    private void FixedUpdate()
    {
        if (_st==1)
        {
            if (_vx!=0||_vz!=0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vx == 0 && _vz== 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st==3)
        {
            _st = 4;
            _rbody.AddForce(new Vector3(0,_jump_power,0),ForceMode.Impulse);
            _animator.Play("BaseJump");
        }
        else if (_st==5)
        {
            _color.a -= 0.01f;
            if (_color.a<=0)
            {
                _st = 0;

                if (_MainCamera.transform.parent==true)
                {
                    _MainCamera.transform.parent = null;
                }
                this.gameObject.SetActive(false);
            }
            _material.color=_color;
        }

        if (_st==2 || _st==4) {
            transform.Translate(_vx / 50, 0, _vz / 50);
        }

        if (!_ground_st)
        {
            if (transform.position.y<0 && _MainCamera.transform.parent)
            {
                _MainCamera.transform.parent = null;
            }
            if (transform.position.y<-20 && _st!=0)
            {
                _st = 0;
                this.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Enemy" && _st==4)
        {
            _Enemy = other.gameObject;
            _EnemyManager = _Enemy.GetComponent<EnemyManager>();
            if (_EnemyManager._st==1||_EnemyManager._st==2)
            {
                _EnemyManager.DameSet();
            }
        }
        else if (other.gameObject.tag=="Cube")
        {
            _ground_st = true;

            if (_st==4)
            {
                _st = 1;
                _animator.Play("JumpBase");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            _ground_st = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy" && _st!=5)
        {
            _Enemy = collision.gameObject;
            _EnemyManager = _Enemy.GetComponent<EnemyManager>();

            if (_EnemyManager._st==1|| _EnemyManager._st == 2)
            {
                _st = 5;
                _timer = 0;
                _animator.Play("BaseDame");
            }
        }
    }
}
