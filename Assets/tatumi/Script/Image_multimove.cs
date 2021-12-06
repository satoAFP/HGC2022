using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_multimove : MonoBehaviour
{
    private int time;
    private Vector3 pos, sca;
    private float MAX, add;

    public bool Move_on;

    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.position;
        time = 0;

        MAX = 510.0f - pos.x;

        add = MAX / 1000 + (MAX / 10000);
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.transform.position;
        sca = this.transform.localScale;

        if (Move_on == true)
        {
            time++;
            if (time < 100)
            {
                //x=10%
                this.transform.position = new Vector3(pos.x + add, pos.y + 3.5f, pos.z);
            }
            else if (time < 300)
            {
                //x=90%
                this.transform.position = new Vector3(pos.x + add * 4, pos.y + 0.3f, pos.z);
            }

            if (time < 300)
            {
                this.transform.localScale = new Vector3(sca.x - 0.003f, sca.y - 0.003f, sca.z);
            }

            if (time == 300)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
