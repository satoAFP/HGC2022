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

    // Start is called before the first frame update
    void Start()
    {
        PA = transform.parent.gameObject;
        tmp = PA.gameObject.transform.position;
        pos = this.transform.position;
        time = 0;

        //現在地からどれだけの移動幅か求める
        MAX = 510.0f - tmp.x - 51.0f;

        //移動幅から移動量を求める
        add = MAX / 1000 + (MAX / 10000);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //大きさ、位置を取得
        pos = this.transform.position;
        sca = this.transform.localScale;

        if (Move_on == true)
        {
            time++;
            if (time < 25)
            {
                //x=10%,y=60%まで
                this.transform.position = new Vector3(pos.x + add, pos.y + 10.0f, pos.z);
            }
            else if (time < 75)
            {
                //x=100%,y=100%まで
                this.transform.position = new Vector3(pos.x + add * 16, pos.y + 0.8f, pos.z);
            }

            if (time < 75)
            {
                //大きさを常一定に減らす
                this.transform.localScale = new Vector3(sca.x - 0.012f, sca.y - 0.012f, sca.z);
            }

            if (time == 75)
            {
                //最後にきえろぉー
                Destroy(this.gameObject);
            }
        }
    }
}
