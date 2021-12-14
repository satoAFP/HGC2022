using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slime_ResultMove : MonoBehaviour
{
    [Header("横に動く速度")]
    public float move_speed;

    [Header("止まる位置")]
    public float stop_pos;

    [Header("ここから下はいじらない")]
    public bool goal_move = false;              //ゴール時のアクション再生


    private Vector3 mem_pos;    //このオブジェクトの初期位置記憶
    private Vector3 pos;        //このオブジェクトの位置

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
