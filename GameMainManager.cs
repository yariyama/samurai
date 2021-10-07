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
    //音符ステータス
    public bool[] _note_st = new bool[30];
    //繰り返し用
    private int i;
    //タイマー
    private float _timer;

    //_st=1-基本

    // Start is called before the first frame update
    void Start()
    {
        for (i=0;i<30;i++)
        {
            _note_st[i] = false;
        }

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

        for (i=0;i<30;i++)
        {
            if (_note_st[i]==false)
            {
                _note_st[i] = true;
                _Note.name = "Note" + i;
                _NoteManager = _Note.GetComponent<NoteManager>();
                _NoteManager._ver = i;

                break;
            }
        }
    }
}
