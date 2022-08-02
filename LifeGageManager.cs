using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGageManager : MonoBehaviour
{
    //�܂�I�u�W�F�N�g
    private GameObject _Maru;

    //�C���[�W
    private Image _image;
    //�X�v���C�g
    public Sprite _spt1;
    public Sprite _spt2;

    //�X�e�[�^�X
    private int _st;
    //�o�[�W����
    public int _ver;

    //_st=1-��{�`
    //_st=2-����

    void Awake()
    {
        _Maru = transform.Find("Maru").gameObject;

        _image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _image.sprite = _spt1;

        if (_ver!=3)
        {
            _Maru.SetActive(false);
        }
    }

    public void DeSet(int _no)
    {
        if (_no==1)
        {
            _st = 2;
            _image.sprite = _spt2;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void MaruSet()
    {
        _Maru.SetActive(true);
    }
}
