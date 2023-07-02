using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    //パンダオブジェクト
    private GameObject _Panda;
    //パンダスクリプト
    private PandaManager _PandaManager;

    //ステージクリアオブジェクト
    private GameObject _StageClear;
    //ステージクリアスクリプト
    private StageClearManager _StageClearManager;

    //ステータス
    private int _st;

    //_st=1-基本形
    //_st=2-クリア

    void Awake()
    {
        _Panda = GameObject.Find("Panda");
        _PandaManager = _Panda.GetComponent<PandaManager>();

        _StageClear = GameObject.Find("StageClear");
        _StageClearManager = _StageClear.GetComponent<StageClearManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _StageClear.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Panda" && _st==1)
        {
            _st = 2;
            _StageClear.SetActive(true);
            _StageClearManager.ActiveSet();
            _PandaManager.ClearSet();
        }
    }
}
