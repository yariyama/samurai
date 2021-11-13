using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLayerManager : MonoBehaviour
{
    public string SortingLayerName = "Default";
    public int SortingOrder = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().sortingLayerName = SortingLayerName;
        GetComponent<MeshRenderer>().sortingOrder = SortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
