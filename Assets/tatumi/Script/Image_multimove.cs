using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_multimove : MonoBehaviour
{
    //移動に必要。ついでにカーソル合わせ時の上下移動も兼ねてる
    private GameObject PA;
    public Vector3 pos, sca;
    private float MAX, add;
    public bool Nomal_mode;
    private float image_alp;

    //信号受け取り。だからかんｓ（以下略
    public int time;
    public bool Move_on;
    private float bye;
    

    // Start is called before the first frame update
    void Start()
    {
        //ぼかしを得る
        PA = transform.Find("Image").gameObject;
        pos = this.transform.position;
        this.transform.Rotate(-180.0f, 0.0f, 0.0f);
        Nomal_mode = true;
        time = 0;
        image_alp = 0;

        //普通と一緒
        MAX =pos.x;

        //移動幅から移動量を求める
        add = (MAX / 10000 + (MAX / 1000));

        //挫折
        bye = 1;

        Move_on = false;
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

            if (time < (25 / bye))
            {
                //x=10%,y=60%まで
                this.transform.position = new Vector3((pos.x - (add * (time / (bye * 4)))), (83.19456f + (-0.9966f * (time / (bye / 2)))), -102.0f);
                if (time < (25 / bye))
                {
                    pos = new Vector3((pos.x - (add * (time / (bye * 2)))), (83.19456f + (-0.9966f * (time / (bye / 2)))), -102.0f);
                }
            }
            else if (time < (75 / bye))
            {
                //x=100%,y=100%まで
                this.transform.position = new Vector3((pos.x - (add * ((time - 24) / (bye * 1.5f)))), (pos.y + (-0.9966f * ((time - 24) / (bye * 40.0f)))), pos.z);
            }


            if (time < (75 / bye))
            {
                //大きさを常一定に減らす
                this.transform.localScale = new Vector3(sca.x - (0.012f * bye), sca.y - (0.012f * bye), sca.z);
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
            
            if (pos.y < 83.19456f)
            {
                //下がる
                this.transform.position = new Vector3(pos.x, pos.y + 0.4f, pos.z);
                image_alp -= 0.04f;
            }
            else 
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
            
            if (pos.y > 77.78191f)
            {
                //上がる
                this.transform.position = new Vector3(pos.x, pos.y - 0.4f, pos.z);
                image_alp += 0.04f;
            }
            else 
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
