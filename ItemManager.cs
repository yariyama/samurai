using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;

    //アニメーター
    private Animator _animator;
    //オーディオソース
    private AudioSource _audio;
    //効果音
    public AudioClip _se1;
    public AudioClip _se2;

    //ステータス
    private int _st;
    //タイプ
    public int _tp;

    //_st=1-基本形
    //_st=2-ゲット

    void Awake()
    {
        _Panda = GameObject.Find("Panda");
        _PandaManager = _Panda.GetComponent<PandaManager>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("base");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Panda" && _st==1)
        {
            if (_tp==2)
            {
                _PandaManager.SDameSet();
                _audio.clip = _se2;
            }
            else
            {
                _audio.clip = _se1;
            }
            _st = 2;
            _animator.Play("get");
            _audio.Play();
        }
    }

    void DelSet()
    {
        Destroy(this.gameObject);
    }
}
