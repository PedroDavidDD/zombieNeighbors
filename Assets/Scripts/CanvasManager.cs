using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Text textPotion;
    [SerializeField] private int amountPotion;

    private void Awake()
    {
        textPotion = GameObject.Find("TextPotion").GetComponent<Text>();
    }

    void Update()
    {
        textPotion.text = amountPotion.ToString();
    }
    public void AmountTextPotion(int cantidad)
    {
        amountPotion += cantidad;
    }
}
