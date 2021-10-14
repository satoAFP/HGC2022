using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    private GameObject mainCamera;              //メインカメラ格納用
    private GameObject playerObject;            //回転の中心となるプレイヤー格納用
    private bool click_check = false;           //クリックし続けているか判定


    public float rotateSpeed = 2.0f;            //回転の速さ

    //呼び出し時に実行される関数
    void Start() {
        //メインカメラとユニティちゃんをそれぞれ取得
        mainCamera = Camera.main.gameObject;
        playerObject = GameObject.Find("Player");
    }


    //単位時間ごとに実行される関数
    void Update() {
        //rotateCameraの呼び出し
        rotateCamera();
    }

    //カメラを回転させる関数
    private void rotateCamera() {
        //マウスの長押し判定
        if (Input.GetMouseButtonDown(0)) 
            click_check = true;
        if (Input.GetMouseButtonUp(0))
            click_check = false;

        if (click_check == true) {
            //Vector3でX,Y方向の回転の度合いを定義
            Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed,  Input.GetAxis("Mouse Y") * rotateSpeed, Input.GetAxis("Mouse Y") * rotateSpeed);
            
            //transform.RotateAround()をしようしてメインカメラを回転させる
            mainCamera.transform.RotateAround(playerObject.transform.position, Vector3.up, angle.x);
            mainCamera.transform.RotateAround(playerObject.transform.position, transform.right, angle.y);
            //mainCamera.transform.RotateAround(playerObject.transform.position, transform.right, angle.z);
        }
    }
}
