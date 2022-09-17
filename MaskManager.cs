using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskManager : MonoBehaviour
{
    //���{�^���I�u�W�F�N�g
    private GameObject _DateButton;
    //���{�^���X�N���v�g
    private DateButtonManager _DateButtonManager;
    //�����X�I�u�W�F�N�g
    private GameObject _DateMes;

    //�C���[�W
    private Image _image;
    //�J���[
    private Color _color;
    //�����X�e�L�X�g
    private Text _date_mes_text;

    //�X�e�[�^�X
    private int _st;
    //�^�C�}�[
    private float _timer;

    //_st=1-��{�`
    //_st=2-�t�F�[�h�A�E�g
    //_st=3-�t�F�[�h�C��


    void Awake()
    {
        _image = GetComponent<Image>();
        _color = _image.color;

        _DateButton = GameObject.Find("DateButton");
        _DateButtonManager = _DateButton.GetComponent<DateButtonManager>();

        _DateMes = transform.Find("DateMes").gameObject;
        _date_mes_text = _DateMes.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 0;
        _DateMes.SetActive(false);
        _image.enabled = false;
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=2)
            {
                _timer = 0;
                _st = 3;
                _DateMes.SetActive(false);
            }
        }
        else if (_st==2)
        {
            _color.a += 0.02f;
            if (_color.a>=1)
            {
                _color.a = 1;
                _st = 1;
                _timer =0;

                ++GameManager._date;
                _DateButtonManager.DateSet();

                _DateMes.SetActive(true);
                _date_mes_text.text = GameManager._date.ToString() + "���o��";
            }
            _image.color = _color;
        }
        else if (_st==3)
        {
            _color.a -= 0.02f;
            if (_color.a <= 0)
            {
                _color.a = 0;
                _st = 0;
                _timer = 0;

                _image.enabled = false;
            }
            _image.color = _color;
        }
    }

    public void OutSet()
    {
        _st = 2;
        _image.enabled = true;
        _color.a = 0;
        _image.color = _color;
    }
}
