using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private string _attackTriggerName = "onAttack";
    [SerializeField] private string _groundBoolName = "isGrounded";
    [SerializeField] private string _jumpTriggerName = "onJump";
    [SerializeField] private string _moveBoolName = "isMoving";
    [SerializeField] private string _xFloatName = "xAxis";
    [SerializeField] private string _zFloatName = "zAxis";

    [Header("Inputs")]
    [SerializeField] private KeyCode _attackKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Physics")]
    [SerializeField] private float _attackDistance = 0.25f;
    [SerializeField] private LayerMask _attackMask;
    [SerializeField] private Transform _attackOrigin;
    [SerializeField] private float _groundDistance = 0.25f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _moveDistance = 0.25f;
    [SerializeField] private LayerMask _moveMask;
    [SerializeField] private float _moveSpeed = 3.5f;

    private bool _isGrounded = true, _isBlocked;

    private Animator _animator;
    private PlayerAvatar _avatar;
    private Rigidbody _rb;

    private Ray _attackRay, _groundRay, _moveRay;
    private RaycastHit _attackHit;

    private Vector2 _moveInputs = new();
    private Vector3 _moveDir = new(), _transformOffset = new();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _avatar = GetComponentInChildren<PlayerAvatar>();
    }

    private void Update()
    {
        _animator.SetLayerWeight(1, _avatar.legsLayerWeight);

        _moveInputs.x = Input.GetAxis("Horizontal");
        _animator.SetFloat(_xFloatName, _moveInputs.x);
        _moveInputs.y = Input.GetAxis("Vertical");
        _animator.SetFloat(_zFloatName, _moveInputs.y);

        _transformOffset = transform.position;
        _transformOffset.y = transform.position.y + 0.05f;

        _isBlocked = IsBlocked(_moveInputs);
        _isGrounded = IsGrounded();

        _animator.SetBool(_groundBoolName, _isGrounded);
        _animator.SetBool(_moveBoolName, _moveInputs.sqrMagnitude != 0.0f);

        if (Input.GetKeyDown(_jumpKey) && _isGrounded)
        {
            _animator.SetTrigger(_jumpTriggerName);
        }

        if (Input.GetKeyDown(_attackKey))
        {
            _animator.SetTrigger(_attackTriggerName);
        }
    }

    private void FixedUpdate()
    {
        if (_moveInputs.sqrMagnitude != 0.0f && !_isBlocked)
        {
            Movement(_moveInputs);
        }
    }

    public void Attack()
    {
        _attackRay = new Ray(_attackOrigin.position, transform.forward);

        if(Physics.Raycast(_attackRay, out _attackHit, _attackDistance, _attackMask))
        {
            if(_attackHit.collider.TryGetComponent(out IDamage damage))
            {
                damage.TakeDamage();
            }
        }
    }

    private bool IsBlocked(Vector2 input)
    {
        _moveRay = new Ray(_transformOffset, (transform.right * input.x + transform.forward * input.y));

        return Physics.Raycast(_moveRay, _moveDistance, _moveMask);
    }

    private bool IsGrounded()
    {
        _groundRay = new Ray(_transformOffset, -transform.up);

        return Physics.Raycast(_groundRay, _groundDistance, _groundMask);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_groundRay.origin, _groundRay.direction * _groundDistance);
        Gizmos.DrawRay(_moveRay.origin, _moveRay.direction * _moveDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_attackRay.origin, _attackRay.direction * _attackDistance);
    }
}
