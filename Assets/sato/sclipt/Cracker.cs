using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cracker : MonoBehaviour
{
    [Header("横に動く力設定")]
    public int move_min_x;
    public int move_max_x;
    [Header("上に動く力設定")]
    public int move_min_y;
    public int move_max_y;

    [Header("紙吹雪減速")]
    public int move_slow;

    [Header("紙吹雪か止まるまでの時間(frame)")]
    public int move_time;

    private Vector3 power;          //紙吹雪に加算されるベクトル
    private Vector3 origin_power;   //加算されるベクトルの初期値


    // Start is called before the first frame update
    void Start()
    {
        origin_power = new Vector3((float)Random.Range(move_min_x, move_max_x) / move_slow, (float)Random.Range(move_min_y, move_max_y) / move_slow, 0);
        power = origin_power;
        this.GetComponent<Image>().color = new Color((float)Random.Range(1, 100) / 100, (float)Random.Range(1, 100) / 100, (float)Random.Range(1, 100) / 100, 255);
        this.gameObject.GetComponent<RectTransform>().Rotate(0.0f, 0.0f, Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        power.x -= (origin_power.x / move_time);
        power.y -= (origin_power.y / move_time);
        this.gameObject.GetComponent<RectTransform>().position += power;

        if (power.y <= 0) 
        {
            Destroy(this.gameObject);
        }
    }
}
