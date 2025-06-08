using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kacho : MonoBehaviour
{
    [SerializeField]
    private float move = 0.1f;
    private bool zBool = false;
    private bool xBool = false;

    void Update()
    {
        transform.Translate(new Vector3(0, 0, move));

        if (Mathf.Floor(this.transform.position.z) >= 5)
        {
            if (!zBool)
            {
                xBool = true;
                zBool = true;
                transform.Rotate(new Vector3(0, -90, 0));
            }
        }

        if (Mathf.Floor(this.transform.position.x) <= 70)
        {
            if (xBool)
            {
                xBool = false;
                zBool = true;
                transform.Rotate(new Vector3(0, -90, 0));
            }
        }

        if (Mathf.Floor(this.transform.position.z) <= -18)
        {
            if (zBool)
            {
                xBool = false;
                zBool = false;
                transform.Rotate(new Vector3(0, -90, 0));
            }
        }

        if (Mathf.Floor(this.transform.position.x) >= 90)
        {
            if (!xBool)
            {
                xBool = true;
                zBool = false;
                transform.Rotate(new Vector3(0, -90, 0));
            }
        }
    }
}
