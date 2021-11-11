using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi_Action_move : MonoBehaviour
{
    [Header("合体番号")]
    public int action_num;

    GameObject player; //参照元OBJそのものが入る変数

    Player script; //参照元Scriptが入る変数

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player"); //ActionButtonをオブジェクトの名前から取得して変数に格納する
        //script = ActionButton.GetComponent<ActionButton_SC>(); //OBJの中にあるScriptを取得して変数に格納する
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
