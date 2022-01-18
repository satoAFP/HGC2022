using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpText : MonoBehaviour
{
    public int Helpmode;//いらん？

    public Text text;

    public GameObject[] yazirusi = new GameObject[2];
    public GameObject oya;
    public GameObject PL;
    public GameObject Card;
    public GameObject CardChoice;
    public GameObject BackButton;
    public GameObject ActionButton;

    Player SC_player;
    DeletAction SC_Back;
    ActionButton_SC SC_action; //参照元Scriptが入る変数

    public int nowtext;

    [Header("表示文章順（一行16文字17文字ごとに改行）")]
    public string[] chars;
    [Header("文章の切り替え終わりの数を指示")]
    public int[] Endchar;

    

    // Start is called before the first frame update
    void Start()
    {
        SC_player = PL.GetComponent<Player>();
        SC_Back = BackButton.GetComponent<DeletAction>();
        SC_action = ActionButton.GetComponent<ActionButton_SC>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Helpmode>0)
        text.text = chars[nowtext+1];
       else
            text.text = chars[nowtext];



        if (Input.GetMouseButtonDown(0))
        {
            nowtext++;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            nowtext--;
        }

        if (nowtext < 0)
            nowtext = 0;

       

        if(Helpmode==0)
        {
            if (nowtext > Endchar[0])
                nowtext = Endchar[0];

            if (nowtext == 2)
            {
                if (Input.GetKey(KeyCode.Return))
                {
                   
                    oya.SetActive(false);
                    SC_player.Movestop = false;
                    
                }
            }
        }
        else if(Helpmode==1)
        {
            if (nowtext < Endchar[0])
                nowtext = Endchar[0];
            else if (nowtext >= Endchar[0] && nowtext <= Endchar[1])
            {
                Card.SetActive(true);
                if (nowtext == 3)
                {
                   
                    yazirusi[0].SetActive(true);
                }
            }
           
            if (nowtext > Endchar[1]-1)
                nowtext = Endchar[1]-1;

            if(SC_Back.objs[0].name.Contains("jump")==true && nowtext == Endchar[1]-1)
            {
                oya.SetActive(false);
                SC_player.Movestop = false;
               
                Card.SetActive(false);
                yazirusi[0].SetActive(false);

                SC_Back.now--;
            }
        }
        else if (Helpmode == 2)
        {
            if (nowtext < Endchar[1])
                nowtext = Endchar[1];
            else if (nowtext >= Endchar[1] && nowtext <= Endchar[2])
            {
                if (nowtext == 5)
                {
                    CardChoice.SetActive(true);
                  
                    yazirusi[1].SetActive(true);
                }
            }

            if (nowtext > Endchar[Helpmode]-1)
                nowtext = Endchar[Helpmode]-1;

            if (SC_Back.objs[0].name.Contains("squat") == true && nowtext == Endchar[Helpmode]-1)
            {
                oya.SetActive(false);
                SC_player.Movestop = false;

                CardChoice.SetActive(false);
                yazirusi[1].SetActive(false);

            }
        }
        else if (Helpmode == 3)
        {
            if (nowtext < Endchar[2])
                nowtext = Endchar[2];
            else if (yazirusi[1].activeSelf == true)
            {
                if (nowtext < Endchar[2]+1)
                    nowtext = Endchar[2] + 1;
            }

            if (nowtext >= 7 && nowtext <=10)
            {
               Card.SetActive(true);

               for (int i = 0; i != 2; i++)
               {
                  yazirusi[i].SetActive(true);
               }

            }
            

            if (nowtext > Endchar[Helpmode] - 1)
                nowtext = Endchar[Helpmode] - 1;

            if (SC_Back.multi_objs[0].name.Contains("multi_highjump(Clone)")==true)
            {
               
                yazirusi[0].SetActive(false);
                

                yazirusi[1].gameObject.transform.position = new Vector3(23.0f, 299.6f, -102.0f);
            }

            if (SC_action.multi_des[0].name.Contains("multi_highjump(Clone)") == true)
            {
                oya.SetActive(false);
                SC_player.Movestop = false;

                Card.SetActive(false);
                for (int i = 0; i != 2; i++)
                {
                    yazirusi[i].SetActive(false);
                }

            }
        }
        else if (Helpmode == 4)
        {
            if (nowtext > Endchar[3])
                nowtext = Endchar[3];

            if (nowtext > Endchar[Helpmode] - 1)
                nowtext = Endchar[Helpmode] - 1;

            if (nowtext == Endchar[Helpmode] - 1)
            {
                if (Input.GetKey(KeyCode.Return))
                {

                    oya.SetActive(false);
                    SC_player.Movestop = false;

                }
            }
        }


    }

    public void SetHelp()
    {
        oya.SetActive(true);
    }
}
