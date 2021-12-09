using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    private GameObject _Panda;
    private PandaManager _PandaManager;

    private bool _hit_st;


    void Awake()
    {
        _Panda = GameObject.Find("Panda");
        _PandaManager = _Panda.GetComponent<PandaManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _hit_st=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Panda" &&(_PandaManager._st==1|| _PandaManager._st == 2|| _PandaManager._st == 3)&&_hit_st)
        {
            _PandaManager.WinSet();
            _hit_st = false;
        }
    }
}
