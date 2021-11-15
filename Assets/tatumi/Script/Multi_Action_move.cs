using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi_Action_move : MonoBehaviour
{
    [Header("合体番号")]
    public int action_num;

    GameObject player; //参照元OBJそのものが入る変数

    GameObject BackButton; //参照元OBJそのものが入る変数

    DeletAction script; //参照元Scriptが入る変数

    [Header("非表示対象オブジェクト")]
    public GameObject Button;

    //現在の位置を取得
    private Vector3 pos;
    private float first_x;//初期位置

    // Player script; //参照元Scriptが入る変数

    // Start is called before the first frame update
    void Start()
    {
        //PL
        player = GameObject.Find("Player"); //ActionButtonをオブジェクトの名前から取得して変数に格納する

        //戻すボタン
        BackButton = GameObject.Find("BackButton"); //オブジェクトの名前から取得して変数に格納する
        script = BackButton.GetComponent<DeletAction>(); //OBJの中にあるScriptを取得して変数に格納する

        pos = this.gameObject.transform.position;
        first_x = pos.x;

        script.multi_objs[script.multi_now] = this.gameObject;
        script.timing[script.multi_now] = script.now;
        script.multi_now++;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PushButton()
    {
        if (action_num == 0)
        {
            player.GetComponent<Player>().Push_highjump();
            Set_Back();
        }
        else if (action_num == 1)
        {
            player.GetComponent<Player>().Push_wallkick();
            Set_Back();
        }
        else if (action_num == 2)
        {
            player.GetComponent<Player>().Push_longjump();
            Set_Back();
        }
        else if (action_num == 3)
        {
            player.GetComponent<Player>().Push_sliding();
            Set_Back();
        }

        
    }

    void Set_Back()
    {
        Button.SetActive(false);
        //消えた時初期位置に戻る
        this.gameObject.transform.position = new Vector3(first_x, -127.0f, pos.z);

        script.objs[script.now] = this.gameObject;
        script.now++;
    }

    //問題点
    public void Set_Active(bool a)
    {
        Button.SetActive(a);
    }
}
