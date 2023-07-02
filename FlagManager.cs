using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    //�p���_�I�u�W�F�N�g
    private GameObject _Panda;
    //�p���_�X�N���v�g
    private PandaManager _PandaManager;

    //�X�e�[�W�N���A�I�u�W�F�N�g
    private GameObject _StageClear;
    //�X�e�[�W�N���A�X�N���v�g
    private StageClearManager _StageClearManager;

    //�X�e�[�^�X
    private int _st;

    //_st=1-��{�`
    //_st=2-�N���A

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
