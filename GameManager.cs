using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //獲得ポイント
    public static int _get_point;

    // Start is called before the first frame update
    void Start()
    {
        _get_point = PlayerPrefs.GetInt("GetPoint",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
