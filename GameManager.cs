using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //���C�t
    public static float _life;
    //�X�R�A
    public static int _score;


    // Start is called before the first frame update
    void Start()
    {
        _life = 3;
        _score = 0;
    }
}
