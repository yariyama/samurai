using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainManager : MonoBehaviour
{
    //音符プレハブ
    public GameObject _Note_pf;
    //音符オブジェクト
    private GameObject _Note;
    //音符スクリプト
    private NoteManager _NoteManager;

    //ステータス
    private int _st;
    //タイマー
    private float _timer;

    //_st=1-基本

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _timer = 0;
                NoteSet();
            }
        }
    }

    //音符セット
    public void NoteSet()
    {
        _Note = Instantiate(_Note_pf) as GameObject;
    }
}
