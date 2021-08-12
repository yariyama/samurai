using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1Manager : MonoBehaviour
{
    private GameObject _HpGage;
    private HpGageManager _HpGageManager;

    public int _ver;

    private void Awake()
    {
        _HpGage = GameObject.Find("HpGage");
        _HpGageManager = _HpGage.GetComponent<HpGageManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (_ver==1)
        {
            if (_HpGageManager._Hp < 100)
            {
                _HpGageManager.GageSet(1, 5);
            }
        }
        else
        {
            if (_HpGageManager._Hp > 0)
            {
                _HpGageManager.GageSet(2, 5);
            }
        }
    }
}
