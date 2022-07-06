using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_move : MonoBehaviour
{
    //�G�t�F�N�g�̎��+�ꏊ�Ǘ��p
    //0-1 ���� 0-�� 1-�E
    //2-3 �W�� 2-�� 3-�E
    public int Efeect_number;

    //�ŏ��ɔ������邩���ʁ����̍��̃J�[�h�������f�p
    public bool first_EF,now_onecard;

    //�p�[�e�B����V�X�e���擾
    private ParticleSystem ps;

    //���F�擾(GameObj���ŃZ�b�g)
    public Gradient grad;

    // Start is called before the first frame update
    void Start()
    {
        //�擾
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
       //�W��������
        if (this.gameObject.activeSelf==true&& Efeect_number<2&& first_EF == true)
        {
            StartCoroutine(Set_Active(false));
            first_EF = false;
        }
        //�W�����𔒂֕ύX
        else if (now_onecard == true&& Efeect_number > 1)
        {
            //�擾
            var col = ps.colorOverLifetime;
            col.enabled = true;
            //White���쐬
            Gradient White = new Gradient();
            //���F�Z�b�g����i�O���f�[�V�����܂߁j
            White.SetKeys(new GradientColorKey[] { }, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
            //���
            col.color = White;
        }
        //�W��������֕ύX�i���̃J�[�h�̎��j
        else if (now_onecard == false && Efeect_number > 1)
        {
            
            var col = ps.colorOverLifetime;
            col.enabled = true;

            col.color = grad;
        }
    }

    //���g���\��
    public void SetActive(bool a)
    {
        first_EF = true;
       
        this.gameObject.SetActive(a);
       
    }

    //���g���\��(2�b�x��)
    public IEnumerator Set_Active(bool b)
    {
        for (int i = 0; i != 1; i++)
        {
            if (i == 0)
                yield return new WaitForSeconds(2.0f);
            else
                //�Ώۂ̕��ʃA�N�V����������
                this.gameObject.SetActive(b);
           
        }
    }
}
