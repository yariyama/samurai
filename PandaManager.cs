using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PandaManager : MonoBehaviour
{
    //���C���J�����I�u�W�F�N�g
    private GameObject _MainCamera;
    //�T���I�u�W�F�N�g
    private GameObject _Monkey;
    //�T���X�N���v�g
    private MonkeyManager _MonkeyManager;
    //�X�R�A�I�u�W�F�N�g
    private GameObject _Score;
    //�Q�[�����C��UI�I�u�W�F�N�g
    private GameObject _GameMainUI;
    //�Q�[�����C��UI�X�N���v�g
    private GameMainUIManager _GameMainUIManager;
    //�`�F�b�N�|�C���g�I�u�W�F�N�g
    private GameObject[] _CheckPoint = new GameObject[3];
    //�Q�[���I�[�o�[�I�u�W�F�N�g
    private GameObject _GameOver;
    //�Q�[���I�[�o�[�X�N���v�g
    private GameOverManager _GameOverManager;
    //�}�X�N�I�u�W�F�N�g
    private GameObject _Mask;
    //�}�X�N�X�N���v�g
    private MaskManager _MaskManager;

    //���W�b�h�{�f�B
    private Rigidbody2D _rbody;
    //�A�j���[�^�[
    private Animator _animator;
    //�X�P�[��
    private Vector2 _scale;
    //���W
    private Vector2 _position;
    //�J�������W
    private Vector3 _camera_position;
    //�R���C�_�[
    private BoxCollider2D _collider;
    //�\��
    private SpriteRenderer _renderer;
    //�F
    private Color _color;
    //�X�R�A�e�L�X�g
    private Text _score_text;
    //�I�[�f�B�I�\�[�X
    private AudioSource _audio;
    //���ʉ�
    public AudioClip _se1;
    public AudioClip _se2;
    public AudioClip _se3;
    public AudioClip _se4;
    public AudioClip _se5;

    //�X�e�[�^�X
    public int _st;
    //�J�E���g
    private int _count;
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
    //���C���J�����x�[�Xy
    private float _camera_yb;


    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�U�����
    //_st=4-�W�����v�O
    //_st=5-�W�����v
    //_st=6-�W�����v��
    //_st=7-�U��
    //_st=8-�U���߂�
    //_st=9-�_���[�W
    //_st=10-��
    //_st=11-��_���[�W
    //_st=12-����
    //_st=13-�����|�[�Y
    //_st=14-�����|�[�Y��
    //_st=15-�t�F�[�h�A�E�g

    void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _scale = transform.localScale;
        _position = transform.position;
        _collider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _color = _renderer.color;
        _audio = GetComponent<AudioSource>();

        _MainCamera = GameObject.Find("Main Camera");
        _camera_position = _MainCamera.transform.position;

        _Score = GameObject.Find("Score");
        _score_text = _Score.GetComponent<Text>();

        _GameMainUI = GameObject.Find("GameMainUI");
        _GameMainUIManager = _GameMainUI.GetComponent<GameMainUIManager>();

        for (int i=1;i<=3;i++)
        {
            _CheckPoint[i - 1] = GameObject.Find("CheckPoint"+i);
        }

        _GameOver = GameObject.Find("GameOver");
        _GameOverManager = _GameOver.GetComponent<GameOverManager>();

        _Mask = GameObject.Find("Mask");
        _MaskManager = _Mask.GetComponent<MaskManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire_x = 1;
        _timer = 0;
        _animator.Play("Base");

        _camera_position.x = _position.x;
        _camera_position.y = _position.y+2;
        _camera_position.z = -10;
        _MainCamera.transform.position = _camera_position;
        _camera_yb = _camera_position.y;

        _GameOver.SetActive(false);
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
                _audio.clip = _se1;
                _audio.Play();
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
        else if (_st==7)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _st = 8;
                _timer =0;
                _animator.Play("AttackAfter");
            }
        }
        else if (_st==9)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _st = 10;
                _count = 0;

                --GameManager._life;
                GameManager._life = Mathf.Ceil(GameManager._life);
                _GameMainUIManager.LifeGageSet();
            }
        }
        else if (_st==10)
        {
            if (_count==0)
            {
                _color.g -= 0.1f;
                _color.b -= 0.1f;
                if (_color.g<=0)
                {
                    _color.g = 0;
                    _color.b = 0;
                    _count = 1;
                }
            }
            else if (_count==1)
            {
                _color.g += 0.1f;
                _color.b += 0.1f;
                if (_color.g >= 1)
                {
                    _color.g = 1;
                    _color.b = 1;
                    _count = 0;
                }
            }

            _color.a -= 0.02f;
            if (_color.a<=0)
            {
                _color.a = 0;
                _st = 0;

                if (GameManager._life > 0)
                {
                    ReturnSet();
                }
                else
                {
                    _GameOver.SetActive(true);
                    _GameOverManager.ActiveSet();
                }
            }
            _renderer.color = _color;
        }
        else if (_st==11)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _timer = 0;

                if (GameManager._life==2||GameManager._life==1||GameManager._life==0)
                {
                    _st = 10;
                    _count = 0;
                    _GameMainUIManager.LifeGageSet();
                }
                else
                {
                    _st = 1;
                    _animator.Play("Base");
                }
            }
        }
        else if (_st==15)
        {
            _color.a -= 0.05f;
            if (_color.a<=0)
            {
                _color.a = 0;
                _st = 0;

                _MaskManager.OutSet();
                this.gameObject.SetActive(false);
            }
            _renderer.color = _color;
        }

        //�ړ�
        if (_vx!=0 && (_st==2||_st==5))
        {
            transform.Translate(_vx/50,0,0);
        }

        //��ʊO���f
        if (_st!=0 && transform.position.y<=-8)
        {
            _st = 0;
            --GameManager._life;
            GameManager._life = Mathf.Ceil(GameManager._life);
            _GameMainUIManager.LifeGageSet();

            if (GameManager._life>0)
            {
                ReturnSet();
            }
            else
            {
                _GameOver.SetActive(true);
                _GameOverManager.ActiveSet();
            }
        }
    }

    void LateUpdate()
    {
        if (_st == 2 || _st == 5)
        {
            _position = transform.position;
            _camera_position.x = _position.x;
            if (_position.y >= 3.5f)
            {
                _camera_position.y = _position.y - 2f;
            }
            else
            {
                _camera_position.y = _camera_yb;
            }
            _MainCamera.transform.position = _camera_position;
        }
    }

    //�U�����f
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_st==5 && _jump_down && collision.gameObject.tag=="Enemy")
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();

            if (_MonkeyManager._st==1 || _MonkeyManager._st==2 || _MonkeyManager._st==3)
            {
                _st = 7;
                _timer = 0;
                _animator.Play("Attack");
                _audio.clip = _se3;
                _audio.Play();

                _MonkeyManager.DameSet();

                GameManager._score += 100;
                _score_text.text = GameManager._score.ToString("0000");
            }
        }
    }

    //���n���f
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Cloud")
        {
            _ground_st = true;

            if ((_st==5 && _jump_down)||_st==8 ||_st==12)
            {
                _st = 6;
                _timer = 0;
                _animator.Play("JumpSet");
                _jump_down = false;
                _audio.clip = _se2;
                _audio.Play();
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

    //�_���[�W�Z�b�g
    public void DameSet()
    {
        _st = 9;
        _timer =0;
        _animator.Play("Death");
        _audio.clip = _se4;
        _audio.Play();

        _collider.enabled = false;
        _rbody.gravityScale = 0;
        //_rbody.velocity = new Vector3(0,0,0);
    }

    //��_���[�W�Z�b�g
    public void SDameSet()
    {
        _st = 11;
        _timer = 0;
        _animator.Play("Dame");

        GameManager._life -= 0.5f;
        _GameMainUIManager.LifeGageSet();
    }

    void JumpDown()
    {
        _jump_down = true;
    }

    //����
    public void ReturnSet()
    {
        _color.r = 1;
        _color.g = 1;
        _color.b = 1;
        _color.a = 1;
        _renderer.color = _color;

        _st = 12;
        _timer = 0;
        _count = 0;
        _ground_st = false;
        _push_st = false;
        _jump_down = true;
        _animator.Play("AttackAfter");
        _collider.enabled = true;
        _rbody.gravityScale = 2;

        for (int i=2;i>=0;i--)
        {
            if (transform.position.x >= _CheckPoint[i].transform.position.x||i==0)
            {
                transform.position = new Vector2(_CheckPoint[i].transform.position.x, _CheckPoint[i].transform.position.y);
                break;
            }
        }
    }

    //�����Z�b�g
    public void WinSet()
    {
        _st = 13;
        _timer = 0;
        _animator.Play("Win");
    }

    //������
    void WinAfter()
    {
        _st = 14;
        _audio.clip = _se5;
        _audio.Play();
    }

    //�t�F�[�h�A�E�g
    public void FadeOut()
    {
        _st = 15;
    }
}
