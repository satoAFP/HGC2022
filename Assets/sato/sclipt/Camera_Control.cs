using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    private GameObject mainCamera;              //���C���J�����i�[�p
    private GameObject playerObject;            //��]�̒��S�ƂȂ�v���C���[�i�[�p
    private bool click_check = false;           //�N���b�N�������Ă��邩����


    public float rotateSpeed = 2.0f;            //��]�̑���

    //�Ăяo�����Ɏ��s�����֐�
    void Start() {
        //���C���J�����ƃ��j�e�B���������ꂼ��擾
        mainCamera = Camera.main.gameObject;
        playerObject = GameObject.Find("Player");
    }


    //�P�ʎ��Ԃ��ƂɎ��s�����֐�
    void Update() {
        //rotateCamera�̌Ăяo��
        rotateCamera();
    }

    //�J��������]������֐�
    private void rotateCamera() {
        //�}�E�X�̒���������
        if (Input.GetMouseButtonDown(0)) 
            click_check = true;
        if (Input.GetMouseButtonUp(0))
            click_check = false;

        if (click_check == true) {
            //Vector3��X,Y�����̉�]�̓x�������`
            Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed,  Input.GetAxis("Mouse Y") * rotateSpeed, Input.GetAxis("Mouse Y") * rotateSpeed);
            
            //transform.RotateAround()�����悤���ă��C���J��������]������
            mainCamera.transform.RotateAround(playerObject.transform.position, Vector3.up, angle.x);
            mainCamera.transform.RotateAround(playerObject.transform.position, transform.right, angle.y);
            //mainCamera.transform.RotateAround(playerObject.transform.position, transform.right, angle.z);
        }
    }
}
