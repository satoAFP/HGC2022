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
        // transform���擾
        Transform myTransform = this.transform;

        // ���W���擾
        Vector3 pos = myTransform.position;


        if (pos.y < 10.0f&&MousFlag==true)
            pos.y += 0.01f;    // y���W��0.01���Z
        else if (pos.y > -66.0f && MousFlag == true)
            pos.y -= 0.01f;     // y���W��0.01���Z


        myTransform.position = pos;  // ���W��ݒ�
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
