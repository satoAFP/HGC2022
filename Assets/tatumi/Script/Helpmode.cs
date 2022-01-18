using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpmode : MonoBehaviour
{
    //PLÇÃOBJ
    public GameObject PL;

    Player SC_player;

    public GameObject HelpText;

    HelpText SC_Htext;

    private int nowint=0;

    // Start is called before the first frame update
    void Start()
    {
        SC_player = PL.GetComponent<Player>();
        SC_Htext = HelpText.GetComponent<HelpText>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        //PLÇ∆ìñÇΩÇÈÇ∆ìÆçÏ
        if (collision.gameObject.tag == "Thorn")
        {
            SC_player.Movestop = true;
           
            SC_Htext.Helpmode = nowint;

            SC_Htext.SetHelp();
            nowint++;
        }
    }

   
}
