using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_text : MonoBehaviour
{
    GameObject ActionButton; //参照元OBJそのものが入る変数

    ActionButton_SC script; //参照元Scriptが入る変数

    public int score_number;

    private int score;

    private Text targetText;

    // Start is called before the first frame update
    void Start()
    {
        ActionButton = GameObject.Find("ActionBotton "); //ActionButtonをオブジェクトの名前から取得して変数に格納する
        script = ActionButton.GetComponent<ActionButton_SC>(); //OBJの中にあるScriptを取得して変数に格納する
    }

    // Update is called once per frame
    void Update()
    {

        score = script.get_score(score_number);
        this.targetText = this.GetComponent<Text>(); 
        this.targetText.text = score.ToString(); 
    }
}
