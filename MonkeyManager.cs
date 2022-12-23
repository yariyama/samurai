using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonkeyManager : MonoBehaviour
{
    //�p���_�I�u�W�F�N�g
    private GameObject _Panda;
    //�p���_�X�N���v�g
    private PandaManager _PandaManager;

    //�A�j���[�^�[
    private Animator _animator;
    //�p�x
    private Vector3 _angle;
    //���W�b�h�{�f�B
    private Rigidbody _rbody;
    //�R���C�_�[
    private BoxCollider _collider;

    //�����L�[���b�V���I�u�W�F�N�g
    private GameObject _MonkeyMesh;
    //�����L�[���b�V�������_���[
    private Renderer _monkey_mesh_renderer;
    //�����L�[���b�V���}�e���A��
    private Material _monkey_mesh_material;
    //�����L�[���b�V���J���[
    private Color _monkey_mash_color;

    //�X�R�A�I�u�W�F�N�g
    private GameObject _Score;
    //�X�R�A�e�L�X�g
    private Text _score_text;

    //�X�e�[�^�X
    public int _st;
    //�ړ��X�s�[�h
    public float _w_speed;
    //��]�X�s�[�h
    public float _a_speed;
    //����
    private int _dire;
    //�^�C�}�[
    private float _timer;
    //�����_���l
    private int _ran;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�U�����
    //_st=4-�_���[�W

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _angle = transform.localEulerAngles;
        _rbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();

        _Panda = GameObject.Find("Panda");
        _PandaManager = _Panda.GetComponent<PandaManager>();

        _MonkeyMesh = transform.Find("MonkeyMesh").gameObject;
        _monkey_mesh_renderer = _MonkeyMesh.GetComponent<Renderer>();
        _monkey_mesh_material = _monkey_mesh_renderer.material;
        _monkey_mash_color = _monkey_mesh_material.color;

        _Score = GameObject.Find("Score");
        _score_text = _Score.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire = 1;
        _timer = 0;
        _angle.y = 270;
        transform.localEulerAngles = _angle;
        _animator.Play("Base");
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _ran = Random.Range(0,5);
                if (_ran==1)
                {
                    _timer =0;
                    _st = 2;
                    _animator.Play("Walk");
                }
            }
        }
        else if (_st==2)
        {
            transform.Translate(0,0,_w_speed/50);
        }
        else if (_st==3)
        {
            transform.Rotate(0,-_a_speed/50,0);
            _angle = transform.localEulerAngles;
            if (_angle.y>=360)
            {
                _angle.y -= 360;
            }
            if (_dire==1 && _angle.y<=90)
            {
                _st = 1;
                _angle.y = 90;
                _dire = 2;
                _animator.Play("Base");
                transform.localEulerAngles = _angle;
            }
            else if (_dire==2 && _angle.y<=270 && _angle.y>=260)
            {
                _st = 1;
                _angle.y = 270;
                _dire = 1;
                _animator.Play("Base");
                transform.localEulerAngles = _angle;
            }
        }
        else if (_st==4)
        {
            _monkey_mash_color.a -= 0.01f;
            if (_monkey_mash_color.a<=0)
            {
                _monkey_mash_color.a = 0;
                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);
            }
            _monkey_mesh_material.color = _monkey_mash_color;
        }
    }

    public void TurnSet()
    {
        _st = 3;
    }

    public void DameSet()
    {
        _st = 4;
        _animator.Play("Dame");
        _collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;

        GameManager._score += 10;
        _score_text.text = GameManager._score.ToString("0000");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name=="Panda" && (_PandaManager._st>=1 && _PandaManager._st<=3))
        {
            _PandaManager.DamSet();
        }
    }
}
