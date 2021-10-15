using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChoice : MonoBehaviour
{
   // private bool MousFlag;
    public Transform myTransform;
    public bool MousFlag;

    // Start is called before the first frame update
    void Start()
    {
        MousFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;


        if (pos.y < 10.0f&&MousFlag==true)
            pos.y += 0.01f;    // y座標へ0.01加算
        else if (pos.y > -66.0f && MousFlag == true)
            pos.y -= 0.01f;     // y座標へ0.01減算


        myTransform.position = pos;  // 座標を設定
    }

    public void OnMouseOver()
    {
        MousFlag = true;
    }

    public void OnMouseExit()
    {
        //MousFlag = false;
    }
}
