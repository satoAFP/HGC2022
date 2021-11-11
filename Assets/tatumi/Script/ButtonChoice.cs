using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChoice : MonoBehaviour
{
    GameObject BackButton; //参照元OBJそのものが入る変数

    DeletAction script; //参照元Scriptが入る変数

    [Header("非表示対象オブジェクト")]
    public GameObject Button;

    //現在の位置を取得
    private Vector3 pos;
    private float first_x;//初期位置

   

    // Start is called before the first frame update
    void Start()
    {
        BackButton = GameObject.Find("BackButton"); //オブジェクトの名前から取得して変数に格納する
        script = BackButton.GetComponent<DeletAction>(); //OBJの中にあるScriptを取得して変数に格納する

        pos = this.gameObject.transform.position;
        first_x = pos.x;

        
    }

    // Update is called once per frame
    void Update()
    {
       

       
    }

    public void PushButton(bool set)
    {

        //左クリ
        if (Input.GetMouseButtonDown(0)||set==true)
        {
            Debug.Log("Left");

            Button.SetActive(false);
            if(script.now==0)
            {
                script.objs[script.now] = this.gameObject;
                script.now++;
            }
            else
            {
                //script.now++;
                script.objs[script.now] = this.gameObject;
            }
        }
        //右クリ
        else if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right");
            Debug.Log($"pos.y={pos.y:0.00}");
            if (pos.x<=600)
            //現在の位置から移動せず
            this.gameObject.transform.position = new Vector3(800.0f, pos.y, pos.z);
            else if(pos.x<=1200)
                //現在の位置から移動
                this.gameObject.transform.position = new Vector3(pos.x+200.0f, pos.y, pos.z);
            else
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);

            pos = this.gameObject.transform.position;

            if (pos.x == 800.0f && this.tag != "Multi_action1")
            {
                Debug.Log("tag1 get");//ok
                this.tag = "Multi_action1";
            }
            else if (pos.x == 1000.0f)
                this.tag = "Multi_action2";
            else if (pos.x == 1200.0f)
                this.tag = "Multi_action3";
            else if (pos.x == 1400.0f)
                this.tag = "Multi_action4";
            else
                this.tag = "Untagged";
        }
        //真ん中クリ
        else if(Input.GetMouseButtonDown(2))
        {
            this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
        }
       
    }

    public void Set_Active(bool set)
    {
        PushButton(set);
    }
}
