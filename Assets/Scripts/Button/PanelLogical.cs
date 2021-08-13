using System.Collections;
using UnityEngine;

public class PanelLogical : MonoBehaviour
{
    private GameButtonLogical[] _childrenButtons;

    private void OnEnable()
    {
        _childrenButtons = GetComponentsInChildren<GameButtonLogical>();
        foreach (var button in _childrenButtons)
        {
            button.OnButtonClick += Deactivate;
        }
    }

    private void OnDisable()
    {
        foreach (var button in _childrenButtons)
        {
            button.OnButtonClick -= Deactivate;
        }
    }

    private void Deactivate()
    {
        StartCoroutine(IsTimeOfChanged());
        foreach (var button in _childrenButtons)
        {
            button.Deactivate();
        }
    }

    private IEnumerator IsTimeOfChanged()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
