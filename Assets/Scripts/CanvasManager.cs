using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Text textPotion;
    [SerializeField] private int amountPotion;

    [SerializeField] private Text textLife;
    [SerializeField] private GameObject life;

    [SerializeField] private Text textZombies;
    [SerializeField] private int amountZombies;

    [SerializeField] private GameObject textDie;

    [SerializeField] public Slider sliderOrbs;
    [SerializeField] private TextMeshProUGUI tmpScore;
    [SerializeField] public static float amountSliderOrbs;

    private void Awake()
    {
        textPotion = GameObject.Find("TextPotion").GetComponent<Text>();

        textLife = GameObject.Find("TextLife").GetComponent<Text>();

        textZombies = GameObject.Find("TextZombies").GetComponent<Text>();

        tmpScore.text = "[" + sliderOrbs.value.ToString() + " / " + sliderOrbs.maxValue.ToString() + "]";

    }    
    void Update()
    {
        textPotion.text = amountPotion.ToString();

        Life life = GameObject.Find("Player").gameObject.GetComponent<Life>();
        if (life != null)
        {
            textLife.text = life.getCurrentLife().ToString();
            if (life.getCurrentLife() <= 0)
            {
                textDie.SetActive(true);
            }
        }

        // Obtener la cantidad inicial de zombies en la escena
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        textZombies.text = zombies.Length.ToString();

        if (sliderOrbs.value == sliderOrbs.maxValue) { 

            if (amountSliderOrbs > sliderOrbs.maxValue)
            {
                float cacheOrbs = amountSliderOrbs - sliderOrbs.maxValue;
                amountSliderOrbs = cacheOrbs;
            }

            sliderOrbs.maxValue = sliderOrbs.maxValue + (sliderOrbs.maxValue * .5f);
        }
        else
        {
            sliderOrbs.value = amountSliderOrbs;
            if (tmpScore != null && sliderOrbs.maxValue < 2000)
            {
                tmpScore.text = "[" + sliderOrbs.value.ToString() + " / " + sliderOrbs.maxValue.ToString() + "]";
            }
        }
    }

    public void AmountTextPotion(int cantidad)
    {
        amountPotion += cantidad;
    }

}
