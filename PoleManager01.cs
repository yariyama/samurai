using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleManager01 : MonoBehaviour
{
    //表示
    private MeshRenderer _renderer;

    //ステータス
    private int _st;

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer.enabled = false;
        _st = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
