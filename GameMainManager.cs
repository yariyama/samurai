using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainManager : MonoBehaviour
{
    //音符プレファブ
    public GameObject _Note_pf;
    //音符オブジェクト
    private GameObject _Note;
    //音符スクリプト
    private NoteManager _NoteManager;

    //オーディオソース
    private AudioSource _audio;
    //BGM
    public AudioClip _bgm1;
    public AudioClip _bgm2;

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

    //音符セットタイム（音楽2）
    public float[] _note_set_t2 = new float[100];
    //音符セット位置（音楽2）
    public int[] _note_set_p2 = new int[100];

    //セットカント
    private int _set_count;
    //繰り返し用
    private int i;
    //タイマー
    public float _timer;
    //セーフエリアまでの移動時間
    public float _safe_time = 2.2f;
    //ベースタイム
    private float _b_time;

    //エンドタイム
    private float _e_time;
    //エンドタイムベース
    public float[] _e_time_b = new float[2];

    //_st=1-基本

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {

        ActiveSet(1);
        
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;

            if (_note_set_t[_set_count]!=0)
            {
                _b_time = _note_set_t[_set_count] - _safe_time;
                if (_timer>=_b_time)
                {
                    ++_set_count;
                    NoteSet(_set_count);
                }
            }

            if (_timer>=_e_time)
            {
                _timer = 0;
                _st = 0;
            }
        }
    }

    //音符セット
    public void NoteSet(int _no)
    {
        _Note = Instantiate(_Note_pf) as GameObject;
        for (i=0;i<30;i++) {
            if (!_note_st[i])
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

    public void ActiveSet(int _no)
    {
        for (i = 1; i < 30; i++)
        {
            _note_st[i] = false;
        }
        _st = 1;
        _timer = 0;
        _set_count = 0;

        if (_no==1)
        {
            _note_set_t = _note_set_t1;
            _note_set_p = _note_set_p1;
            _audio.clip = _bgm1;
        }
        else if (_no==2)
        {
            _note_set_t = _note_set_t2;
            _note_set_p = _note_set_p2;
            _audio.clip = _bgm2;
        }
        _e_time = _e_time_b[_no - 1];

        _audio.Play();
    }
}
