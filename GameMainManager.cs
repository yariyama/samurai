using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainManager : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g
    private GameObject _Player;
    //�v���C���[�X�N���v�g
    private PlayerManager _PlayerManager;
    //�Q�b�g�|�C���g�I�u�W�F�N�g
    private GameObject _GetPoint;

    //�Q�b�g�|�C���g�e�L�X�g
    private Text _get_point_text;

    //�^�C�}�[
    private float _timer;


    void Awake()
    {
        _Player = GameObject.Find("Player");
        _PlayerManager = _Player.GetComponent<PlayerManager>();

        _GetPoint = GameObject.Find("GetPoint");
        _get_point_text = _GetPoint.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_PlayerManager._st>=1 && _PlayerManager._st<=5)
        {
            _timer += Time.deltaTime;
            if (_timer>=3)
            {
                _timer = 0;
                GameManager._get_point += 1;
                _get_point_text.text = GameManager._get_point.ToString("0000");
            }
        }
    }
}
