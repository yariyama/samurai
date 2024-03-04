using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStartManager : MonoBehaviour
{
    //�Q�[�����C���I�u�W�F�N�g
    private GameObject _GameMain;
    //�Q�[�����C���X�N���v�g
    private GameMainManager _GameMainManager;

    //���W
    private Vector2 _position;
    //�e�L�X�g
    private Text _text;

    //�X�e�[�^�X
    private int _st;
    //�^�C�}�[
    private float _timer;

    //_st=1-��{�`
    //_st=2-�X���C�h
    //_st=3-�|����
    //_st=4-�E�F�C�g

    void Awake()
    {
        _position = transform.localPosition;
        _text = GetComponent<Text>();

        _GameMain = GameObject.Find("GameMain");
        _GameMainManager = _GameMain.GetComponent<GameMainManager>();
    }

    void Start()
    {
        _text.enabled = false;
        _st = 4;
        _timer =0;
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=1.5f)
            {
                _timer = 0;
                _st = 3;
            }
        }
        else if (_st==2)
        {
            _position.x -= 30;
            if (_position.x<=0)
            {
                _position.x = 0;
                _st = 1;
            }
            transform.localPosition = _position;
        }
        else if (_st == 3)
        {
            _position.x -= 30;
            if (_position.x <= -800)
            {
                _GameMainManager.BGMStrat();
                this.gameObject.SetActive(false);
            }
            transform.localPosition = _position;
        }
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _timer = 0;
                ActiveSet();
            }
        }
    }

    public void ActiveSet()
    {
        _text.enabled = true;
        _text.text = GameManager._stage.ToString() + "STAGE";
        _st = 2;
        _position.x = 800;
        transform.localPosition = _position;
    }
}
