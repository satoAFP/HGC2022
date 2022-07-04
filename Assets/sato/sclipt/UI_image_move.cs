using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_image_move : MonoBehaviour
{
    [SerializeField] private bool MoveCheck;
    [SerializeField] private int MoveFrame;
    [SerializeField] private float MovePower;


    private Vector3 UseSize = new Vector3(0, 0, 0);
    private bool move = true;
    private float defolt_size = 1;
    private int count = 0;



    // Start is called before the first frame update
    void Start()
    {
        UseSize = gameObject.GetComponent<RectTransform>().localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MoveCheck)
        {
            count++;
            if (count % MoveFrame == 0 && move) 
                move = false;
            else if(count % MoveFrame == 0 && !move)
                move = true;


            if (move)
            {
                UseSize.x += MovePower;
                UseSize.y += MovePower;
            }
            else 
            {
                UseSize.x -= MovePower;
                UseSize.y -= MovePower;
            }

            gameObject.GetComponent<RectTransform>().localScale = UseSize;
        }
    }
}
