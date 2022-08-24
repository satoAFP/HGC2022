using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class touka : MonoBehaviour
{
    [SerializeField, Header("�t�F�[�h���x")] private int FadeTime;


    //���g�̐e�I�u�W�F�N�g
    private GameObject ParentObj;

    //�ύX����}�e���A���̐F���
    private Color32 MaterialColor;

    //��l�����G�ꂽ����
    private bool pHit = false;

    


    // Start is called before the first frame update
    void Start()
    {
        //�e�I�u�W�F�N�g�擾
        ParentObj = transform.parent.gameObject;

        //�}�e���A���擾
        Material material = ParentObj.GetComponent<MeshRenderer>().material;
        //�����_�[���[�h��Fade�ɕύX
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

        //�}�e���A���̃J���[���擾
        MaterialColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //���߂̃t�F�[�h����
        if(pHit)
        {
            if (MaterialColor.a > 255 % FadeTime) 
                MaterialColor.a -= (byte)FadeTime;
        }
        //���l����
        ParentObj.GetComponent<MeshRenderer>().material.color = MaterialColor;
    }


    private void OnTriggerEnter(Collider other)
    {
        //��l���Ƃ̓����蔻��
        if (other.gameObject.tag == "Player")
        {
            pHit = true;
        }
    }

}
