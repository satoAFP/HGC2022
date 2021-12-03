using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChoice : MonoBehaviour
{
    GameObject BackButton; //参照元OBJそのものが入る変数

    DeletAction script; //参照元Scriptが入る変数

    GameObject ActionButton; //参照元OBJそのものが入る変数

    ActionButton_SC scriptac; //参照元Scriptが入る変数

    public bool vanish;
    private bool now_ani;

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

        ActionButton = GameObject.Find("ActionBotton "); //ActionButtonをオブジェクトの名前から取得して変数に格納する
        scriptac = ActionButton.GetComponent<ActionButton_SC>(); //OBJの中にあるScriptを取得して変数に格納する

        pos = this.gameObject.transform.position;
        first_x = pos.x;

        vanish = true;
        now_ani = false;
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    public void PushButton(bool set)
    {
        if (now_ani == false)
        {
            Debug.Log("消えかけてはない");
            //左クリ
            if (Input.GetMouseButtonDown(0) || set == true)
            {
                Debug.Log("Left");
                //now_ani = true;

                int n = this.transform.parent.childCount;

                Debug.Log($"childs={n:0}"); 

                GameObject child = transform.Find("image_move").gameObject;

                Debug.Log($"childs={child:0}");

                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;


                //var animator = Button.GetComponent<Animator>();
                //animator.Play("Selected");
                //animator.Update(0f);

                //消す処理をscriptに一任
                //if (set==false)
                //Invoke(nameof(null_active), 1.15f);
                //else
                //    Button.SetActive(false);
                //vanish = true;
                //消えた時初期位置に戻る
                this.gameObject.transform.position = new Vector3(first_x, -127.0f, pos.z);

                script.objs[script.now] = this.gameObject;
                script.now++;
                scriptac.set_text((int)(first_x / 130), 1);
            }
            //右クリ
            else if (Input.GetMouseButtonDown(1))
            {
                pos = this.gameObject.transform.position;
                Debug.Log("Right");
                // Debug.Log($"pos.y={pos.y:0.00}");
                if (pos.x <= 500)
                    //現在の位置から移動せず
                    this.gameObject.transform.position = new Vector3(530.0f, pos.y, pos.z);
                else if (pos.x < 790)
                    //現在の位置から移動
                    this.gameObject.transform.position = new Vector3(pos.x + 130.0f, pos.y, pos.z);
                else
                    this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);

                pos = this.gameObject.transform.position;

                if (pos.x == 530.0f)
                {
                    Debug.Log("tag1 get");//ok
                    this.tag = "Multi_action1";
                }
                else if (pos.x == 660.0f)
                    this.tag = "Multi_action2";
                else if (pos.x == 790.0f)
                    this.tag = "Multi_action3";
               
                else
                    this.tag = "Untagged";
            }
            //真ん中クリ
            else if (Input.GetMouseButtonDown(2))
            {
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
                this.tag = "Untagged";
            }
            else if (set == false)
            {
               
                Button.SetActive(true);
                vanish = true;
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
                this.tag = "Untagged";
                scriptac.set_text((int)(first_x / 130), 1);
            }
        }
       
    }

    void null_active()
    {
        Button.SetActive(false);
    }

    public void Set_Active(bool set)
    {
        now_ani = false;
        PushButton(set);
    }

}
