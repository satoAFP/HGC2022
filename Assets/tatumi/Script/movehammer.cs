using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//U‚èŽqŒ^‚É‰ñ“]B”’l‚ðŒÀ“x‚ð’´‚¦‚³‚¹‚ê‚Îˆê•ûŒü‚Ì‚Ý‚Ì‰ñ“]‚à‚Å‚«‚éB
public class movehammer : MonoBehaviour
{
    //ƒCƒ“ƒXƒyƒNƒ^[‚ÅÝ’è----------------------------------
    [Header("U‚è‘¬“x")]
    public float moveSpeed;

    [Header("ŒÀ“xU‚èŠp“x")]
    public float hammerRad;

    [Header("U‚è•ûŒü")]
    public bool back;

    //private•Ï”-----------------------------------------
    [Header("‰ŠúŠp“x[ŒÀ“x‚ð’´‚¦‚È‚¢‚æ‚¤‚É!]")]
    public float nowhammeRad;
    private int z;
    private float x, y;

    // Start is called before the first frame update
    void Start()
    {
        //‰Šú‰»
        x = 0.0f;
        y = 0.0f;
        z = 0;
        transform.Rotate(new Vector3(x, y, nowhammeRad));
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //‰E‰ñ‚èA¶Žü‚è‰ñ“]
        if (back == true)
        {
            nowhammeRad = 1.0f * moveSpeed;
        }
        else
        {
            nowhammeRad = -1.0f * moveSpeed;
        }

       
        //‰ñ“]
        transform.Rotate(new Vector3(x, y, nowhammeRad));

        //‰ñ“]Šp“x‚ð®”‚Ì‚Ý‚É•ÏŠ·
        z = (int)gameObject.transform.localEulerAngles.z;

        //Œ»Ý‚ÌŠp“x‚ð‚à‚Æ‚É‰ñ“]•ûŒü‚ð•ÏX
        if (360 - hammerRad >= z && 350 - hammerRad <= z)
        {
            if (back == true)
            {
                back = false;
            }
            else
            {
                back = true;
            }
        }
        else if (hammerRad <= z && hammerRad + 10 >= z)
        {
            if (back == true)
            {
                back = false;
            }
            else
            {
                back = true;
            }
        }
    }

    public float Getnowrad()
    {
        return nowhammeRad;
    }

}
