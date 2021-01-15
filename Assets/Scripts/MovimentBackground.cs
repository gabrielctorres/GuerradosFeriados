using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentBackground : MonoBehaviour
{

    public Material material;  
    Vector2 offset,offset2;
    // Use this for initialization

    private void Awake()
    {
        
    }

    void Start()
    {

        offset = new Vector2(0.5f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;

    }
}
