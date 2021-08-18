using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Player _player;
    private PlayerMover _playerMover;
    private float _oldTransformPositionY;

    private const string  _parameterNameFlyingDown = "flyingDown";
    private const string _parameterNameDie = "die";
    private const string _parameterNameOnGround = "onGround";
    private const string _parameterNameFall = "fall";

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMover = GetComponent<PlayerMover>();
        _animator = GetComponent<Animator>();
        _oldTransformPositionY = transform.position.y;
    }

    private void OnEnable()
    {
        _player.Died += StartDeathAnimation;
        _playerMover.Landing += StartLandingAnimation;
        _playerMover.Fall += StartFallAnimation;
        _playerMover.Jumped += StartJumpedAnimation;
    }

    private void OnDisable()
    {
        _player.Died -= StartDeathAnimation;
        _playerMover.Landing -= StartLandingAnimation;
        _playerMover.Fall -= StartFallAnimation;
        _playerMover.Jumped -= StartJumpedAnimation;
    }

    private void Update()
    {
        if (_playerMover.JumpPermission == false)
        {
            if (_oldTransformPositionY > transform.position.y)
                _animator.SetTrigger(_parameterNameFlyingDown);
            _oldTransformPositionY = transform.position.y;
        }
    }

    private void StartDeathAnimation()
    {
        _animator.SetTrigger(_parameterNameDie);
    }

    private void StartLandingAnimation()
    {
        _animator.SetBool(_parameterNameOnGround, true);
    }

    private void StartFallAnimation()
    {
        _animator.SetTrigger(_parameterNameFall);
        _animator.SetBool(_parameterNameOnGround, false);
    }

    private void StartJumpedAnimation()
    {
        _animator.SetBool(_parameterNameOnGround, false);
    }
}
