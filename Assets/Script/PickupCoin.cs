using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    public float points = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Moneda recogida!");
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.score += points;
            }
            Destroy(gameObject);
        }
    }
}
