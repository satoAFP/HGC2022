using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//カード選択時吸い込まれる演出（Animは位置が動的なため使用不可）
public class Image_move : MonoBehaviour
{
   
    //動き作成に必要--------------
    private int time;
    private Vector3 pos,sca;
    private float MAX,add;
    //----------------------------

    //ここで信号受け渡し＆動きをする。
    public bool Move_on;
    private float bye;
    //0=-90.6f,1=-64.5,2=-38.5f,3=-12.5f//差26
    public float parent_posx;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        this.transform.position = new Vector3(parent_posx,56.23f,-102.0f);

        //現在地からどれだけの移動幅か求める
        MAX = parent_posx;

        //移動幅から移動量を求める
        add = (MAX / 1000 + (MAX / 100));

        //一応速さ変更用のもの。現在は凍結
        bye = 1;

      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //大きさ、位置を取得
        sca = this.transform.localScale;

        this.transform.position = new Vector3(parent_posx, 56.23f, -102.0f);

        if (Move_on == true)
        {
            time++;
            //最初のみ反映
            if (time == 1)
            {
                //色情報取得
                Image image = this.gameObject.GetComponent<Image>();

                //反映(視覚化)
                image.color = new Color(255, 255, 255, 255);
            }

            //移動処理---------------------------------------------------------------------------------------------------------------------------------------
            if (time < (25/bye))
            {
                //x=10%,y=60%まで移動
               this.transform.position = new Vector3((parent_posx-(add*(time/(bye*2)))),(56.23f+(-0.9966f*(time/(bye/2)))), -102.0f);
                if (time < (25 / bye))
                {
                    pos = new Vector3((parent_posx - (add * (time / (bye * 2)))), (56.23f + (-0.9966f * (time / (bye / 2)))), -102.0f);
                }
            }
            else if (time < (75/bye))
            {
                //x=100%,y=100%まで移動
                this.transform.position = new Vector3((pos.x - (add * ((time-24) / (bye / 1.5f)))), (pos.y + (-0.9966f * ((time-24) / (bye * 5.0f)))), pos.z);
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            
            //大きさを一定に減らす
            if (time < (75/bye))
            {
                //大きさを常一定に減らす
                this.transform.localScale = new Vector3(sca.x - (0.012f*bye), sca.y - (0.012f*bye), sca.z);
            }

            //計1.3秒で消える
            if (time == (75/bye))
            {
                //最後にきえろぉー
                Destroy(this.gameObject);
            }
        }
    }
}
