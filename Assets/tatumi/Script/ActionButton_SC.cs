using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class ActionButton_SC : MonoBehaviour
{
    [Header("���̃V�[���ȊO�͕\�����Ȃ�")]
    [Header("���肵�����݂��Ȃ�-------------------------")]
    public string SceneName;

    [Header("�K�����g���w��")]
    public GameObject Object;

    [Header("�G��Ȃ�")]
    public string NowStage;

    [Header("�q�̗v�f��")]
    [Header("�������\��---------------------------------")]
    public int Child_num;

    [Header("�������w��")]
    public int[] Duplicate;

    [Header("����Obj�w��")]
    public GameObject[] childGameObjects;
  
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        NowStage = SceneManager.GetActiveScene().name;

        for (int i = 0; i != Child_num; i++)
        {
            for (int k = 0; k != Duplicate[i]; k++)
            {
               GameObject newObj = Instantiate(childGameObjects[i], this.transform, false);
            }
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == SceneName)
        {
            ;
        }
        else
            Destroy(Object, .01f);
    }
}
