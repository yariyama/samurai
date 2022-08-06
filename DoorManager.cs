using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    //プレイヤーオブジェクト
    private GameObject _Player;
    //プレイヤースクリプト
    private PlayerManager _PlayerManager;

    //ステータス
    private int _st;
    //タイマー
    private float _timer;

    //_st=1-基本形
    //_st=2-反応

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                SceneManager.LoadScene("SubScene");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (_st==1 && other.gameObject.name=="Player")
        {
            _st = 2;
            _timer = 0;

            _Player = other.gameObject;
            _PlayerManager = _Player.GetComponent<PlayerManager>();
        }
    }
}
