using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_text : MonoBehaviour
{
    GameObject ActionButton; //�Q�ƌ�OBJ���̂��̂�����ϐ�

    ActionButton_SC script; //�Q�ƌ�Script������ϐ�

    public int score_number;

    private int score;

    private Text targetText;

    // Start is called before the first frame update
    void Start()
    {
        ActionButton = GameObject.Find("ActionBotton "); //ActionButton���I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        script = ActionButton.GetComponent<ActionButton_SC>(); //OBJ�̒��ɂ���Script���擾���ĕϐ��Ɋi�[����
    }

    // Update is called once per frame
    void Update()
    {

        score = script.get_score(score_number);
        this.targetText = this.GetComponent<Text>(); 
        this.targetText.text = score.ToString(); 
    }
}
