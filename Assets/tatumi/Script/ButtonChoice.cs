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

    //スッーと消えるよう変数（現在凍結）
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

        ActionButton = GameObject.Find("ActionBotton"); //ActionButtonをオブジェクトの名前から取得して変数に格納する
        scriptac = ActionButton.GetComponent<ActionButton_SC>(); //OBJの中にあるScriptを取得して変数に格納する

        //最初に出た位置を覚える（戻る処理に使う）
        pos = this.gameObject.transform.position;
        first_x = pos.x;

        //スッーと消えるよう変数（現在凍結）
        vanish = true;
        now_ani = false;
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    public void PushButton(bool set)
    {
        //スッーと消えるよう変数（現在凍結）
        if (now_ani == false)
        {
           
            //左クリ
            if (Input.GetMouseButtonDown(0))
            {
               
                //now_ani = true;

                //自身の子供から複製対象選択＆動かす-------------------------------

                //子供数取得なお使わん
                //int n = this.transform.parent.childCount;

               //image_moveを取得（自身の子供が一番早い）
                GameObject child = transform.Find("image_move").gameObject;

                //複製、動かす信号を発信
                GameObject newObj = Instantiate(child, this.transform, false);
                newObj.GetComponent<Image_move>().Move_on = true;
                //------------------------------------------------------------------


                //スッーと消えるよう変数（現在凍結）--------------------------------
                //var animator = Button.GetComponent<Animator>();
                //animator.Play("Selected");
                //animator.Update(0f);

                //消す処理をscriptに一任
                //if (set==false)
                //Invoke(nameof(null_active), 1.15f);
                //else
                //    Button.SetActive(false);
                //vanish = true;

                //---------------------------------------------------------------------

                //消えた時初期位置に戻る
                this.gameObject.transform.position = new Vector3(first_x, -127.0f, pos.z);

                //バックボタンに登録
                script.objs[script.now] = this.gameObject;
                script.now++;

                //使用回数+1
                scriptac.set_text((int)(first_x / 130), 1);
            }
            //右クリ
            else if (Input.GetMouseButtonDown(1)&&set==false)
            {
                //現在位置取得
                pos = this.gameObject.transform.position;
                GameObject[] multi = GameObject.FindGameObjectsWithTag("Multis");

                if (pos.x >= 660)
                    this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
                else if (pos.x >= 530)
                    //次へ
                    this.gameObject.transform.position = new Vector3(pos.x + 130.0f, pos.y, pos.z);

                else if (pos.x == first_x)
                {
                    Debug.Log(pos.x);
                    //現在の位置から移動
                    this.gameObject.transform.position = new Vector3(530.0f, pos.y, pos.z);

                }
                else
                {
                   
                    this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
                }

                if (multi.Length == 0)
                {
                    this.gameObject.transform.position = new Vector3(660.0f, pos.y, pos.z);
                   
                }
                else if (multi.Length == 1)
                {
                    Vector3 multi_pos = multi[0].gameObject.transform.position;

                    if (multi_pos.x == 530.0f)
                        this.gameObject.transform.position = new Vector3(660.0f, pos.y, pos.z);
                    else
                        this.gameObject.transform.position = new Vector3(530.0f, pos.y, pos.z);
                }
                else if (multi.Length == 2)
                {
                    this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
                   
                }

                //移動後の場所取得
                pos = this.gameObject.transform.position;

                //それぞれの場所でtag付与（マルチクリエイトへ）
                if (pos.x == 530.0f)
                {
                    this.tag = "Multi_action1";
                }
                else if (pos.x == 660.0f)
                    this.tag = "Multi_action2";
                else
                    this.tag = "Untagged";
            }
            //真ん中クリ
            else if (Input.GetMouseButtonDown(2))
            {
                //初期位置へ（タグも再付与）
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
                this.tag = "Untagged";
            }
            //他スクリプトより申請時
            else if (set == true)
            {
                //マルチクリエイトからの申請
                //自身の消えた処理と同じ-------------------以下同文
                script.objs[script.now] = this.gameObject;
                script.now++;
               
                this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
                this.tag = "Untagged";
                scriptac.set_text((int)(first_x / 130), 1);
            }

            else if (set == false)
            {
                //バックで戻されたとき、数を戻す
                scriptac.set_text((int)(first_x / 130), -1);

            }

        }
       
    }

    //Inovekようだったが使わん（アニメーションで使用）
    //void null_active()
    //{
    //    Button.SetActive(false);
    //}

    //他スクリプトからいじるようかんすう
    public void Set_Active(bool set)
    {
        now_ani = false;
        PushButton(set);
    }

    //初期位置に戻らす
    public void Set_Back()
    {
        this.gameObject.transform.position = new Vector3(first_x, pos.y, pos.z);
        this.tag = "Untagged";
    }

}
