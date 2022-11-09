using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHealthMaximator : MonoBehaviour
{
    private int _minHealth;
    private int _maxHealth;

    public void SetMinMaxHealth(int health)
    {
        if (health < _minHealth)
            _minHealth = health;

        if (health > _maxHealth)
            _maxHealth = health;
    }

    public int[] GetMinMaxHealth()
    {
        return new int[] { _minHealth, _maxHealth };
    }

    public void ResetMinMaxHealth()
    {
        _minHealth = 0;
        _maxHealth = 0;
    }
}
