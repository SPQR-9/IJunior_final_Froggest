using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(ButtonActivatorAfterAWhile))]
public class ButtonAnimationController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator _animator;
    private Button _button;
    private ButtonActivatorAfterAWhile _buttonActivatorAfterAWhile;

    private const string _parameterNameOnButtonPoint = "OnButtonPoint";
    private const string _parameterNameOnButtonClick = "OnButtonClick";
    private const string _parameterNameDeactivate = "Deactivate";
    private const string _parameterNameDisable = "Disable";

    private void Awake()
    {
        _buttonActivatorAfterAWhile = GetComponent<ButtonActivatorAfterAWhile>();
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _buttonActivatorAfterAWhile.OnButtonClick += StartClickAnimation;
        _buttonActivatorAfterAWhile.OnButtonDisableAfterAWhile += StartDisableAnimation;
    }

    private void OnDisable()
    {
        _buttonActivatorAfterAWhile.OnButtonClick -= StartClickAnimation;
        _buttonActivatorAfterAWhile.OnButtonDisableAfterAWhile -= StartDisableAnimation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.IsInteractable())
            _animator.SetBool(_parameterNameOnButtonPoint, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_button.IsInteractable())
            _animator.SetBool(_parameterNameOnButtonPoint, false);
    }

    public void Deactivate()
    {
        _animator.SetTrigger(_parameterNameDeactivate);
    }

    private void StartClickAnimation()
    {
        _animator.SetTrigger(_parameterNameOnButtonClick);
    }

    private void StartDisableAnimation()
    {
        _animator.SetBool(_parameterNameDisable, true);
    }
}
