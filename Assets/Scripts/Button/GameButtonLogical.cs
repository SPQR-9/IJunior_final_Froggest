using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
public class GameButtonLogical : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    private Animator _animator = null;
    private Button _button;

    [SerializeField] private bool _isEventAfterSecondExist;
    [SerializeField] private bool _isDisableAfterActivation;
    [SerializeField] private float _timeOfEventActivation = 1f;
    [SerializeField] private UnityEvent _onClickAfterAWhile;

    public event UnityAction OnButtonClick;
    public event UnityAction OnButtonDisable;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
    }

    private void OnDisable()
    {
        OnButtonDisable?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_button.IsInteractable())
            _animator.SetBool("OnButtonPoint", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_button.IsInteractable())
            _animator.SetBool("OnButtonPoint", false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_button.IsInteractable())
        {
            
            if(_isEventAfterSecondExist)
                StartCoroutine(ActivateAfterAWhile());
            _animator.SetTrigger("OnButtonClick");
            OnButtonClick?.Invoke();
        }
    }

    public void Deactivate()
    {
        _animator.SetTrigger("Deactivate");
    }

    private IEnumerator ActivateAfterAWhile()
    {
        yield return new WaitForSeconds(_timeOfEventActivation);
        if (_isDisableAfterActivation)
            _animator.SetBool("Disable", true);
        _onClickAfterAWhile?.Invoke();
    }
}
