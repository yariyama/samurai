using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMTitleManager : MonoBehaviour
{
    //�e�L�X�g
    private Text _text;

    //�X�e�[�^�X
    private int _st;
    //�^�C�v
    private int _tp;
    //�^�C�}�[
    private float _timer;

    //_st=1-��{�`

    void Awake()
    {
        _text = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        if (_st == 1)
        {
            _timer += Time.deltaTime;
            if (_timer >= 2)
            {
                _timer = 0;
                _st = 0;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void ActiveSet(int _no)
    {
        _st = 1;
        _tp = _no;

        if (_tp == 1)
        {
            _text.text = "��������̃}�[�`��";
        }
        else if (_tp == 2)
        {
            _text.text = "�D�ԃ|�b�|��";
        }
    }
}
