using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateButtonManager : MonoBehaviour
{
    //�e�L�X�g�I�u�W�F�N�g
    private GameObject _Text;
    //�}�X�N�I�u�W�F�N�g
    private GameObject _Mask;
    //�}�X�N�X�N���v�g
    private MaskManager _MaskManager;
    //�g�[�N�}�X�N�I�u�W�F�N�g
    private GameObject _TalkMask;
    //�g�[�N���C���X�N���v�g
    private TalkMainManager _TalkMainManager;

    //�X�P�[��
    private Vector2 _scale;
    //�e�L�X�g�e�L�X�g
    private Text _text_text;

    //�X�e�[�^�X
    private int _st;
    //�^�C�}�[
    private float _timer;

    //_st=1-��{�`
    //_st=2-�v�b�V��

    void Awake()
    {
        _scale = transform.localScale;

        _Text = transform.Find("Text").gameObject;
        _text_text = _Text.GetComponent<Text>();

        _Mask = GameObject.Find("Mask");
        _MaskManager = _Mask.GetComponent<MaskManager>();

        _TalkMask = GameObject.Find("TalkMask");
        _TalkMainManager = _TalkMask.GetComponent<TalkMainManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _timer = 0;
        DateSet();
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer = 0;
                _st = 1;
                _scale.x = 1;
                _scale.y = 1;
                transform.localScale = _scale;

                _MaskManager.OutSet();
            }
        }
    }

    public void ButtonPush()
    {
        if (_st==1 && _TalkMainManager._st==0)
        {
            _st = 2;
            _timer =0;
            _scale.x = 0.9f;
            _scale.y = 0.9f;
            transform.localScale = _scale;
        }
    }

    public void DateSet()
    {
        _text_text.text = GameManager._date.ToString()+"���o��";
    }
}
