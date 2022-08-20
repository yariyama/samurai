using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRendererd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ƒJƒƒ‰‚É‰f‚Á‚Ä‚¢‚éŠÔ
    void OnWillRenderObject()
    {
        if (Camera.current.tag=="Mirror")
        {
            Debug.Log("OK!");
        }
    }
}
