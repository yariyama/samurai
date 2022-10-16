using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FukidashiManager : MonoBehaviour
{
    //�g�[�N�}�X�N�I�u�W�F�N�g
    private GameObject _TalkMask;
    //�g�[�N���C���X�N���v�g
    private TalkMainManager _TalkMainManager;
    //�����o���I�u�W�F�N�g
    private GameObject _Fukidashi;
    //�����o���X�N���v�g
    private FukidashiManager _FukidashiManager;
    //�e�L�X�g�I�u�W�F�N�g
    private GameObject _Text;
    //�}��1�I�u�W�F�N�g
    private GameObject _Man1;
    //�}��1�X�N���v�g
    private ManManager _Man1Manager;
    //�}��6�I�u�W�F�N�g
    private GameObject _Man6;
    //�}��6�X�N���v�g
    private ManManager _Man6Manager;
    //�}��7�I�u�W�F�N�g
    private GameObject _Man7;
    //�}��7�X�N���v�g
    private ManManager _Man7Manager;
    //�}��8�I�u�W�F�N�g
    private GameObject _Man8;
    //�}��8�X�N���v�g
    private ManManager _Man8Manager;

    //�C���[�W
    private Image _image;
    //�X�v���C�g
    public Sprite _spt1;
    public Sprite _spt2;
    //���W
    private Vector2 _position;
    //�e�L�X�g�e�L�X�g
    private Text _text_text;

    //�X�e�[�^�X
    public int _st;
    //�o�[�W����
    public int _ver;
    //�ʒu
    public int _pos_st;
    //�g�[�N
    private int _talk_st;
    //�A�E�gy�ʒu1
    public float _out_y1;
    //�A�E�gy�ʒu2
    public float _out_y2;
    //�C��y�ʒu1
    public float _in_y1;
    //�C��y�ʒu2
    public float _in_y2;
    //x�ʒu1
    public float _set_x1;
    //x�ʒu2
    public float _set_x2;
    //�^�C�}�[
    private float _timer;
    //����NO
    private int _qes_no;
    //�����_���l
    private int _ran;

    //_st=1-��{�`
    //_st=2-�X���C�h

    void Awake()
    {
        _TalkMask = GameObject.Find("TalkMask");
        _TalkMainManager = _TalkMask.GetComponent<TalkMainManager>();

        _image = GetComponent<Image>();
        _position = transform.localPosition;

        _Text = transform.Find("Text").gameObject;
        _text_text = _Text.GetComponent<Text>();

        _Man1 = GameObject.Find("Man1");
        _Man1Manager = _Man1.GetComponent<ManManager>();
        _Man6 = GameObject.Find("Man6");
        _Man6Manager = _Man6.GetComponent<ManManager>();
        _Man7 = GameObject.Find("Man7");
        _Man7Manager = _Man7.GetComponent<ManManager>();
        _Man8 = GameObject.Find("Man8");
        _Man8Manager = _Man8.GetComponent<ManManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _st = 0;
        _pos_st = 0;
        _talk_st = 0;
        _image.enabled = false;
        _text_text.enabled = false;
        _position.y = _out_y1;
        transform.localPosition = _position;
        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_talk_st==1 && _pos_st==1)
            {
                _timer += Time.deltaTime;
                if (_timer>=1)
                {
                    _timer = 0;
                    _TalkMainManager.FukidashiSet(2,_qes_no);
                }
            }
        }
        else if (_st==2)
        {
            _position.y += 10;
            if (_pos_st==0 && _position.y>=_in_y1)
            {
                _position.y = _in_y1;
                _pos_st = 1;
                _st = 1;
                _timer = 0;

                if (_talk_st==2)
                {
                    _TalkMainManager._st = 1;
                }
            }
            else if (_pos_st == 1 && _position.y >= _in_y2)
            {
                _position.y = _in_y2;
                _pos_st = 2;
                _st = 1;
                _timer = 0;
            }
            else if (_pos_st == 2 && _position.y >= _out_y1)
            {
                _pos_st = 0;
                _st = 0;
                _timer = 0;
                _image.enabled = false;
                _text_text.enabled = false;
            }
            transform.localPosition = _position;
        }
    }

    public void ActiveSet(int _no1, int _no2)
    {
        _image.enabled = true;
        _text_text.enabled = true;

        if (_no1 == 1)
        {
            _talk_st = 1;
            _image.sprite = _spt2;
            _position.x = _set_x1;

            if (_no2 == 1)
            {
                _text_text.text = "���q�ǂ��H";
            }
            else if (_no2==2)
            {
                _text_text.text = "������������";
            }
            _qes_no = _no2;

            _TalkMainManager._st = 2;
        }
        else if (_no1 == 2)
        {
            _talk_st = 2;
            _image.sprite = _spt1;
            _position.x = _set_x2;

            if (_no2 == 1)
            {
                _text_text.text = "�܂��܂�";
                _Man6Manager.ActiveSet(3);
                _Man7Manager.ActiveSet(3);
                _Man1Manager.ActiveSet(1);
            }
            else if (_no2==2)
            {
                _ran = Random.Range(0,5);
                if (_ran==1)
                {
                    _text_text.text = "�������͂�����";
                    _Man1Manager.ActiveSet(3);
                    _Man6Manager.ActiveSet(3);
                    _Man7Manager.ActiveSet(1);
                }
                else
                {
                    _text_text.text = "���肪�Ƃ��I";
                    _Man1Manager.ActiveSet(3);
                    _Man7Manager.ActiveSet(3);
                    _Man6Manager.ActiveSet(1);
                }
            }
        }
        _position.y = _out_y1;
        transform.localPosition = _position;

        _pos_st = 0;

        _st = 2;

        if (_ver == 1)
        {
            _Fukidashi = GameObject.Find("Fukidashi3");
        }
        else
        {
            _Fukidashi = GameObject.Find("Fukidashi" + (_ver - 1));
        }
        _FukidashiManager = _Fukidashi.GetComponent<FukidashiManager>();

        if (_FukidashiManager._st == 1 && _FukidashiManager._pos_st == 1)
        {
            _FukidashiManager.SlideSet();
        }
    }

    public void SlideSet()
    {
        _st = 2;

        if (_ver == 1)
        {
            _Fukidashi = GameObject.Find("Fukidashi3");
        }
        else
        {
            _Fukidashi = GameObject.Find("Fukidashi" + (_ver - 1));
        }
        _FukidashiManager = _Fukidashi.GetComponent<FukidashiManager>();

        if (_FukidashiManager._st==1 && _FukidashiManager._pos_st==2)
        {
            _FukidashiManager.SlideSet();
        }
    }
}
