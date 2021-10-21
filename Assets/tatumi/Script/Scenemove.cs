using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemove : MonoBehaviour
{
    [Header("ˆÚ“®‚µ‚½‚¢ƒV[ƒ“–¼“ü—Í")]
    public string[] SceneName;

    public void PushButton() {
        SceneManager.LoadScene(SceneName[0]);
    }
}
