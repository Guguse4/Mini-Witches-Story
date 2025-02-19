using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private GameObject _teleportZonePrefab;

    private PlayerControls _actions;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Animator _animator;
    private bool _isFacingRight = true;
    private bool _isChargingAction = false;
    private GameObject _currentTeleportZone;
    
    private void Awake()
    {
        _actions = new PlayerControls();
        _actions.Enable();
        _actions.Actions.ChargedAction.started += ChargedAction_started;
        _actions.Actions.ChargedAction.canceled += ChargedAction_canceled;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        if(!_isChargingAction)
            Move();
    }

    private void GatherInput()
    {
        _movement = _actions.Actions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        _rb.linearVelocity = _movement * _speed;
        _animator.SetFloat("Speed", _movement.magnitude);
        if ((_movement.x < 0 && _isFacingRight) || (_movement.x > 0 && !_isFacingRight))
            FlipCharacter();
    }

    private void FlipCharacter()
    {
        GetComponent<SpriteRenderer>().flipX = _isFacingRight;
        _isFacingRight = !_isFacingRight;
    }

    private void ChargedAction_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isChargingAction = true;
        _animator.SetTrigger("StartCharge");
        _animator.SetBool("Charge", true);
        _currentTeleportZone = Instantiate(_teleportZonePrefab, transform.position, Quaternion.identity, transform);
    }
    
    private void ChargedAction_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isChargingAction = false;
        _animator.SetBool("Charge", false);
        transform.position = _currentTeleportZone.transform.position;
        Destroy(_currentTeleportZone);
    }
}
