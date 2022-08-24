using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemove : MonoBehaviour
{
    [Header("�ړ��������V�[��������")]
    public string[] SceneName;

    [Header("�X�e�[�W�̐�")]
    public int stage_num;

    [Header("����")]
    public GameObject clown;
    [Header("�~�b�V����")]
    public GameObject mission;

    [Header("����������")]
    public GameObject clown_cracker;
    [Header("�~�b�V����������")]
    public GameObject mission_cracker;

    [Header("clear_manager�����")]
    public Mem_mission Mem_Mission;

    [Header("�N���b�N����SE")]
    public AudioClip pic;

    static public int Scene_num = 0;

    static public bool Clown_OK = false;
    static public bool Mission_OK = false;

    private int clown_time = 0;
    private int mission_time = 0;

    private AudioSource audio;  //�g�p����I�[�f�B�I�\�[�X
    private bool first = true;  //��񂵂����������Ȃ�

    void Start()
    {
        audio = GameObject.Find("SE_manager").GetComponent<AudioSource>();
    }

    void FixedUpdate() 
    {
        //�X�e�[�W�V�[���̎�
        if (SceneManager.GetActiveScene().name != "Title" &&
            SceneManager.GetActiveScene().name != "StageSelect" &&
            SceneManager.GetActiveScene().name != "Result" &&
            SceneManager.GetActiveScene().name != "LastResult") 
        {
            //�~�b�V�������N���A����Ă��邩���L��
            if (GameObject.Find("Player").GetComponent<Mission>().Clown_OK == true)
                Clown_OK = true;
            else
                Clown_OK = false;

            if (GameObject.Find("Player").GetComponent<Mission>().Mission_OK == true)
                Mission_OK = true;
            else
                Mission_OK = false;
        }
        
        if (SceneManager.GetActiveScene().name == "Result"||
            SceneManager.GetActiveScene().name == "LastResult")
        {
            clown_time++;
            mission_time++;
            if (Clown_OK == true) 
            {
                if (clown_time >= 90)
                {
                    clown.gameObject.SetActive(true);
                    //�����������Ă��鏈��
                    if (clown.gameObject.GetComponent<RectTransform>().sizeDelta.x > 240.0f)
                    {
                        clown.gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(24.0f, 21.0f);
                    }
                    if(clown.gameObject.GetComponent<RectTransform>().sizeDelta.x == 264.0f)
                    {
                        GameObject a = Instantiate(clown_cracker, GameObject.Find("cracker").transform.position, Quaternion.identity);
                        a.transform.parent = GameObject.Find("cracker").transform;
                    }
                }
            }
            if (Mission_OK == true)
            {
                if(mission_time >= 120)
                {
                    mission.gameObject.SetActive(true);
                    //���������Ă��鏈��
                    if (mission.gameObject.GetComponent<RectTransform>().sizeDelta.x > 240.0f)
                    {
                        mission.gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(24.0f, 24.0f);
                    }
                    if (mission.gameObject.GetComponent<RectTransform>().sizeDelta.x == 264.0f)
                    {
                        GameObject b = Instantiate(mission_cracker, GameObject.Find("cracker2").transform.position, Quaternion.identity);
                        b.transform.parent = GameObject.Find("cracker2").transform;
                    }
                }
            }
        }

        for (int i = 1; i <= stage_num; i++)
        {
            if (SceneManager.GetActiveScene().name == "Stage" + i)
            {
                //���g�̌��݂̃X�e�[�W�L��
                Scene_num = i;

            }
        }

        if (SceneManager.GetActiveScene().name == "Result" ||
            SceneManager.GetActiveScene().name == "LastResult") 
            Mem_Mission.merge();
    }

    //�w�肵���V�[���Ɉړ�
    public void PushScene() 
    {
        Clown_OK = false;
        Mission_OK = false;
        SceneManager.LoadScene(SceneName[0]);
        audio.PlayOneShot(pic);

    }

    //�l�N�X�g�X�e�[�W�{�^��
    public void PushNextScene() 
    {
        Clown_OK = false;
        Mission_OK = false;

        for (int i = 1; i <= stage_num - 1; i++) 
        {
            if (Scene_num == i)
                SceneManager.LoadScene("Stage" + (i + 1));
        }

        if (Scene_num == stage_num)
            SceneManager.LoadScene("Title");
        audio.PlayOneShot(pic);

    }

    //���g���C�{�^��
    public void PushRetryScene()
    {
        Clown_OK = false;
        Mission_OK = false;

        for (int i = 1; i <= stage_num; i++)
        {
            if (Scene_num == i)
                SceneManager.LoadScene("Stage" + i);
        }
        audio.PlayOneShot(pic);

    }

    public void PushNowScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audio.PlayOneShot(pic);

    }
}
