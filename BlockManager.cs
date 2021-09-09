using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    //座標
    private Vector3 _position;
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

    //_st=1-基本形
    //_st=2-セット

    void Awake()
    {
        _position = transform.localPosition;
        _renderer = GetComponent<MeshRenderer>();
        _material = _renderer.material;
        _color = _material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveSet(int _no)
    {
        if (_no == 1)
        {
            _st = 2;
            _color.g = 0;
            _color.b = 0;
            _color.a = 0.5f;
        }
        else if (_no == 2)
        {
            _st = 1;
            _color.g = 1;
            _color.b = 1;
            _color.a = 1;
        }
        _renderer.material.color = _color;
    }
}
