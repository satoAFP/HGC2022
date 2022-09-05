using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMove : MonoBehaviour
{
    [SerializeField, Header("移動速度")] private float MoveSpeed;
    [SerializeField, Header("最終移動座標")] private float StopPos;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.GetComponent<RectTransform>().localPosition.y <= StopPos)
            gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, MoveSpeed, 0);
    }
}
