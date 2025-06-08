using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleague : MonoBehaviour
{
    [SerializeField] private float move = 0.1f;
    [SerializeField] private GameObject _colleagueUI;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private bool movingToB = true;

    void Update()
    {
        Transform target = movingToB ? pointB : pointA;
        transform.position = Vector3.MoveTowards(transform.position, target.position, move * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB; // ‰•œ
            transform.Rotate(0, 180, 0);
            _colleagueUI.transform.Rotate(0, 180, 0);
        }
    }
}
