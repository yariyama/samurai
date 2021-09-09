//★
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    //ブロックヒットオブジェクト★
    private GameObject _BlockHit;

    //座標
    private Vector3 _position;
    //アザー座標★
    private Vector3 _other_psition;
    //ブロックヒットコライダー★
    private BoxCollider _block_hit_Cllider;
    //表示
    private MeshRenderer _renderer;
    //色
    private Color _color;
    //マテリアル
    private Material _material;

    //ステータス
    public int _st;
    //バージョン
    public int _ver;
    //日時★
    public DateTime _now;

    //_st=1-基本形
    //_st=2-セット

    void Awake()
    {
        //★
        _BlockHit = transform.Find("BlockHit").gameObject;
        _block_hit_Cllider = _BlockHit.GetComponent<BoxCollider>();

        _position = transform.localPosition;
        _renderer = GetComponent<MeshRenderer>();
        _material = _renderer.material;
        _color = _material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //他のブロックと当たる★
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block" && _now > other.gameObject.GetComponent<BlockManager>()._now)
        {
            _other_psition = other.gameObject.transform.localPosition;
            _position = transform.localPosition;
            if (_position.x == _other_psition.x && _position.z == _other_psition.z)
            {
                _position.y = _other_psition.y + 1f;
                transform.localPosition = _position;
            }
        }
    }

    public void ActiveSet(int _no)
    {
        //★
        _now = DateTime.Now;
        if (_no == 1)
        {
            _st = 2;
            _color.g = 0;
            _color.b = 0;
            _color.a = 0.5f;
            //★
            _block_hit_Cllider.enabled = false;
        }
        else if (_no == 2)
        {
            _st = 1;
            _color.g = 1;
            _color.b = 1;
            _color.a = 1;
            //★
            _block_hit_Cllider.enabled = true;
        }
        _renderer.material.color = _color;
    }
}
