using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    //表示
    private MeshRenderer _renderer;
    //マテリアル
    private Material _material;
    //テクスチャ
    public Texture _texture1;
    public Texture _texture2;
    public Texture _texture3;
    public Texture _texture4;

    //ステータス
    public int _st;
    //ライフ
    private int _life;

    //_st=1-基本形

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _material = _renderer.material;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _life = 4;
        _material.mainTexture = _texture1;
    }

    //ダメージセット
    public void DameSet()
    {
        --_life;
        if (_life==3)
        {
            _material.mainTexture = _texture2;
        }
        else if (_life == 2)
        {
            _material.mainTexture = _texture3;
        }
        else if (_life == 1)
        {
            _material.mainTexture = _texture4;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
