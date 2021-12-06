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

    private bool now_ani;

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

        now_ani = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PushButton()
    {
        if (now_ani == false)
        {
            if (action_num == 0)
            {
                player.GetComponent<Player>().Push_highjump();
                Set_Back();

                int n = this.transform.parent.childCount;

                Debug.Log($"childs={n:0}");

                GameObject child = transform.Find("image_move").gameObject;

                Debug.Log($"childs={child:0}");

                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;
            }
            else if (action_num == 1)
            {
                player.GetComponent<Player>().Push_wallkick();
                Set_Back();

                int n = this.transform.parent.childCount;

                Debug.Log($"childs={n:0}");

                GameObject child = transform.Find("image_move").gameObject;

                Debug.Log($"childs={child:0}");

                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;
            }
            else if (action_num == 2)
            {
                player.GetComponent<Player>().Push_longjump();
                Set_Back();

                int n = this.transform.parent.childCount;

                Debug.Log($"childs={n:0}");

                GameObject child = transform.Find("image_move").gameObject;

                Debug.Log($"childs={child:0}");

                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;
            }
            else if (action_num == 3)
            {
                player.GetComponent<Player>().Push_sliding();
                Set_Back();

                int n = this.transform.parent.childCount;

                Debug.Log($"childs={n:0}");

                GameObject child = transform.Find("image_move").gameObject;

                Debug.Log($"childs={child:0}");

                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;
            }
        }

        
    }

    void Set_Back()
    {
        //now_ani = true;
        //Invoke(nameof(null_active), 1.15f);
        //Button.SetActive(false);
        //消えた時初期位置に戻る
        this.gameObject.transform.position = new Vector3(first_x, -127.0f, pos.z);

        script.objs[script.now] = this.gameObject;
        script.now++;
    }

    void null_active()
    {
        Button.SetActive(false);
    }


    public void Set_Active(bool a)
    {
        now_ani = false;
        Button.SetActive(a);
    }
}
