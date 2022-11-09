using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BrickHealth : MonoBehaviour
{
    public delegate void HealthChangedEventHandler(int health, BrickHealthMaximator maximator);
    public event HealthChangedEventHandler HealthChangedEvent;

    public UnityEvent DieEvent;

    [SerializeField] private Text _healthText;
    [SerializeField] private SoundPlayer _soundPlayer;

    [SerializeField] private int _minHealth;
    [SerializeField] private int _maxHealth;

    private BrickHealthMaximator _healthMaximator;

    private int _health;

    private void OnEnable()
    {
        _healthMaximator = FindObjectOfType<BrickHealthMaximator>();
        _soundPlayer = FindObjectOfType<SoundPlayer>();

        _health = Random.Range(_minHealth, _maxHealth);

        ChangeHealth();
    }

    private void Die()
    {
        DieEvent?.Invoke();
        gameObject.SetActive(false);
    }

    public void GetDamage(int damage = 1)
    {
        if(this.enabled)
            _health -= damage;

        if (_health <= 0)
            Die();
        else
        {
            ChangeHealth();
            _soundPlayer.PlaySound();
        }
    }

    public int GetHealth()
    {
        return _health;
    }

    public void SetHealth(int minHealth = 1, int maxHealth = 1)
    {
        _minHealth = minHealth;
        _maxHealth = maxHealth;
    }

    private void ChangeHealth()
    {
        _healthMaximator.SetMinMaxHealth(_health);

        HealthChangedEvent?.Invoke(_health, _healthMaximator);
        _healthText.text = _health.ToString();
    }

}
