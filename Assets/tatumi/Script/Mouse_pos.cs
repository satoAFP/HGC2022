using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_pos : MonoBehaviour
{
    [Header("�e�I�u�W�F�N�g")]
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

            // ���[�J�����W�ł̍��W���擾
            Vector3 localPos = Child_Transform.localPosition;
            localPos.x = -580.0f + mousePos.x + M_x;    // ���[�J�����W����ɂ����Ax���W��1�ɕύX
            localPos.y = -320.0f + mousePos.y + M_y;    // ���[�J�����W����ɂ����Ay���W��1�ɕύX
            localPos.z = 750.0f;    // ���[�J�����W����ɂ����Az���W��1�ɕύX
            Child_Transform.localPosition = localPos; // ���[�J�����W�ł̍��W��ݒ�

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
