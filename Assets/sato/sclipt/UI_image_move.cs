using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_image_move : MonoBehaviour
{
    [SerializeField] private bool MoveCheck = true;


    private Vector3 UseSize = new Vector3(0, 0, 0);

    

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
            UseSize.x += 0.01f;
            UseSize.y += 0.01f;

            gameObject.GetComponent<RectTransform>().localScale = UseSize;
        }
    }
}
