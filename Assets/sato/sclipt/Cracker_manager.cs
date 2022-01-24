using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracker_manager : MonoBehaviour
{
    [Header("紙吹雪を複製する個数")]
    public int clone_num;

    [Header("複製するオブジェクト")]
    public GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < clone_num; i++) 
        {
            GameObject a = Instantiate(clone, this.transform.position, Quaternion.identity);
            a.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
