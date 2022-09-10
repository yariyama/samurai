using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManManager : MonoBehaviour
{
    //�}���I�u�W�F�N�g
    private GameObject _Man;
    //�}���X�N���v�g
    private ManManager _ManManager;
    //�o�b�N�{�^���I�u�W�F�N�g
    private GameObject _BackButton;
    //�o�b�N�{�^���X�N���v�g
    private BackButtonManager _BackButtonManager;

    //�f�����C��UI�I�u�W�F�N�g
    private GameObject _DemoMainUI;
    //�f�����C��UI�X�N���v�g
    private DemoMainUIManager _DemoMainUIManager;
    //�����^���I�u�W�F�N�g
    private GameObject _Mental;
    //�����^���X�N���v�g
    private ParameterManager _MentalManager;
    //�ő吳�C�x�I�u�W�F�N�g
    private GameObject _SanityMax;
    //�ő吳�C�x�X�N���v�g
    private ParameterManager _SanityMaxManager;
    //�ő�S�����I�u�W�F�N�g
    private GameObject _HeartMax;
    //�ő�S�����X�N���v�g
    private ParameterManager _HeartMaxManager;

    //���W
    private Vector2 _position;

    //�X�e�[�^�X
    private int _st;
    //�o�[�W����
    public int _ver;
    //�Z�b�gx���W
    public float _set_x;
    //�Z�b�gy���W
    public float _set_y;
    //�A�E�gx���W
    public float _out_x;
    //�^�C�}�[
    private float _timer;

    //_st=1-��{�`
    //_st=2-�A�E�g�X���C�h
    //_st=3-�A�E�g
    //_st=4-�E�F�C�g
    //_st=5-�C���X���C�h

    void Awake()
    {
        _position = transform.localPosition;

        _BackButton = GameObject.Find("BackButton");
        _BackButtonManager = _BackButton.GetComponent<BackButtonManager>();

        _DemoMainUI = transform.parent.gameObject;
        _DemoMainUIManager = _DemoMainUI.GetComponent<DemoMainUIManager>();

        _Mental = GameObject.Find("Mental");
        _MentalManager = _Mental.GetComponent<ParameterManager>();

        _SanityMax = GameObject.Find("SanityMax");
        _SanityMaxManager = _SanityMax.GetComponent<ParameterManager>();

        _HeartMax = GameObject.Find("HeartMax");
        _HeartMaxManager = _HeartMax.GetComponent<ParameterManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_ver==1)
        {
            _st = 1;
            _position.x = _set_x;

            _BackButton.SetActive(false);
            _Mental.SetActive(false);
            _SanityMax.SetActive(false);
            _HeartMax.SetActive(false);
        }
        else
        {
            _st = 3;
            _position.x = _out_x;
        }
        _position.y = _set_y;
        transform.localPosition = _position;

        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _position.x += 40;
            if (_position.x>=_out_x)
            {
                _st = 4;
                _timer = 0;
            }
            transform.localPosition = _position;
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _st = 3;
                _timer = 0;
                _ManManager.InSet();
            }
        }
        else if (_st==5)
        {
            _position.x -= 40;
            if (_position.x <= _set_x)
            {
                _st = 1;
                _position.x = _set_x;

                if (_DemoMainUIManager._mode_st!=0)
                {
                    _BackButton.SetActive(true);
                    _BackButtonManager.ActiveSet();
                }

                if (_ver==2)
                {
                    _Mental.SetActive(true);
                    _MentalManager.TextSet();
                }
                else if (_ver == 3)
                {
                    _SanityMax.SetActive(true);
                    _SanityMaxManager.TextSet();
                }
                else if (_ver == 4)
                {
                    _HeartMax.SetActive(true);
                    _HeartMaxManager.TextSet();
                }
            }
            transform.localPosition = _position;
        }
    }

    public void OutSet(int _no)
    {
        if (_st==1)
        {
            _st = 2;
            _Man = GameObject.Find("Man"+(_no+1));
            _ManManager = _Man.GetComponent<ManManager>();

            if (_ver == 2)
            {
                _Mental.SetActive(false);
            }
            else if (_ver == 3)
            {
                _SanityMax.SetActive(false);
            }
            else if (_ver == 4)
            {
                _HeartMax.SetActive(false);
            }
        }
    }

    public void InSet()
    {
        if (_st==3)
        {
            _st = 5;
        }
    }
}
