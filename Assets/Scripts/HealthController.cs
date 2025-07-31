using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private float _maxHealth = 100;
    private float _currentHealth = 100;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private GameController _gameController;
    [SerializeField] private float _damageAmount, _healthAmount;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            TakeDamage(_damageAmount);
        }
        else if (collision.CompareTag("Health"))
        {
            Heal(_healthAmount);
            collision.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount; // Negative amount will increase health
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        if (_currentHealth == 0)
        {
            _gameController.Die();
            _currentHealth = _maxHealth; // Reset health on death
        }
        UpdateHealthBar();
    }

    private void Heal(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _healthBarFill.fillAmount = _currentHealth / _maxHealth;
    }

    // Added method to access current health for reset
    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
}