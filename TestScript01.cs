using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript01 : MonoBehaviour
{
    private Rigidbody2D _rbody;
    private BoxCollider2D _collider;


    void Awake()
    {
        _rbody = this.GetComponent<Rigidbody2D>();
        _collider = this.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //_rbody.gravityScale = 0;
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
