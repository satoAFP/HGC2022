using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mem_mission : MonoBehaviour
{
    [Header("�N���A����̉����Ɛ������ꂼ������")]
    public GameObject[] set_clown;
    public GameObject[] set_star;

    public Scenemove Scenemove;

    public static bool[] stage_clown_mem = new bool[20];
    public static bool[] stage_star_mem = new bool[20];

    //���U���g����A���Ă������m�F
    private bool[] check_clown_mem = new bool[20];
    private bool[] check_star_mem = new bool[20];

    public void get_clown(bool a,int b)
    {
        stage_clown_mem[b] = a;
    }

    public void get_star(bool a, int b)
    {
        stage_star_mem[b] = a;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Scenemove.get_Scene_name() == "Result") 
        {
            check_clown_mem = stage_clown_mem;
            check_star_mem = stage_star_mem;
        }

        for (int i=0;i<20;i++)
        {
            if (check_clown_mem[i])
                set_clown[i].SetActive(true);

            if (check_star_mem[i])
                set_star[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
