using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //パンダオブジェクト
    private GameObject _Panada;
    //パンダスクリプト
    private PandaManager _PandaManager;

    //アニメーター
    private Animator _animator;

    //ステータス
    private int _st;
    //タイプ
    public int _tp;


    //_st=1-基本形
    //_st=2-ゲット


    void Awake()
    {
        _Panada = GameObject.Find("Panda");
        _PandaManager = _Panada.GetComponent<PandaManager>();

        _animator = GetComponent<Animator>();
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
            }
            _st = 2;
            _animator.Play("get");
        }
    }

    void DelSet()
    {
        Destroy(this.gameObject);
    }
}
