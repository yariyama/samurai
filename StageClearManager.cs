using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : MonoBehaviour
{
    //座標
    private Vector2 _position;
    //オーディオソース
    private AudioSource _audio;
    //スライド音
    public AudioClip _slide_se;

    //ステータス
    private int _st;

    //_st=1-基本形
    //_st=2-セット

    void Awake()
    {
        _position = transform.localPosition;
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
            }
            transform.localPosition = _position;
        }
    }

    public void ActiveSet()
    {
        _st = 2;
        _position.x = 800;
        transform.localPosition = _position;

        _audio.clip = _slide_se;
        _audio.Play();
    }
}
