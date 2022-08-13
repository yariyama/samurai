using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainManager : MonoBehaviour
{
    //プレイヤーオブジェクト
    private GameObject _Player;
    //プレイヤースクリプト
    private PlayerManager _PlayerManager;
    //ゲットポイントオブジェクト
    private GameObject _GetPoint;

    //ゲットポイントテキスト
    private Text _get_point_text;

    //タイマー
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
