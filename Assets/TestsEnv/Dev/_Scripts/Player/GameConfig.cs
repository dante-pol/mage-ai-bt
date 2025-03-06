using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{   
    public float RunSpeed => _runSpeed;
    public float MoveSpeed => _moveSpeed;
    public float JumpForce => _jumpForce;
    public float Gravity => _gravity;
    public float MouseSensitivity => _mouseSensivity;
    public float PlayerMaxHealth => _maxHealth;

    [Header("Player Movement")]
    [SerializeField] private float _runSpeed = 10f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;

    [Header("Gravity Settings")]
    [SerializeField] private float _gravity = -15f;

    [Header("Camera Settings")]
    [SerializeField] private float _mouseSensivity = 2f;

    [Header("Player Health")]
    [SerializeField] private float _maxHealth = 10;
    

}