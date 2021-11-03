using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    //ブロックヒットオブジェクト
    private GameObject _BlockHit;

    //座標
    private Vector3 _position;
    //アザー座標
    private Vector3 _other_psition;
    //ブロックヒットコライダー
    private BoxCollider _block_hit_Cllider;
    //表示
    private MeshRenderer _renderer;
    //色
    private Color _color;
    //マテリアル
    private Material _material;
    //テクスチャ★
    public Texture _texture1;
    public Texture _texture2;
    public Texture _texture3;
    public Texture _texture4;

    //ステータス
    public int _st;
    //バージョン
    public int _ver;
    //日時
    public DateTime _now;
    //ライフ★
    private int _life;
    //ダメージステータス★
    private int _dame_st;

    //_st=1-基本形
    //_st=2-セット

    void Awake()
    {
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
        //★
        _material.mainTexture = _texture1;
        _life = 10;
        _dame_st = 0;
    }

    //他のブロックと当たる
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block" && _now > other.gameObject.GetComponent<BlockManager>()._now)
        {
            _other_psition = other.gameObject.transform.localPosition;
            _position = transform.localPosition;
            if (_position.x == _other_psition.x && _position.z == _other_psition.z)
            {
                //if (_position == _other_psition)
                //{
                    _position.y = _other_psition.y + 1f;
                transform.localPosition = _position;
            }
        }
    }

    public void ActiveSet(int _no)
    {
        _now = DateTime.Now;
        if (_no == 1)
        {
            _st = 2;
            _color.g = 0;
            _color.b = 0;
            _color.a = 0.5f;

            _block_hit_Cllider.enabled = false;
        }
        else if (_no == 2)
        {
            _st = 1;
            _color.g = 1;
            _color.b = 1;
            _color.a = 1;

            _block_hit_Cllider.enabled = true;

            //★
            GameManager._block_st.Add(true);
            GameManager._block_px.Add(transform.position.x);
            GameManager._block_py.Add(transform.position.y);
            GameManager._block_pz.Add(transform.position.z);
        }
        _renderer.material.color = _color;
    }

    //消滅セット
    public void DesSet()
    {
        if (_ver == GameManager._block_c)
        {
            --GameManager._block_c;
        }
        //★
        if (_st == 1)
        {
            GameManager._block_st[_ver - 1] = false;
        }

        Destroy(this.gameObject);

    }

    //ダメージセット★
    public void DameSet()
    {
        --_life;
        if (_dame_st == 0 && _life <= 7)
        {
            _dame_st = 1;
            _material.mainTexture = _texture2;
        }
        else if (_dame_st == 1 && _life <= 5)
        {
            _dame_st = 2;
            _material.mainTexture = _texture3;
        }
        else if (_dame_st == 2 && _life <= 3)
        {
            _dame_st = 3;
            _material.mainTexture = _texture4;
        }

        if (_life <= 0)
        {
            DesSet();
        }
    }
}
