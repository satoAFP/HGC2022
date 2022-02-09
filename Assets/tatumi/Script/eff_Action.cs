using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eff_Action : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Copied()
    {
        
            GameObject copied = Object.Instantiate(this.gameObject) as GameObject;

            eff_Action script = copied.gameObject.GetComponent<eff_Action>();

        copied.gameObject.transform.position = this.gameObject.transform.position;

            Destroy(script);
            Destroy(copied,1.0f);
        
    }
}
