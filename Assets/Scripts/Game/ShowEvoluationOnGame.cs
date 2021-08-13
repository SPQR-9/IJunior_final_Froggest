using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShowEvoluationOnGame : MonoBehaviour
{
    private Image _starImage;

    [SerializeField] private List<Sprite> _starSprites;
    [SerializeField] private RatingCounter _ratingCounter;

    private void Awake()
    {
        _starImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        ShowEvaluation(_ratingCounter.GetEvaluation());
    }

    public void ShowEvaluation(int evaluation)
    {
        _starImage.sprite = _starSprites[evaluation];
    }
}
