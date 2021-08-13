using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private bool _jumpPermission;
    private float _oldTransformPositionY;
    private Animator _animator;
    private bool _pause = false;

    [SerializeField] private Player _player;
    [SerializeField] private Target _target;
    [SerializeField] private float _force;
    [SerializeField] private float _minJumpForceY;
    [SerializeField] private float _maxJumpForceY;
    [SerializeField] private float _rateOfChangeJumpForce;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _slider.minValue = _minJumpForceY;
        _slider.maxValue = _maxJumpForceY;
        _oldTransformPositionY = transform.position.y;
    }

    private void OnEnable()
    {
        _player.IsDie += StartDeathAnimation;
    }

    private void OnDisable()
    {
        _player.IsDie -= StartDeathAnimation;
    }

    private void StartDeathAnimation()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _animator.SetTrigger("die");
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
            _slider.value += Time.deltaTime * _rateOfChangeJumpForce;
        }
        if(Input.GetKeyUp(KeyCode.Space) && _jumpPermission == true)
        {
            _jumpPermission = false;
            _rigidbody.AddForce(new Vector2(targetDirection.x * _force, _slider.value * _force), ForceMode2D.Impulse);
            _animator.SetBool("onGround", false);
            _slider.value = _slider.minValue;
        }
        if(_jumpPermission == false)
        {
            if (_oldTransformPositionY > transform.position.y)
                _animator.SetTrigger("flyingDown");
            _oldTransformPositionY = transform.position.y;
        }
    }

    public void DisableMover(bool isPause)
    {
        _pause = isPause;
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
            _animator.SetBool("onGround",true);
            _jumpPermission = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _) && _jumpPermission == true)
        {
            _animator.SetTrigger("fall");
            _animator.SetBool("onGround", false);
            _jumpPermission = false;
        }
    }
}


