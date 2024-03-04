using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : MonoBehaviour
{
    //�Q�[�����C���I�u�W�F�N�g
    private GameObject _GameMain;
    //�Q�[�����C���X�N���v�g
    private GameMainManager _GameMainManager;

    //���W
    private Vector2 _position;
    //�p�x
    private Vector3 _angle;

    //�X�e�[�^�X
    private int _st;

    //_st=1-��{�`
    //_st=2-�X���C�h
    //_st=3-��]

    void Awake()
    {
        _position = transform.localPosition;
        _angle = transform.localEulerAngles;

        _GameMain = GameObject.Find("GameMain");
        _GameMainManager = _GameMain.GetComponent<GameMainManager>();
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _position.x -= 20;
            if (_position.x<=0)
            {
                _position.x = 0;
                _st = 1;

                _GameMainManager.BGMStop();
            }
            transform.localPosition = _position;
        }
        else if (_st==3)
        {
            _angle.z += 5;
            if (_angle.z>=90)
            {
                _angle.z = 90;
                _st = 1;
            }
            transform.localEulerAngles = _angle;
        }
    }

    public void ActiveSet()
    {
        _st = 2;
        _position.x = 800;
        transform.localPosition = _position;
        //_st = 3;
    }
}
