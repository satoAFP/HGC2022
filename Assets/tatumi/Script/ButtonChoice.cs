using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChoice : MonoBehaviour
{
    GameObject BackButton; //参照元OBJそのものが入る変数

    DeletAction script; //参照元Scriptが入る変数

    [Header("非表示対象オブジェクト")]
    public GameObject Button;

    // Start is called before the first frame update
    void Start()
    {
        BackButton = GameObject.Find("BackButton"); //オブジェクトの名前から取得して変数に格納する
        script = BackButton.GetComponent<DeletAction>(); //OBJの中にあるScriptを取得して変数に格納する
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushButton()
    {

        script.objs[script.now] = this.gameObject;
        script.now += 1;

        Button.SetActive(false);

        //左クリ
        if (Input.GetMouseButtonDown(0))
        {
            Button.SetActive(false);
            if(script.now==0)
            {
                script.objs[script.now] = this.gameObject;
            }
            else
            {
                script.now++;
                script.objs[script.now] = this.gameObject;
            }
        }
        //右クリ
        else if(Input.GetMouseButtonDown(1))
        {

        }
        //真ん中クリ
        else if(Input.GetMouseButtonDown(2))
        {

        }
       
    }
}
