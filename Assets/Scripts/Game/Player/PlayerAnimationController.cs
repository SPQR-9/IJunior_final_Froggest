using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Player _player;
    private PlayerMover _playerMover;
    private float _oldTransformPositionY;

    private const string  _flyingDown = "flyingDown";
    private const string _die = "die";
    private const string _onGround = "onGround";
    private const string _fall = "fall";

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
        _playerMover.Landed += StartLandingAnimation;
        _playerMover.Fall += StartFallAnimation;
        _playerMover.Jumped += StartJumpedAnimation;
    }

    private void OnDisable()
    {
        _player.Died -= StartDeathAnimation;
        _playerMover.Landed -= StartLandingAnimation;
        _playerMover.Fall -= StartFallAnimation;
        _playerMover.Jumped -= StartJumpedAnimation;
    }

    private void Update()
    {
        if (_playerMover.JumpPermission == false)
        {
            if (_oldTransformPositionY > transform.position.y)
                _animator.SetTrigger(_flyingDown);
            _oldTransformPositionY = transform.position.y;
        }
    }

    private void StartDeathAnimation()
    {
        _animator.SetTrigger(_die);
    }

    private void StartLandingAnimation()
    {
        _animator.SetBool(_onGround, true);
    }

    private void StartFallAnimation()
    {
        _animator.SetTrigger(_fall);
        _animator.SetBool(_onGround, false);
    }

    private void StartJumpedAnimation()
    {
        _animator.SetBool(_onGround, false);
    }
}
