using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonSoldier_demoManager : MonoBehaviour
{
    //プレイヤー
    private GameObject _Player;
    //プレイヤースクリプト
    private PlayerManager _PlayerManager;

    //座標
    private Vector3 _position;

    //ステータス
    private int _st;
    //カウント
    private int _count;

    //_st=1-基本形
    //_st=2-ショット

    void Awake()
    {
        _Player = transform.parent.gameObject;
        _PlayerManager = _Player.GetComponent<PlayerManager>();

        _position = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void FixedUpdate()
    {
        if (_st == 2)
        {
            if (_count == 0)
            {
                _position.z -= 0.02f;
                if (_position.z <= -0.06)
                {
                    _position.z = -0.06f;
                    _count = 1;
                }
            }
            else
            {
                _position.z += 0.02f;
                if (_position.z >= 0)
                {
                    _position.z = 0;
                    _st = 1;
                    _count = 0;

                    _PlayerManager.BaseSet();
                }
            }

            transform.localPosition = _position;
        }
    }

    public void ShootSet()
    {
        _st = 2;
    }
}
