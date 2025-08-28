using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private string _groundBoolName = "isGrounded";
    [SerializeField] private string _jumpTriggerName = "onJump";
    [SerializeField] private string _xFloatName = "xAxis";
    [SerializeField] private string _zFloatName = "zAxis";

    [Header("Inputs")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Physics")]
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _moveSpeed = 3.5f;

    private bool _isGrounded = true;

    private Animator _animator;
    private Rigidbody _rb;

    private Vector2 _moveInputs = new();
    private Vector3 _moveDir = new();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _moveInputs.x = Input.GetAxis("Horizontal");
        _animator.SetFloat(_xFloatName, _moveInputs.x);
        _moveInputs.y = Input.GetAxis("Vertical");
        _animator.SetFloat(_zFloatName, _moveInputs.y);

        _animator.SetBool(_groundBoolName, _isGrounded);

        if (Input.GetKeyDown(_jumpKey) && _isGrounded)
        {
            _animator.SetTrigger(_jumpTriggerName);
            _isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        if (_moveInputs.sqrMagnitude != 0.0f)
        {
            Movement(_moveInputs);
        }
    }

    public void Jump()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void Movement(Vector2 input)
    {
        _moveDir = (transform.right * input.x + transform.forward * input.y).normalized;

        _rb.MovePosition(transform.position + _moveDir * _moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 30 && !_isGrounded)
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 30 && _isGrounded)
        {
            _isGrounded = false;
        }
    }
}
