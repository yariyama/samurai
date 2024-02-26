using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //�A�j���[�^�[
    private Animator _animtor;
    //���W�b�h�{�f�B
    private Rigidbody _rbody;

    //�p���_���b�V���I�u�W�F�N�g
    private GameObject _PandaMesh;
    //�p���_���b�V���R���C�_�[
    private CapsuleCollider _panda_mesh_collider;

    //�p���_���b�V�������_���[
    private Renderer _panda_mesh_renderer;
    //�p���_���b�V���}�e���A��
    private Material _panda_mesh_material;
    //�p���_���b�V���J���[
    private Color _panda_mesh_color;

    //�p�x
    private Vector3 _angle2;

    //���C���J�����I�u�W�F�N�g
    private GameObject _MainCamera;

    //�t�F�C�X�I�u�W�F�N�g
    private GameObject[] _Face = new GameObject[2];

    //�|�C���g�I�u�W�F�N�g
    private GameObject[] _Point = new GameObject[4];

    //�Q�[���I�[�o�[�I�u�W�F�N�g
    private GameObject _GameOver;
    //�Q�[���I�[�o�[�X�N���v�g
    private GameOverManager _GameOverManager;

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

    //�����L�[�I�u�W�F�N�g
    private GameObject _Monkey;
    //�����L�[�X�N���v�g
    private MonkeyManager _MonkeyManager;

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
    //_st=8-�_���[�W
    //_st=9-�����_���[�W
    //_st=10-�N���A
    //_st=11-�X�^�[�g

    void Awake()
    {
        //��`
        _animtor = this.GetComponent<Animator>();
        _rbody = this.GetComponent<Rigidbody>();
        _angle2 = transform.localEulerAngles;

        _PandaMesh = transform.Find("PandaMesh").gameObject;
        _panda_mesh_collider = _PandaMesh.GetComponent<CapsuleCollider>();
        _panda_mesh_renderer = _PandaMesh.GetComponent<Renderer>();
        _panda_mesh_material = _panda_mesh_renderer.material;
        _panda_mesh_color = _panda_mesh_material.color;

        _MainCamera = GameObject.Find("Main Camera");

        for (int i=0; i<2; i++)
        {
            _Face[i] = GameObject.Find("Face"+(i+1));
        }

        for (int i = 0; i < 4; i++)
        {
            _Point[i] = GameObject.Find("Point" + (i + 1));
        }

        _GameOver = GameObject.Find("GameOver");
        _GameOverManager = _GameOver.GetComponent<GameOverManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 11;
        _count = 0;
        _ground_st = true;
        _animtor.Play("Base");

        _angle2.y = 180;
        transform.localEulerAngles = _angle2;

        _GameOver.SetActive(false);
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
        //�W�����v�O
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
        //�W�����v
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
        else if (_st==8)
        {
            _panda_mesh_color.a -= 0.01f;
            if (_panda_mesh_color.a<=0)
            {
                _panda_mesh_color.a = 0;
                if (GameManager._zan_c>0)
                {
                    --GameManager._zan_c;
                    _Face[GameManager._zan_c].SetActive(false);
                    FallSet();
                }
                else
                {
                    _MainCamera.transform.parent = null;
                    _GameOver.SetActive(true);
                    _GameOverManager.ActiveSet();
                    this.gameObject.SetActive(false);
                }
            }
            _panda_mesh_material.color = _panda_mesh_color;
        }
        else if (_st==9)
        {
            if (transform.position.y<=-10)
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
        else if (_st==11)
        {
            if (_count==0)
            {
                _timer += Time.deltaTime;
                if (_timer>=1)
                {
                    _timer =0;
                    _count = 1;
                    _animtor.Play("StartSet");
                }
            }
            else if (_count==2)
            {
                _timer += Time.deltaTime;
                if (_timer>=1)
                {
                    _timer =0;
                    _count = 3;
                    _animtor.Play("StartAfter");
                }
            }
            else if (_count==4)
            {
                _angle2.y -= 10;
                if (_angle2.y<=0)
                {
                    _angle2.y = 0;
                    _count = 5;
                    _animtor.Play("TurnAfter");
                }
                transform.localEulerAngles = _angle2;
            }
        }

        if (_st==2 || _st==4)
        {
            this.transform.Translate(0, 0, _vz / 50);
            this.transform.Rotate(0,_angle/50,0);
        }

        if (_st!=9 && transform.position.y<=-1)
        {
            _st = 9;
            _animtor.Play("FallDame");
            _MainCamera.transform.parent = null;
        }
    }

    public void DameSet()
    {
        _st = 8;
        _animtor.Play("StrengthDame");
        _panda_mesh_collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
    }

    private void FallSet()
    {
        _MainCamera.transform.parent = this.gameObject.transform;
        _MainCamera.transform.localPosition = new Vector3(0,2,-3);
        _MainCamera.transform.localEulerAngles = new Vector3(10,0,0);

        _rbody.constraints = RigidbodyConstraints.FreezeRotation;
        _panda_mesh_collider.enabled = true;

        _panda_mesh_color.a = 1;
        _panda_mesh_material.color = _panda_mesh_color;

        for (int i=3; i>=0; i--)
        {
            if (transform.position.z>=_Point[i].transform.position.z || i==0)
            {
                transform.position = _Point[i].transform.position;
                _st = 7;
                _animtor.Play("JumpDown");
                break;
            }
        }
    }

    public void ClearSet()
    {
        _st = 10;
        _animtor.Play("Base");
    }

    public void StartSet(int _no)
    {
        if (_no==1)
        {
            _count = 2;
            _animtor.Play("WaveHand");
        }
        else if (_no==2)
        {
            _count = 4;
            _animtor.Play("TurnSet");
        }
        else if (_no==3)
        {
            _st = 1;
            _animtor.Play("Base");

            _MainCamera.transform.parent = this.transform;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Monkey" && _st==4)
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();

            _st = 6;
            _timer = 0;
            _animtor.Play("JumpAttack");
            _rbody.constraints = RigidbodyConstraints.FreezePosition;

            _MonkeyManager.DameSet();
        }
        else if ((collision.gameObject.tag == "Cube"||collision.gameObject.tag == "Cloud") && (_st == 4||_st==7))
        {
            _st = 5;
            _timer = 0;
            _animtor.Play("JumpArrive");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Cube"|| other.gameObject.tag == "Cloud")
        {
            _ground_st = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cube" || other.gameObject.tag == "Cloud")
        {
            _ground_st = false;
        }
    }
}
