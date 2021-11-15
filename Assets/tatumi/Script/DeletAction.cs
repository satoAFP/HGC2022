using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletAction : MonoBehaviour
{
    [Header("覚える通常限度数")]
    //オブジェ格納用変数
    public GameObject[] objs;

    [Header("覚える合体アクション限度数")]
    //オブジェ格納用変数
    public GameObject[] multi_objs;

    [Header("触らない")]
    public int now,multi_now;

    public int[] timing;

    // Start is called before the first frame update
    void Start()
    {
        now = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushButton()
    {
        if (now != 0)
        {
            objs[now - 1].GetComponent<ButtonChoice>().Set_Active(false);
            //object[now]= null;
            
            now--;
        }
    }
}
