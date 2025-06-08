using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    [Tooltip("追従させたいターゲット")]
    private GameObject target;
    // カメラとの距離
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}
