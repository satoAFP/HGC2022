using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movehammer : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ�----------------------------------
    [Header("�U�葬�x")]
    public float moveSpeed;

    [Header("���x�U��p�x")]
    public float hammerRad;

    [Header("�U�����")]
    public bool back;

    //private�ϐ�-----------------------------------------
    [Header("�����p�x[���x�𒴂��Ȃ��悤��!]")]
    public float nowhammeRad;
    private int z;
    private float x, y;

    // Start is called before the first frame update
    void Start()
    {
        x = 0.0f;
        y = 0.0f;
        z = 0;
        transform.Rotate(new Vector3(x, y, nowhammeRad));
        //nowhammeRad = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (back == true)
        {
            nowhammeRad = 1.0f * moveSpeed;
        }
        else
        {
            nowhammeRad = -1.0f * moveSpeed;
        }

        // nowhammeRad = (int)nowhammeRad;

        transform.Rotate(new Vector3(x, y, nowhammeRad));

        z = (int)gameObject.transform.localEulerAngles.z;

        if (360 - hammerRad >= z && 350 - hammerRad <= z)
        {
            if (back == true)
            {
                back = false;
            }
            else
            {
                back = true;
            }
        }
        else if (hammerRad <= z && hammerRad + 10 >= z)
        {
            if (back == true)
            {
                back = false;
            }
            else
            {
                back = true;
            }
        }
    }

    public float Getnowrad()
    {
        return nowhammeRad;
    }

}
