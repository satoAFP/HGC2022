using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�`���[�g���A���p
public class Helpmode : MonoBehaviour
{
    //PL��OBJ��script�擾�itext���j----------
    public GameObject PL;

    Player SC_player;

    public GameObject HelpText;

    HelpText SC_Htext;
    //-----------------------------------------

    //���Ԗڂ�txt���J�E���g�p
    private int nowint=0;

    // Start is called before the first frame update
    void Start()
    {
        //���g�������
        SC_player = PL.GetComponent<Player>();
        SC_Htext = HelpText.GetComponent<HelpText>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        //PL�Ɠ�����Ɠ���
        if (collision.gameObject.tag == "Thorn")
        {
            //�w���v�̕������o����PL�̓������~������
            SC_player.Movestop = true;
           
            SC_Htext.Helpmode = nowint;

            SC_Htext.SetHelp();
            nowint++;
        }
    }

   
}
