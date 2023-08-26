using UnityEngine;

public class PotionSpeed : MonoBehaviour
{
    [SerializeField] private float incSpeed = 3f;
    [SerializeField] private int pointSpeed = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            CanvasManager canvasManager = GameObject.Find("CanvasManager").GetComponent<CanvasManager>();
            if (player != null && canvasManager != null)
            {
                player.IncreaseSpeed(incSpeed);
                canvasManager.AmountTextPotion(pointSpeed);
                Destroy(this.gameObject);
            }
        }
    }
}
