using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Around_collider : MonoBehaviour
{
    public bool wall_check = false;          //主人公の周りで判定取る用

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerStay(Collider collider) {
        if (collider.tag == "Wall") {
            wall_check = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.tag == "Wall") {
            wall_check = false;
        }
    }
}
