using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ�----------------------------------
    [Header("�ړ����x")]
    public float moveSpeed;
    [Header("�W�����v��")]
    public float push_power;
    [Header("�ő�W�����v��")]
    public int Max_Jmup;

    //private�ϐ�-----------------------------------------
    private Vector3 push;//���Z�������x�N�g����
    private int Jump_Count = 0;//�A���ŃW�����v�����񐔂��J�E���g
    private bool Jump_Flag = true;//

    // Start is called before the first frame update
    void Start() {
        push = new Vector3(0.0f, push_power, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate() {
        //���E����
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        float moveX = inputX * moveSpeed * Time.deltaTime;
        float moveZ = inputZ * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0.0f, moveZ);

        //�W�����v����
        if (transform.position.y <= 1.0f || Max_Jmup - 1 != Jump_Count) {
            if (Input.GetKey(KeyCode.Space)) {
                if (Jump_Flag == true) {
                    this.GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
                    Jump_Count++;
                    Jump_Flag = false;
                }
            }
            else {
                Jump_Flag = true;
            }
        }
        if(transform.position.y<=1.0f) {
            Jump_Count = 0;
        }
    }
}
