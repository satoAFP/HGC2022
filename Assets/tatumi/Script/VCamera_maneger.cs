using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamera_maneger : MonoBehaviour
{
    //0=front,1=right,2=left,3=Back,4�ȍ~=all
    public int MAXCamera;

    //�V�l�}�J�����擾�p
    [Header("�J�����Ώ� 0=back,1=right,2=front,3=left,4�ȍ~=all")]
    public CinemachineVirtualCamera[] Cameras;

    //�D�揇�ʊ�l
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
        //a�̐����ɂ��Ή�����Cameras�̒��̃V�l�}�J�����Ɉڂ�
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
