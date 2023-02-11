using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire = 1;
        _timer =0;
        _animator.Play("Base");
        _angle.y = 270;
        transform.localEulerAngles = _angle;
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
                    _timer = 0;
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
            else if (_dire == 2 && _angle.y <= 270 && _angle.y >= 260)
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
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void TurnSet()
    {
        _st = 3;
    }

    public void DameSet()
    {
        _st = 4;
        _timer = 0;
        _animator.Play("Dame");
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
        _collider.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name=="Panda" && _PandaManager._st>=1 && _PandaManager._st<=3)
        {
            _PandaManager.DameSet();
        }
    }
}
