using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterManager : MonoBehaviour
{
    //�Q�b�g�|�C���g�I�u�W�F�N�g
    private GameObject _GetPoint;

    //�e�L�X�g
    private Text _text;
    //�Q�b�g�|�C���g�e�L�X�g
    private Text _get_point_text;

    //�X�e�[�^�X
    private int _st;
    //�o�[�W����
    public int _ver;
    //�J�E���g
    private int _count;
    //�^�C�}�[
    private float _timer;

    //_st=1-��{�`
    //_st=2-���Z

    void Awake()
    {
        _text = GetComponent<Text>();

        _GetPoint = GameObject.Find("GetPoint");
        _get_point_text = _GetPoint.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (GameManager._get_point>=10) {
                _timer += Time.deltaTime;
                if (_timer >= 2)
                {
                    if (_count == 0) {
                        _count = 1;
                        _timer = 0;

                        if (_ver == 1)
                        {
                            _text.text = "�����^�� " + GameManager._mental.ToString() + " + 1";
                        }
                        else if (_ver == 2)
                        {
                            _text.text = "�ő吳�C�x " + GameManager._sanity_max.ToString() + " + 1";
                        }
                        else if (_ver == 3)
                        {
                            _text.text = "�ő�S���� " + GameManager._heart_max.ToString() + " + 1";
                        }
                    }
                    else if (_count == 1)
                    {
                        _st = 2;
                        _timer = 0;

                        if (_ver == 1)
                        {
                            GameManager._mental += 1;
                            GameManager._get_point -= 10;
                            _text.text = "�����^�� " + GameManager._mental.ToString();
                        }
                        else if (_ver == 2)
                        {
                            GameManager._sanity_max += 1;
                            GameManager._get_point -= 10;
                            _text.text = "�ő吳�C�x " + GameManager._sanity_max.ToString();
                        }
                        else if (_ver == 3)
                        {
                            GameManager._heart_max += 1;
                            GameManager._get_point -= 10;
                            _text.text = "�ő�S���� " + GameManager._heart_max.ToString();
                        }
                        _get_point_text.text = GameManager._get_point.ToString("0000");
                    }
                }
            }
        }
    }

    public void TextSet()
    {
        _st = 1;
        _count = 0;
        _timer = 0;

        if (_ver==1)
        {
            _text.text = "�����^�� "+GameManager._mental.ToString();
        }
        else if (_ver == 2)
        {
            _text.text = "�ő吳�C�x " + GameManager._sanity_max.ToString();
        }
        else if (_ver == 3)
        {
            _text.text = "�ő�S���� " + GameManager._heart_max.ToString();
        }
    }
}
