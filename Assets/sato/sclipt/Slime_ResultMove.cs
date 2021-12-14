using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slime_ResultMove : MonoBehaviour
{
    [Header("���ɓ������x")]
    public float move_speed;

    [Header("�~�܂�ʒu")]
    public float stop_pos;

    [Header("�������牺�͂�����Ȃ�")]
    public bool goal_move = false;              //�S�[�����̃A�N�V�����Đ�


    private Vector3 mem_pos;    //���̃I�u�W�F�N�g�̏����ʒu�L��
    private Vector3 pos;        //���̃I�u�W�F�N�g�̈ʒu

    // Start is called before the first frame update
    void Start()
    {
        mem_pos = this.gameObject.GetComponent<RectTransform>().position;
        pos = this.gameObject.GetComponent<RectTransform>().position;
        DontDestroyOnLoad(GameObject.Find("slime_UI"));
    }

    // Update is called once per frame
    void Update()
    {
        if (goal_move)
        {
            pos.x += move_speed;
            this.gameObject.GetComponent<RectTransform>().position = pos;

            if (this.gameObject.GetComponent<RectTransform>().localPosition.x > 0)  
            {
                if (SceneManager.GetActiveScene().name != "Result")
                {
                    SceneManager.LoadScene("Result");
                }
            }
            if (pos.x > stop_pos) 
            {
                Debug.Log("aaa");
                pos = mem_pos;
                goal_move = false;
            }
        }
    }
}
