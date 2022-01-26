using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_pos : MonoBehaviour
{
    [Header("親オブジェクト")]
    public GameObject AC_Button;

    Mouse_pos SC_Child;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;

            float M_x, M_y;

            M_x = (mousePos.x / 7.5f);
            M_y = (mousePos.y / 8.7f);

            GameObject newObj = Instantiate(this.gameObject, AC_Button.transform, true);

            Transform Child_Transform = newObj.transform;

            // ローカル座標での座標を取得
            Vector3 localPos = Child_Transform.localPosition;
            localPos.x = -580.0f + mousePos.x + M_x;    // ローカル座標を基準にした、x座標を1に変更
            localPos.y = -320.0f + mousePos.y + M_y;    // ローカル座標を基準にした、y座標を1に変更
            localPos.z = 750.0f;    // ローカル座標を基準にした、z座標を1に変更
            Child_Transform.localPosition = localPos; // ローカル座標での座標を設定

            //newObj.transform.position = new Vector3(-580.0f + mousePos.x + M_x, -320.0f + mousePos.y + M_y, 60.0f);

            SC_Child = newObj.gameObject.GetComponent<Mouse_pos>();

            Destroy(SC_Child);

            //this.gameObject.transform.position = new Vector3(-580.0f+mousePos.x+M_x, -320.0f+mousePos.y+M_y, 60.0f);

            StartCoroutine(Set_Active(newObj));
        }
    }

    public IEnumerator Set_Active(GameObject a)
    {
        for (int i = 0; i != 2; i++)
        {
            if (i == 0)
                yield return new WaitForSeconds(1.0f);
            else
            {
                Destroy(a.gameObject);
            }

        }
    }
}
