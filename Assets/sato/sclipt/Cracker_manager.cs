using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracker_manager : MonoBehaviour
{
    [Header("������𕡐������")]
    public int clone_num;

    [Header("��������I�u�W�F�N�g")]
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
