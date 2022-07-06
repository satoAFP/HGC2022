using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_move : MonoBehaviour
{
    //エフェクトの種類+場所管理用
    //0-1 爆発 0-左 1-右
    //2-3 集中 2-左 3-右
    public int Efeect_number;

    //最初に発動するか判別＆今の合体カード枚数判断用
    public bool first_EF,now_onecard;

    //パーティかるシステム取得
    private ParticleSystem ps;

    //虹色取得(GameObj側でセット)
    public Gradient grad;

    // Start is called before the first frame update
    void Start()
    {
        //取得
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
       //集中線消去
        if (this.gameObject.activeSelf==true&& Efeect_number<2&& first_EF == true)
        {
            StartCoroutine(Set_Active(false));
            first_EF = false;
        }
        //集中線を白へ変更
        else if (now_onecard == true&& Efeect_number > 1)
        {
            //取得
            var col = ps.colorOverLifetime;
            col.enabled = true;
            //Whiteを作成
            Gradient White = new Gradient();
            //白色セットする（グラデーション含め）
            White.SetKeys(new GradientColorKey[] { }, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
            //代入
            col.color = White;
        }
        //集中線を虹へ変更（合体カードの時）
        else if (now_onecard == false && Efeect_number > 1)
        {
            
            var col = ps.colorOverLifetime;
            col.enabled = true;

            col.color = grad;
        }
    }

    //自身を非表示
    public void SetActive(bool a)
    {
        first_EF = true;
       
        this.gameObject.SetActive(a);
       
    }

    //自身を非表示(2秒遅延)
    public IEnumerator Set_Active(bool b)
    {
        for (int i = 0; i != 1; i++)
        {
            if (i == 0)
                yield return new WaitForSeconds(2.0f);
            else
                //対象の普通アクションを消す
                this.gameObject.SetActive(b);
           
        }
    }
}
