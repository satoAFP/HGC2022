using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class ActionButton_SC : MonoBehaviour
{
    [Header("このシーン以外は表示しない")]
    [Header("特定しか存在しない-------------------------")]
    public string SceneName;

    [Header("必ず自身を指定")]
    public GameObject Object;

    [Header("触らない")]
    public string NowStage;

    [Header("子の要素数")]
    [Header("複数枚表示---------------------------------")]
    public int Child_num;

    [Header("複製数指定")]
    public int[] Duplicate;

    [Header("複製Obj指定")]
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
