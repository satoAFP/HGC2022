using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade_SceneMove : MonoBehaviour
{
    [Header("ここから下はいじらない")]
    public bool goal_move = false;              //ゴール時のアクション再生

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goal_move)
        {
            this.gameObject.GetComponent<Image>().color += new Color32(0, 0, 0, 1);Debug.Log("" + this.gameObject.GetComponent<Image>().color.a);
            if (this.gameObject.GetComponent<Image>().color.a >= 1.5f)
            {
                SceneManager.LoadScene("Result");
            }
        }
        else
        {
            this.gameObject.GetComponent<Image>().color -= new Color32(0, 0, 0, 1);
            if (this.gameObject.GetComponent<Image>().color.a <= 0) 
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
