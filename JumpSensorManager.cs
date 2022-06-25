using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSensorManager : MonoBehaviour
{
    //プレイヤーオブジェクト
    private GameObject _Player;
    //プレイヤースクリプト
    private PlayerManager _PlayerManager;

    //ステータス
    public int _st;

    void Awake()
    {
        _Player = transform.parent.gameObject;
        _PlayerManager = _Player.GetComponent<PlayerManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (_st==1)
        {
            _PlayerManager.UpSet();
            _st = 0;
        }
    }
}
