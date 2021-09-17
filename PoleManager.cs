using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleManager : MonoBehaviour
{
    //チップセットオブジェクト★
    private GameObject _ChipSet;
    //チップセットスクリプト★
    private ChipSetManager _ChipSetManager;

    //表示
    private MeshRenderer _renderer;

    //ステータス
    private int _st;

    void Awake()
    {
        //★
        _ChipSet = GameObject.Find("ChipSet");
        _ChipSetManager = _ChipSet.GetComponent<ChipSetManager>();
        _ChipSet.SetActive(false);

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
