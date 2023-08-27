
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider sliderHealth;

    public void UpdateFloatingHealthBar(float currentHealth, float maxHealth)
    {
        sliderHealth.value = currentHealth / maxHealth;
    }
}
