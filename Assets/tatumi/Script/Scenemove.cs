using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemove : MonoBehaviour
{
    [Header("�ړ��������V�[��������")]
    public string[] SceneName;

    public void PushButton() {
        SceneManager.LoadScene(SceneName[0]);
    }
}
