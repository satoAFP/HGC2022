using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_move : MonoBehaviour
{
    //親の状態取得
    private GameObject PA;
    private Vector3 tmp;
    //-------------------------------

    //動き作成に必要--------------
    private int time;
    private Vector3 pos,sca;
    private float MAX,add;
    //----------------------------

    //ここで信号受け渡し＆動きをする。正直関数でいいんじゃね？？？？
    public bool Move_on;
    private float bye;
    //0=-90.6f,1=-64.5,2=-38.5f,3=-12.5f//差26
    public float parent_posx;

    // Start is called before the first frame update
    void Start()
    {
        PA = transform.parent.gameObject;
        tmp = PA.gameObject.transform.position;
        //pos = this.transform.position;
        time = 0;

        this.transform.position = new Vector3(parent_posx,56.23f,-102.0f);

        //現在地からどれだけの移動幅か求める
        MAX = parent_posx;

        //移動幅から移動量を求める
        add = (MAX / 1000 + (MAX / 100));

        //挫折
        bye = 1;

       // Move_on = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //大きさ、位置を取得
        sca = this.transform.localScale;

       

        if (Move_on == true)
        {
            time++;
            if (time < (25/bye))
            {
                //x=10%,y=60%まで
               this.transform.position = new Vector3((parent_posx-(add*(time/(bye*2)))),(56.23f+(-0.9966f*(time/(bye/2)))), -102.0f);
                if (time < (25 / bye))
                {
                    pos = new Vector3((parent_posx - (add * (time / (bye * 2)))), (56.23f + (-0.9966f * (time / (bye / 2)))), -102.0f);
                }
            }
            else if (time < (75/bye))
            {
                //x=100%,y=100%まで
                this.transform.position = new Vector3((pos.x - (add * ((time-24) / (bye / 1.5f)))), (pos.y + (-0.9966f * ((time-24) / (bye * 5.0f)))), pos.z);
            }

            

            if (time < (75/bye))
            {
                //大きさを常一定に減らす
                this.transform.localScale = new Vector3(sca.x - (0.012f*bye), sca.y - (0.012f*bye), sca.z);
            }

            if (time == (75/bye))
            {
                //最後にきえろぉー
                Destroy(this.gameObject);
            }
        }
    }
}
