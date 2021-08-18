using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMover : MonoBehaviour
{
    public event UnityAction Jumped;
    public event UnityAction Fall;
    public event UnityAction Landing;

    [SerializeField] private Player _player;
    [SerializeField] private Target _target;
    [SerializeField] private float _force;
    [SerializeField] private float _minJumpForceY;
    [SerializeField] private float _maxJumpForceY;
    [SerializeField] private float _rateOfChangeJumpForce;
    [SerializeField] private Slider _forceJumpSlider;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private bool _jumpPermission;
    private bool _pause = false;

    public bool JumpPermission => _jumpPermission;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _forceJumpSlider.minValue = _minJumpForceY;
        _forceJumpSlider.maxValue = _maxJumpForceY;
    }

    private void OnEnable()
    {
        _player.Died += DisableGravity;
    }

    private void OnDisable()
    {
        _player.Died -= DisableGravity;
    }

    private void DisableGravity()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void Update()
    {
        if (_pause)
            return;
        Vector2 targetDirection = transform.TransformDirection(_target.transform.localPosition.normalized);
        if (targetDirection.x < 0)
            _sprite.flipX = false;
        else if (targetDirection.x > 0)
            _sprite.flipX = true;
        if(Input.GetKey(KeyCode.Space) && _jumpPermission == true)
        {
            _forceJumpSlider.value += Time.deltaTime * _rateOfChangeJumpForce;
        }
        if(Input.GetKeyUp(KeyCode.Space) && _jumpPermission == true)
        {
            _jumpPermission = false;
            _rigidbody.AddForce(new Vector2(targetDirection.x * _force, _forceJumpSlider.value * _force), ForceMode2D.Impulse);
            Jumped?.Invoke();
            _forceJumpSlider.value = _forceJumpSlider.minValue;
        }
    }

    public void DisableMover()
    {
        _pause = true;
    }

    public void EnableMover()
    {
        _pause = false;
    }

    private void DisableInertia()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
        {
            DisableInertia();
            Landing?.Invoke();
            _jumpPermission = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _) && _jumpPermission == true)
        {
            Fall?.Invoke();
            _jumpPermission = false;
        }
    }
}


