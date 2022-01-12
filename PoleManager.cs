using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleManager : MonoBehaviour
{
    //ブロックオブジェクト
    private GameObject _Block;
    //ブロックスクリプト
    private BlockManager _BlockManager;

    //ステータス
    private int _st;
    //ヒットの可否
    private bool _hit_st;

    //_st=1-基本形

    void OnTriggerStay(Collider other)
    {
        if (_hit_st)
        {
            if (other.gameObject.tag=="Block")
            {
                _hit_st = false;
                _Block = other.gameObject;
                _BlockManager = _Block.GetComponent<BlockManager>();
                _BlockManager.DameSet();
            }
        }
    }

    public void ActiveSet()
    {
        _st = 1;
        _hit_st = true;
    }
}
