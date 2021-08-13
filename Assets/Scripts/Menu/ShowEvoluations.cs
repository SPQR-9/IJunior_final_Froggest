using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShowEvoluations : MonoBehaviour
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private MenuSceneManager _menuManager;

    private Image _starImage;

    private void Start()
    {
        _starImage = GetComponent<Image>();
        Sprite spriteStar = _menuManager.ShowEvoluations(_numberLevel);
        _starImage.sprite = spriteStar;
    }
}
