using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Slider shieldSlider;
    Slider healthSlider;

    PlayerShipHealth playerHealth;

    void Start() {
        playerHealth = Gamemanager.Instance.player.GetComponent<PlayerShipHealth>();
        GetUIElements();
        shieldSlider.value = playerHealth.Shield;
        healthSlider.value = playerHealth.Health;
    }

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

        //do lerp als tijd over anders fuck die shit

        //shieldSlider.value = Mathf.Lerp(shieldSlider.value, playerHealth.Shield, Time.deltaTime);
        //healthSlider.value = Mathf.Lerp(healthSlider.value, playerHealth.Health, Time.deltaTime); 
    
        shieldSlider.value = playerHealth.Shield;
        healthSlider.value = playerHealth.Health;
    }
}
