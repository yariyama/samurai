using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    //カメラ
    private Camera _camera;

    //ステータス
    public int _st;

    //_st=1-基本形

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _st = 0;
        _camera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveSet()
    {
        _st = 1;
        _camera.enabled = true;
    }

    public void DelSet()
    {
        _st = 0;
        _camera.enabled = false;
    }
}
