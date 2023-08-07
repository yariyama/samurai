using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int _st;
    //�^�C�}�[
    private float _timer;
    //�X�e�[�W
    public string _stage;

    //_st=1-��{�`
    //_st=2-�N���A

    private void Awake()
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
        _timer =0;
        _StageClear.SetActive(false);
    }

    void FixedUpdate()
    {
        if (_st==2)
        {
            _timer += Time.deltaTime;
            if (_timer>=3)
            {
                _timer = 0;
                SceneManager.LoadScene(_stage);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Panda" && _st==1)
        {
            _st = 2;
            _PandaManager.ClearSet();
            _StageClear.SetActive(true);
            _StageClearManager.ActiveSet();
        }
    }
}
