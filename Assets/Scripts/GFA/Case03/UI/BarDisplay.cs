using GFA.Case03.Mediators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarDisplay : MonoBehaviour
{
    [SerializeField] private PlayerMediator playerMediator;
    [SerializeField] private Image _barImage;
    private void OnEnable()
    {
        GameSession.Instance.HealthChanged += OnChangedHealth;
    }
    private void OnDisable()
    {
        GameSession.Instance.HealthChanged -= OnChangedHealth;
    }

    private void OnChangedHealth(float val)
    {

        UpdateHealthDisplay(val);
    }

    private void UpdateHealthDisplay(float val)
    {
        _barImage.fillAmount = GameSession.Instance.Health;
        //_moneyText.text = $"+{GameSession.Instance.Money}";
    }
    private void UpdateHealthDisplay()
    {
        UpdateHealthDisplay(GameSession.Instance.Health);
    }
}
