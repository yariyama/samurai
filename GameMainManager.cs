﻿using System.Collections;
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

    //音符セットタイム
    private float[] _note_set_t = new float[100];
    //音符セット位置
    private int[] _note_set_p = new int[100];

    //音符セットタイム（音楽1）
    public float[] _note_set_t1 = new float[100];
    //音符セット位置（音楽1）
    public int[] _note_set_p1 = new int[100];

    //セットカウント
    private int _set_count;

    //繰り返し用
    private int i;
    //タイマー
    private float _timer;
    //セーフエリアまでの移動時間
    public float _safe_time=2.2f;
    //ベースタイム
    private float _b_time;


    //_st=1-基本

    // Start is called before the first frame update
    void Start()
    {
        for (i=0;i<30;i++)
        {
            _note_st[i] = false;
        }

        _set_count = 0;

        _st = 1;
        _timer = 0;
        ActiveSet(1);
    }

    //アクティブ化
    public void ActiveSet(int _no)
    {
        if (_no==1)
        {
            _note_set_t = _note_set_t1;
            _note_set_p = _note_set_p1;
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_note_set_t[_set_count]!=0) {
               _b_time =_note_set_t[_set_count] - _safe_time;
                if (_timer >= _b_time)
                {
                    ++_set_count;
                    NoteSet(_set_count);
                }
            }
        }
    }

    //音符セット
    public void NoteSet(int _no)
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
                _NoteManager.ActiveSet(_note_set_p[_no-1]);
                break;
            }
        }
    }
}
