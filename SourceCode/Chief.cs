using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Chief : MonoBehaviour
{
    [SerializeField] private float move = 0.1f;
    [SerializeField] private GameObject _chiefUI;
    [SerializeField] private Transform[] points;
    private int currentPointIndex = 1;

    void Update()
    {
        if (points.Length == 0) return;

        Transform targetPoint = points[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, move * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length; // ŽŸ‚Ìƒ|ƒCƒ“ƒg‚Ö
            _chiefUI.transform.Rotate(0, 90, 0);
            transform.Rotate(0, -90, 0);
        }
    }
}