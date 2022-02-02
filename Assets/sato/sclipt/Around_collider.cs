using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Around_collider : MonoBehaviour
{
    public bool wall_check = false;          //主人公の周りで判定取る用

    public bool delete_arrow = false;          //主人公の周りで判定取る用

    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update() 
    {

        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Jumpblock")
        {
            wall_check = true;

            if (GameObject.Find("Player").GetComponent<Player>().Longjump_check == true)
            {
                Debug.Log("aaa");
                Destroy(collider.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.tag == "Wall")
        {
            wall_check = true;
        }
        if (collider.tag == "Jumpblock")
        {
            wall_check = true;

            if (GameObject.Find("Player").GetComponent<Player>().Longjump_check == true)
            {
                Destroy(collider.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.tag == "Wall") 
        {
            wall_check = false;
            GameObject.Find("Player").GetComponent<Player>().Longjump_check = false;
        }
    }
}
