using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultuAction_Creit : MonoBehaviour
{
    [Header("必ずActionButtonを指定")]
    public GameObject AC_button;

    //名前一部取得（かかわりあるものはすべて取得,小文字不可？）
    private string target = "G";

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PushButton()
    {
        //pos=800位置にいるやつ全取得
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Multi_action1");

        if (blocks[0] == null)
        {
            ;
        }
        else
        {
            //名前を見る（この辺りは別scriptで判定したほうがよさそう）
            if (blocks[0].name.Contains(target) == true)
            {
                if (blocks[1].name.Contains(target) == true)
                {
                    //ハイジャンプ作成（位置はｘ＝800）
                    // プレハブをGameObject型で取得
                    GameObject obj = (GameObject)Resources.Load("highjump");
                    // プレハブを元に、インスタンスを生成、
                    Instantiate(obj, new Vector3(800.0f, 0.0f, 0.0f), Quaternion.Euler(0, 0, 0), AC_button.transform);

                    for (int i = 0; i != 2; i++)
                    {
                        blocks[i].GetComponent<ButtonChoice>().Set_Active(true);
                    }
                }
            }
        }
        
    }
}
        
    

