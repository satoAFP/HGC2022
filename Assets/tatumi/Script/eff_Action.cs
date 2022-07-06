using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eff_Action : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Copied()
    {
        //複製
        GameObject copied = Object.Instantiate(this.gameObject) as GameObject;

        //スクリプトも
        eff_Action script = copied.gameObject.GetComponent<eff_Action>();


        //移動させる
        copied.gameObject.transform.position = this.gameObject.transform.position;

        //本体はエラー起こすため少し遅らせて削除
        Destroy(script);
        Destroy(copied, 1.0f);

    }
}
