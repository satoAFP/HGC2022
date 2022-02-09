using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mem_mission : MonoBehaviour
{
    [Header("�N���A����̉����Ɛ������ꂼ������")]
    public GameObject[] set_clown;
    public GameObject[] set_star;

    //���ۃN���A���Ă��邩�̔�����Ƃ�
    public static bool[] stage_clown_mem = new bool[20];
    public static bool[] stage_star_mem = new bool[20];

    //���U���g����A���Ă������m�F
    public static bool[] check_clown_mem = new bool[20];
    public static bool[] check_star_mem = new bool[20];

    public void get_clown(bool a,int b)
    {
        stage_clown_mem[b] = a;
    }

    public void get_star(bool a, int b)
    {
        stage_star_mem[b] = a;
    }

    public void merge()
    {
        for (int i = 0; i < 20; i++)
        {
            check_clown_mem[i] = stage_clown_mem[i];
            check_star_mem[i] = stage_star_mem[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
