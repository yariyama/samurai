using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainUIManager : MonoBehaviour
{
    private GameObject[] _LifeGage = new GameObject[3];
    private LifeGageManager[] _LifeGageManager = new LifeGageManager[3];

    void Awake()
    {
        for (int i=0; i<3;i++)
        {
            _LifeGage[i] = transform.Find("LifeGage"+(i+1)).gameObject;
            _LifeGageManager[i] = _LifeGage[i].GetComponent<LifeGageManager>();
        }
    }

    public void LifeGageSet()
    {
        if (GameManager._life==2.5f)
        {
            _LifeGageManager[2].DeSet(1);
        }
        else if (GameManager._life == 1.5f)
        {
            _LifeGageManager[1].DeSet(1);
        }
        else if (GameManager._life == 0.5f)
        {
            _LifeGageManager[0].DeSet(1);
        }
        else if (GameManager._life == 2f)
        {
            _LifeGageManager[2].DeSet(2);
            _LifeGageManager[1].MaruSet();
        }
        else if (GameManager._life == 1f)
        {
            _LifeGageManager[1].DeSet(2);
            _LifeGageManager[0].MaruSet();
        }
        else
        {
            _LifeGageManager[0].DeSet(2);
        }
    }
}
