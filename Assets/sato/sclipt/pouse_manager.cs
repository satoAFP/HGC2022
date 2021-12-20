using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pouse_manager : MonoBehaviour
{
    [Header("çƒê∂É{É^Éì")]
    public GameObject start;

    [Header("í‚é~É{É^Éì")]
    public GameObject stop;

    [Header("îwåiâÊëú")]
    public GameObject back;


    private bool pouse_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pouse()
    {
        if (GameObject.Find("Player").GetComponent<Player>().count_check)
        {
            if (!pouse_flag)
            {
                StartCoroutine(InputCoroutine());
                start.SetActive(true);
                back.SetActive(true);
                stop.SetActive(false);
                //GameObject.Find("Player").GetComponent<Player>().enabled = false;
                //GameObject.Find("Player").GetComponent<Player>().Movestop = true;
                pouse_flag = true;
            }
            else
            {
                start.SetActive(false);
                back.SetActive(false);
                stop.SetActive(true);
                //GameObject.Find("Player").GetComponent<Player>().enabled = true;
                //GameObject.Find("Player").GetComponent<Player>().Movestop = false;
                pouse_flag = false;
            }
        }

    }

    private IEnumerator InputCoroutine()
    {
        yield return new WaitForSeconds(10.0f);

    }
}
