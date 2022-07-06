using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamera_maneger : MonoBehaviour
{
    //0=front,1=right,2=left,3=Back,4以降=all
    public int MAXCamera;

    //シネマカメラ取得用
    [Header("カメラ対象 0=back,1=right,2=front,3=left,4以降=all")]
    public CinemachineVirtualCamera[] Cameras;

    //優先順位基準値
    private int nowCamera = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CameraChange(int a)
    {
        //aの数字により対応したCamerasの中のシネマカメラに移る
        for (int i = 0; i != MAXCamera; i++)
        {
            Cameras[i].Priority = 10;
        }

        if (MAXCamera < a || 0 > a)
            ;
        else
            Cameras[a].Priority = 13;
    }
}
