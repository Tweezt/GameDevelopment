using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFlash : MonoBehaviour
{

    private Renderer rend;
    private Color storedColor;

    public static HeadFlash instance;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        storedColor = rend.material.GetColor("_Color");
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeadColor()
    {
        rend.material.SetColor("_Color", Color.red);
    }

    public void SetHeadOriginal()
    {
        rend.material.SetColor("_Color", storedColor);
    }
}
