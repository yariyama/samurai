using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //�����L�[�I�u�W�F�N�g
    private GameObject _Monkey;
    //�����L�[�X�N���v�g
    private MonkeyManager _MonkeyManager;

    //�A�j���[�^�[
    private Animator _animtor;
    //���W�b�h�{�f�B
    private Rigidbody _rbody;
    //�R���C�_�[
    private BoxCollider _collider;

    //�p���_���b�V���I�u�W�F�N�g
    private GameObject _PandaMesh;
    //�p���_���b�V���\��
    private SkinnedMeshRenderer _panda_mesh_renderer;

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
    //��]��
    private float _angle;
    //��]�X�s�[�h
    public float _angle_speed;

    //�X�e�[�^�X
    public int _st;
    //�J�E���g
    private int _count;

    //_st=1-��{�`
    //_st=2-��]
    //_st=3-�W�����v�O
    //_st=4-�W�����v
    //_st=5-���n
    //_st=6-�_���[�W
    //_st=7-�U��
    //_st=8-�U����

    void Awake()
    {
        _animtor = GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();

        _PandaMesh = transform.Find("PandaMesh").gameObject;
        _panda_mesh_renderer = _PandaMesh.GetComponent<SkinnedMeshRenderer>();
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
        //_vx = 0;
        _vz = 0;
        _angle = 0;

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
            //_vx = _speed;
            _angle = _angle_speed;
        }
        else if (Input.GetKey("left"))
        {
            //_vx = -_speed;
            _angle = -_angle_speed;
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
            if (_vz!=0 || _angle!=0)
            {
                _st = 2;
                _animtor.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_vz == 0 && _angle==0)
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
            _timer += Time.deltaTime;
            if (_count==0)
            {
                if (_timer>=0.2f)
                {
                    _timer = 0;
                    _count = 1;
                    _animtor.Play("JumpTop");
                }
            }
            else if (_count == 1)
            {
                if (_timer >= 0.2f)
                {
                    _timer = 0;
                    _count = 2;
                    _animtor.Play("JumpDown");
                }
            }
        }
        else if (_st==5)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _timer =0;
                _st = 1;
                _animtor.Play("Base");
            }
        }
        else if (_st==6)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _timer = 0;
                _st = 0;
                _panda_mesh_renderer.enabled = false;
            }
        }
        else if (_st==7)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _timer = 0;
                _st = 8;
                _animtor.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        if (_st==1||_st==2||_st==4) {
            transform.Translate(0, 0, _vz / 50);
            transform.Rotate(0,_angle/50,0);
        }
    }

    public void DameSet()
    {
        _st = 6;
        _timer = 0;
        _animtor.Play("StrengthDame");
        _collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_st==4||_st==8) && other.gameObject.tag=="Cube")
        {
            _st = 5;
            _timer =0;
            _animtor.Play("JumpArrive");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Monkey" && _st==4)
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
            if (_MonkeyManager._st<=3)
            {
                _st = 7;
                _timer = 0;
                _animtor.Play("JumpAttack");
                _rbody.constraints = RigidbodyConstraints.FreezePosition;

                _MonkeyManager.DameSet();
            }
        }
    }
}
