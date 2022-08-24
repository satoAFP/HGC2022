using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class touka : MonoBehaviour
{
    [SerializeField, Header("フェード速度")] private int FadeTime;


    //自身の親オブジェクト
    private GameObject ParentObj;

    //変更するマテリアルの色情報
    private Color32 MaterialColor;

    //主人公が触れた判定
    private bool pHit = false;

    


    // Start is called before the first frame update
    void Start()
    {
        //親オブジェクト取得
        ParentObj = transform.parent.gameObject;

        //マテリアル取得
        Material material = ParentObj.GetComponent<MeshRenderer>().material;
        //レンダーモードをFadeに変更
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

        //マテリアルのカラー情報取得
        MaterialColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //透過のフェード処理
        if(pHit)
        {
            if (MaterialColor.a > 255 % FadeTime) 
                MaterialColor.a -= (byte)FadeTime;
        }
        //数値入力
        ParentObj.GetComponent<MeshRenderer>().material.color = MaterialColor;
    }


    private void OnTriggerEnter(Collider other)
    {
        //主人公との当たり判定
        if (other.gameObject.tag == "Player")
        {
            pHit = true;
        }
    }

}
