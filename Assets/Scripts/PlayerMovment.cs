using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovment : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
  
    void Update()
    {
        // Get input from keyboard or controller
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normalize to prevent faster diagonal movement
        _moveInput = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // Move the player
        _rb.velocity = _moveInput * moveSpeed;
    }
}
