using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Physics")]
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _moveSpeed = 3.5f;

    private Rigidbody _rb;

    private Vector2 _moveInputs = new();
    private Vector3 _moveDir = new();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        _moveInputs.x = Input.GetAxis("Horizontal");
        _moveInputs.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(_jumpKey))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (_moveInputs.sqrMagnitude != 0.0f)
        {
            Movement(_moveInputs);
        }
    }

    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void Movement(Vector2 input)
    {
        _moveDir = (transform.right * input.x + transform.forward * input.y).normalized;

        _rb.MovePosition(transform.position + _moveDir * _moveSpeed * Time.fixedDeltaTime);
    }
}
