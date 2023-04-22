using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealBarControl : MonoBehaviour
{
    [SerializeField] private Health enemyHealth;
    [SerializeField] private Slider healthBarSlider;

    // Update is called once per frame
    void Update()
    {
        ShowHealthBar();
    }
    private void ShowHealthBar(){
        healthBarSlider.value = (float)Math.Round(enemyHealth.currentHealth/10.0f, 1);
    }
}
