using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float RotateSpeed { get => _rotateSpeed; set => _rotateSpeed = value;}
    public float MovePower { get => _movePower; set => _movePower = value; }
    public float CanMoveTimer { get => _canMoveTimer; set => _canMoveTimer = value; }

    [Header("ボールの回転スピード")]
    [SerializeField] private float _rotateSpeed;
    [Header("プレイヤーが移動する力")]
    [SerializeField] private float _movePower;
    [Header("移動できる時間")]
    [SerializeField] private float _canMoveTimer;
}
