using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamera_maneger : MonoBehaviour
{
    //0=front,1=right,2=left,3=Back,4à»ç~=all
    public int MAXCamera;
    [Header("ÉJÉÅÉâëŒè€ 0=front,1=right,2=left,3=Back,4à»ç~=all")]
    public CinemachineVirtualCamera[] Cameras;
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
        for(int i=0;i!=MAXCamera;i++)
        {
            Cameras[i].Priority = 10;
        }

        Cameras[a].Priority = 11;
    }
}
