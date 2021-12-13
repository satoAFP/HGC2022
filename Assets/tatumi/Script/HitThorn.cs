using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitThorn : MonoBehaviour
{
    [Header("‹­‚³")]
    public float power;

    //—Í
    Vector3 chikara = new Vector3(20.0f, 20.0f, 20.0f);

    //‘Šè‚ÌƒŠƒWƒbƒh‚ğŠi”[
    Rigidbody aiteRigid;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        //PL‚Æ“–‚½‚é‚Æ“®ì
        if (collision.gameObject.tag == "Player")
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            Debug.Log("thornHit(up)!");
        }
    }
}
