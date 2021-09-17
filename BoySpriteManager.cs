using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoySpriteManager : MonoBehaviour
{
    //攻撃状態
    public int _attack_st;

    //_attack_st=1-攻撃セット
    //_attack_st=2-攻撃

    // Start is called before the first frame update
    void Start()
    {
        _attack_st = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AttackSet()
    {
        _attack_st = 1;
    }

    void Attack()
    {
        _attack_st = 2;
    }

    void AttackEnd()
    {
        _attack_st = 0;
    }
}
