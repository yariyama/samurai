using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //�p���_���b�V���I�u�W�F�N�g
    private GameObject _PandaMesh;
    //�����L�[�I�u�W�F�N�g
    private GameObject _Monkey;
    //�����L�[�X�N���v�g
    private MonkeyManager _MonkeyManager;
    //���C���J�����I�u�W�F�N�g
    private GameObject _MainCamera;
    //�|�C���g�I�u�W�F�N�g
    private GameObject[] _Point = new GameObject[3];
    //�t�F�C�X�I�u�W�F�N�g
    private GameObject[] _Face = new GameObject[2];
    //�Q�[���I�[�o�[�I�u�W�F�N�g
    private GameObject _GameOver;
    //�Q�[���I�[�o�[�X�N���v�g
    private GameOverManager _GameOverManager;

    //���W�b�h�{�f�B
    private Rigidbody _rbody;
    //�A�j���[�^�[
    private Animator _animator;
    //�R���C�_�[
    private CapsuleCollider _collider;

    //�p���_���b�V�������_���[
    private Renderer _panda_mesh_renderer;
    //�p���_���b�V���}�e���A��
    private Material _panda_mesh_material;
    //�p���_���b�V���J���[
    private Color _panda_mesh_color;


    //�X�e�[�^�X
    public int _st;
    //�J�E���g
    private int _count;
    //�^�C�}�[
    private float _timer;
    //�ړ���z
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
    //�ڒn
    private bool _ground_st;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�W�����v�Z�b�g
    //_st=4-�W�����v
    //_st=5-�W�����v���n
    //_st=6-�_���[�W
    //_st=7-�U��
    //_st=8-�U����

    void Awake()
    {
        _rbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();

        _PandaMesh = transform.Find("PandaMesh").gameObject;
        _panda_mesh_renderer = _PandaMesh.GetComponent<Renderer>();
        _panda_mesh_material = _panda_mesh_renderer.material;
        _panda_mesh_color = _panda_mesh_material.color;

        _MainCamera = transform.Find("Main Camera").gameObject;

        for (int i=0;i<3;i++)
        {
            _Point[i] = GameObject.Find("Point"+(i+1));
        }

        for (int i = 0; i < 2; i++)
        {
            _Face[i] = GameObject.Find("Face" + (i + 1));
        }

        _GameOver = GameObject.Find("GameOver");
        _GameOverManager = _GameOver.GetComponent<GameOverManager>();
        _GameOver.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _count = 0;
        _timer = 0;
        _animator.Play("Base");
        _push_st = false;
        _ground_st=true;

        _MainCamera.transform.localPosition = new Vector3(0,3,-5);
        _MainCamera.transform.localEulerAngles = new Vector3(20,0,0);
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

        if (Input.GetKey("space") && _st!=4)
        {
            if (_push_st == false && _ground_st==true)
            {
                _push_st = true;
                JumpSet();
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
                _st = 4;
                _timer = 0;
                _count = 0;
                _rbody.AddForce(new Vector3(0, _jump_p, 0), ForceMode.Impulse);
                _animator.Play("JumpUp");
            }
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_count==0 && _timer>=0.3f)
            {
                _count = 1;
                _timer = 0;
                _animator.Play("JumpTop");
            }
            else if (_count==1 && _timer>=0.2f)
            {
                _count = 2;
                _timer =0;
                _animator.Play("JumpDown");
            }
        }
        else if (_st==5)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _st = 1;
                _timer =0;
                _animator.Play("Base");
            }
        }
        else if (_st==6)
        {
            _panda_mesh_color.a -= 0.01f;
            if (_panda_mesh_color.a<=0)
            {
                _panda_mesh_color.a = 0;
                _panda_mesh_renderer.enabled = false;
                //this.gameObject.SetActive(false);

                if (GameManager._zan_c>0)
                {
                    --GameManager._zan_c;
                    _Face[GameManager._zan_c].SetActive(false);
                    FallSet();
                }
                else
                {
                    _GameOver.SetActive(true);
                    _GameOverManager.ActiveSet();
                    this.gameObject.SetActive(false);
                }
            }
            _panda_mesh_material.color = _panda_mesh_color;
        }
        else if (_st==7)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _timer =0;
                _st=8;
                _animator.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        if (_st==2||_st==4) {
            if (_vz != 0) {
                transform.Translate(0, 0, _vz / 50);
            }
            if (_angle != 0)
            {
                transform.Rotate(0, _angle / 50, 0);
            }
        }

        if (_MainCamera.transform.parent && !_ground_st && transform.position.y<=-6)
        {
            _MainCamera.transform.parent = null;
        }
        else if (!_MainCamera.transform.parent && transform.position.y <= -10)
        {
            if (GameManager._zan_c > 0)
            {
                --GameManager._zan_c;
                _Face[GameManager._zan_c].SetActive(false);
                FallSet();
            }
            else
            {
                _GameOver.SetActive(true);
                _GameOverManager.ActiveSet();
                this.gameObject.SetActive(false);
            }
        }
    }

    private void JumpSet()
    {
        _st = 3;
        _timer = 0;
        _animator.Play("JumpSet");
    }

    public void DameSet()
    {
        _st = 6;
        _animator.Play("StrengthDame");
        _collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
    }

    //�����Z�b�g
    public void FallSet()
    {
        _MainCamera.transform.parent = this.gameObject.transform;
        _MainCamera.transform.localPosition = new Vector3(0, 3, -5);
        _MainCamera.transform.localEulerAngles = new Vector3(20, 0, 0);

        _collider.enabled = true;
       
        _panda_mesh_renderer.enabled = true;
        _panda_mesh_color.a = 1;
        _panda_mesh_material.color = _panda_mesh_color;

        for (int i=2;i>=0;i--)
        {
            if (transform.position.z>=_Point[i].transform.position.z||i==0)
            {
                transform.position = _Point[i].transform.position;
                break;
            }
        }

        _st = 8;
        _animator.Play("JumpDown");
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
                _animator.Play("JumpAttack");
                _rbody.constraints = RigidbodyConstraints.FreezePosition;

                _MonkeyManager.DameSet();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_st==4||_st==8) && other.gameObject.tag=="Cloud")
        {
            _st = 5;
            _timer = 0;
            _animator.Play("JumpArrive");
            _ground_st = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="Cloud")
        {
            _ground_st = false;
        }
    }
}
