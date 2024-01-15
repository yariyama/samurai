using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    //�p�x
    private Vector3 _angle;
    //�A�j���[�^�[
    private Animator _animator;
    //���W�b�h�{�f�B
    private Rigidbody _rbody;

    //�����L�[���b�V���I�u�W�F�N�g
    private GameObject _MonkeyMesh;
    //�����L�[���b�V���R���C�_�[
    private CapsuleCollider _monkey_mesh_collider;

    //�����L�[���b�V�������_���[
    private Renderer _monkey_mesh_renderer;
    //�����L�[���b�V���}�e���A��
    private Material _monkey_mesh_material;
    //�����L�[���b�V���J���[
    private Color _monkey_mesh_color;

    //�X�s�[�h
    public float _speed;
    //�X�e�[�^�X
    private int _st;
    //�^�C�}�[
    private float _timer;
    //�����_���l
    private int _ran;
    //����
    private int _dire;
    //��]�X�s�[�h
    public float _a_speed;

    //_st=1-��{�`
    //_st=2-�ړ�
    //_st=3-�^�[��
    //_st=4-�_���[�W

    //_dire=1-�E
    //_dire=2-��

    void Awake()
    {
        _angle = this.transform.localEulerAngles;
        _animator = this.GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody>();

        _MonkeyMesh = transform.Find("MonkeyMesh").gameObject;
        _monkey_mesh_collider = _MonkeyMesh.GetComponent<CapsuleCollider>();
        _monkey_mesh_renderer = _MonkeyMesh.GetComponent<Renderer>();
        _monkey_mesh_material = _monkey_mesh_renderer.material;
        _monkey_mesh_color = _monkey_mesh_material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _angle.y = 270;
        transform.localEulerAngles = _angle;
        _dire = 1;
        _animator.Play("Base");
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _ran = Random.Range(0,10);
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
            transform.Translate(0,0,_speed/50);
        }
        else if (_st==3)
        {
            if (_dire==1)
            {
                transform.Rotate(0,-_a_speed/50,0);
                _angle = transform.localEulerAngles;
                if (_angle.y<=90)
                {
                    _angle.y = 90;
                    transform.localEulerAngles = _angle;
                    _st = 1;
                    _dire = 2;
                    _animator.Play("Base");
                }
            }
            else if (_dire==2)
            {
                transform.Rotate(0, _a_speed / 50, 0);
                _angle = transform.localEulerAngles;
                if (_angle.y >= 270)
                {
                    _angle.y = 270;
                    transform.localEulerAngles = _angle;
                    _st = 1;
                    _dire = 1;
                    _animator.Play("Base");
                }
            }
        }
        else if (_st==4)
        {
            _monkey_mesh_color.a -= 0.01f;
            if (_monkey_mesh_color.a<=0)
            {
                Destroy(this.gameObject);
            }
            _monkey_mesh_material.color = _monkey_mesh_color;
        }
    }

    //�^�[���؂芷��
    public void TurnSet()
    {
        _st = 3;
    }

    //�_���[�W�؂芷��
    public void DameSet()
    {
        _st = 4;
        _timer = 0;
        _animator.Play("Dame");
        _monkey_mesh_collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
    }
}
