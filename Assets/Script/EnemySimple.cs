using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El jugador chocó con un enemigo!");
            // Aquí puedes poner lo que pasa: perder, restar puntos, reiniciar, etc.
            Time.timeScale = 0; // pausa el juego
        }
    }
}
