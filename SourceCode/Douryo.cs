using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    private int counter;
    [SerializeField]
    private int counterTime;
    [SerializeField]
    private float move = 0.1f;

    void Update()
    {
        transform.Translate(new Vector3(0, 0, move));
        counter++;
        if (counter >=counterTime)
        {
            counter = 0;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}