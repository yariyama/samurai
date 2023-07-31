using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //�����L�[�I�u�W�F�N�g
    private GameObject _Monkey;
    //�����L�[�X�N���v�g
    private MonkeyManager _MonkeyManager;

    //�p���_���b�V���I�u�W�F�N�g
    private GameObject _PandaMesh;
    //�p���_���b�V�������_���[
    private Renderer _panda_mesh_renderer;
    //�p���_���b�V���}�e���A��
    private Material _panda_mesh_material;
    //�p���_���b�V���J���[
    private Color _panda_mesh_color;

    //���C���J�����I�u�W�F�N�g
    private GameObject _MainCamera;

    //�Q�[���I�[�o�[�I�u�W�F�N�g
    private GameObject _GameOver;
    //�Q�[���I�[�o�[�X�N���v�g
    private GameOverManager _GameOverManager;

    //�t�F�C�X�I�u�W�F�N�g
    private GameObject[] _Face=new GameObject[2];

    //�|�C���g�I�u�W�F�N�g
    private GameObject[] _Point = new GameObject[3];

    //�A�j���[�^�[
    private Animator _animator;
    //���W�b�h�{�f�B
    private Rigidbody _rbody;
    //�R���C�_�[
    private CapsuleCollider _collider;
    //�I�[�f�B�I�\�[�X
    private AudioSource _audio;
    //�W�����v���ʉ�
    public AudioClip _jump_se;
    //���n��
    public AudioClip _land_se;
    //�U����
    public AudioClip _attack_se;
    //�_���[�W��
    public AudioClip _dame_se;
    //�N���A�[��
    public AudioClip _clear_se;

    //�X�e�[�^�X
    public int _st;
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
    //_st=6-�U��
    //_st=7-�U����
    //_st=8-�_���[�W

    void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _rbody = this.GetComponent<Rigidbody>();
        _collider = this.GetComponent<CapsuleCollider>();
        _audio = GetComponent<AudioSource>();

        _PandaMesh = transform.Find("PandaMesh").gameObject;
        _panda_mesh_renderer = _PandaMesh.GetComponent<Renderer>();
        _panda_mesh_material = _panda_mesh_renderer.material;
        _panda_mesh_color = _panda_mesh_material.color;

        _MainCamera = transform.Find("Main Camera").gameObject;

        _GameOver = GameObject.Find("GameOver");
        _GameOverManager = _GameOver.GetComponent<GameOverManager>();

        for (int i=0;i<2;i++)
        {
            _Face[i] = GameObject.Find("Face"+(i+1));
        }

        for (int i = 0; i < 3; i++)
        {
            _Point[i] = GameObject.Find("Point" + (i + 1));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _count = 0;
        _timer = 0;
        _animator.Play("Base");

        _GameOver.SetActive(false);
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
        if (_st == 1)
        {
            if (_vz != 0 || _angle != 0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st == 2)
        {
            if (_vz == 0 && _angle == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st == 3)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.2f)
            {
                _timer = 0;
                _st = 4;
                _count = 0;
                _rbody.AddForce(new Vector3(0, _jump_p, 0), ForceMode.Impulse);
                _animator.Play("JumpUp");
                _audio.clip = _jump_se;
                _audio.Play();
            }
        }
        else if (_st == 4)
        {
            _timer += Time.deltaTime;
            if (_count == 0)
            {
                if (_timer >= 0.3f)
                {
                    _timer = 0;
                    _count = 1;
                    _animator.Play("JumpTop");
                }
            }
            else if (_count == 1)
            {
                if (_timer >= 0.2f)
                {
                    _timer = 0;
                    _count = 2;
                    _animator.Play("JumpDown");
                }
            }
        }
        else if (_st == 5)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.3f)
            {
                _timer = 0;
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st == 6)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                _timer = 0;
                _st = 7;
                _animator.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
        else if (_st==8)
        {
            _panda_mesh_color.a -= 0.01f;
            if (_panda_mesh_color.a <= 0)
            {
                _panda_mesh_color.a = 0;
                _MainCamera.transform.parent = null;

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

        if (_MainCamera.transform.parent && transform.position.y<=-6)
        {
            _MainCamera.transform.parent = null;
        }
        else if (!_MainCamera.transform.parent && transform.position.y <= -5)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Cloud" && (_st==4||_st==7))
        {
            _st = 5;
            _count = 0;
            _timer = 0;
            _animator.Play("JumpArrive");
            _rbody.drag = 0;
            _audio.clip = _land_se;
            _audio.Play();
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
                _st = 6;
                _timer =0;
                _animator.Play("JumpAttack");
                _rbody.constraints = RigidbodyConstraints.FreezePosition;
                _MonkeyManager.DameSet();
                _audio.clip = _attack_se;
                _audio.Play();
            }
        }
    }

    public void DameSet()
    {
        _st = 8;
        _timer = 0;
        _animator.Play("StrengthDame");
        _collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
        _audio.clip = _dame_se;
        _audio.Play();
    }

    public void FallSet()
    {
        _MainCamera.transform.parent = this.gameObject.transform;
        _MainCamera.transform.localPosition = new Vector3(0,3,-3);
        _MainCamera.transform.localEulerAngles = new Vector3(20,0,0);

        _rbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rbody.drag = 1;
        _collider.enabled = true;

        _panda_mesh_color.a = 1;
        _panda_mesh_material.color = _panda_mesh_color;

        for (int i=2;i>=0;i--)
        {
            if (transform.position.z>=_Point[i].transform.position.z || i==0)
            {
                transform.position = _Point[i].transform.position;
                break;
            }
        }

        _st = 7;
        _animator.Play("JumpDown");
    }
}
