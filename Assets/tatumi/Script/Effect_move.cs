using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_move : MonoBehaviour
{
    //エフェクトの種類+場所管理用
    //0-1 爆発 0-左 1-右
    //2-3 集中 2-左 3-右
    public int Efeect_number;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf==true&& Efeect_number<2)
        {
            ;
        }
    }

    public void SetActive(bool a)
    {
        this.SetActive(a);
    }
}
