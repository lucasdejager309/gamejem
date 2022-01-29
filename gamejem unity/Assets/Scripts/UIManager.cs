using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Slider shieldSlider;
    Slider healthSlider;

    PlayerShipHealth playerHealth;

    void GetUIElements() {
        shieldSlider = GameObject.FindGameObjectWithTag("PlayerShieldSlider").GetComponent<Slider>();
        shieldSlider.maxValue = playerHealth.maxShield;
        healthSlider = GameObject.FindGameObjectWithTag("PlayerHealthSlider").GetComponent<Slider>();
        healthSlider.maxValue = playerHealth.maxHealth;
    }

    void Update() {
        if (playerHealth == null) {
            playerHealth = Gamemanager.Instance.player.GetComponent<PlayerShipHealth>();
        }
        GetUIElements();
        shieldSlider.value = playerHealth.Shield;
        healthSlider.value = playerHealth.Health;
    }
}
