using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_pos_UI : MonoBehaviour
{
    [Header("�{�^���Ƀ}�E�X�悹���Ƃ���SE")]
    public AudioClip se_on_button;

    private Vector3 mp;         //�}�E�X�̃|�W�V����
    private Vector3 pos;        //���̃I�u�W�F�N�g�̃T�C�Y
    private Vector2 size;       //���̃I�u�W�F�N�g�̃T�C�Y

    private bool one_SE = true; //�{�^���ɏ�������ASE����񂵂��Ȃ�Ȃ�
    private AudioSource audio;  //�g�p����I�[�f�B�I�\�[�X

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�W���ɂ���SE_manager�i�[
        audio = GameObject.Find("SE_manager").GetComponent<AudioSource>();

        pos = this.gameObject.GetComponent<RectTransform>().position;
        size = this.gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X�̃|�W�V�����X�V
        mp = Input.mousePosition;


        if ((mp.x >= pos.x - (size.x / 2)) && (mp.x <= pos.x + (size.x / 2)) &&
            (mp.y >= pos.y - (size.y / 2)) && (mp.y <= pos.y + (size.y / 2)))
        {
            if (one_SE)
            {
                //SE�𗬂�
                audio.PlayOneShot(se_on_button);
                one_SE = false;
            }
        }
        else
        {
            one_SE = true;
        }

    }
}
