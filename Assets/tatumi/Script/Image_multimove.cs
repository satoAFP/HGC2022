using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_multimove : MonoBehaviour
{
    //移動に必要。ついでにカーソル合わせ時の上下移動も兼ねてる
    private GameObject PA;
    private Vector3 pos, sca;
    private float MAX, add;
    private bool Nomal_mode;
    private float image_alp;

    //信号受け取り。だからかんｓ（以下略
    public int time;
    public bool Move_on;
    public float bye;

    // Start is called before the first frame update
    void Start()
    {
        //ぼかしを得る
        PA = transform.Find("Image").gameObject;
        pos = this.transform.position;
        Nomal_mode = true;
        time = 0;
        image_alp = 0;

        //普通と一緒
        MAX = 510.0f - pos.x;

        add = MAX / 1000 + (MAX / 10000);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos = this.transform.position;
        sca = this.transform.localScale;
      
        if (Move_on == true)
        {
            time++;

            //色情報取得
            Image image = PA.GetComponent<Image>();

            //反映
            image.color = new Color(255, 255, 255, 0);
          

            if (time < (25/bye))
            {
                //x=10%
                this.transform.position = new Vector3(pos.x + add, pos.y + (14.0f*bye), pos.z);
            }
            else if (time < (75/bye))
            {
                //x=90%
                this.transform.position = new Vector3(pos.x + add * (16*bye), pos.y + (1.2f*bye), pos.z);
            }

            if (time < (75/bye))
            {
                this.transform.localScale = new Vector3(sca.x - (0.012f*bye), sca.y - (0.012f*bye), sca.z);
            }

            if (time == (75/bye))
            {
                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);
            }
        }
        //上下移動
        else if(Nomal_mode==true)
        {
            
            if (pos.y > -127.0f)
            {
                //下がる
                this.transform.position = new Vector3(pos.x, pos.y - 0.8f, pos.z);
                image_alp -= 0.04f;
            }
            else if (pos.y == -127.0f)
            {
                //Nomal_mode = false;//Nomal解除
                image_alp = 0;
            }

            //色情報取得
            Image image = PA.GetComponent<Image>();

            //反映
            image.color = new Color(255, 255, 255, image_alp);

        }
        else if (Nomal_mode == false)
        {
            
            if (pos.y < -100.0f)
            {
                //上がる
                this.transform.position = new Vector3(pos.x, pos.y + 0.8f, pos.z);
                image_alp += 0.04f;
            }
            else if (pos.y == -100.0f)
            {
                ;//何もなし
                image_alp = 255;
            }

            //色情報取得
            Image image = PA.GetComponent<Image>();

            //反映
            image.color = new Color(255, 255, 255, image_alp);
        }


    }

    //信号でフラグ切り替え----------------
    public void Hidding()
    {
        //pos = this.transform.position;
        Nomal_mode = false;

    }

    public void Nomal()
    {
        Nomal_mode = true;
    }
    //---------------------------------------

}
