using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletAction : MonoBehaviour
{
    [Header("覚える限度数")]
    //オブジェ格納用変数
    public GameObject[] objs;

    [Header("触らない")]
    public int now;

    // Start is called before the first frame update
    void Start()
    {
        now = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
