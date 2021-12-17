using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_move : MonoBehaviour
{
    //エフェクトの種類+場所管理用
    //0-1 爆発 0-左 1-右
    //2-3 集中 2-左 3-右
    public int Efeect_number;

    //いらんきがする
    public bool first_EF;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        if (this.gameObject.activeSelf==true&& Efeect_number<2&& first_EF == true)
        {
            Debug.Log("YES");
            StartCoroutine(Set_Active(false));
            first_EF = false;
        }
    }

    public void SetActive(bool a)
    {
        first_EF = true;
       
        this.gameObject.SetActive(a);
       
    }

    public IEnumerator Set_Active(bool b)
    {
        for (int i = 0; i != 3; i++)
        {
            if (i == 0)
                yield return new WaitForSeconds(2.0f);
            else
                //対象の普通アクションを消す
                this.gameObject.SetActive(b);
           
        }
    }
}
