using UnityEngine;
using UnityEngine.SceneManagement;

public class Trofeo : MonoBehaviour
{
    [SerializeField] int frutasNecesarias = 14; // <- desbloquea a partir de 14

    private void OnTriggerEnter2D(Collider2D other)
    {
        // solo el jugador lo puede activar
        if (other.GetComponent<Jugador>() == null) return;

        var gm = FindAnyObjectByType<GameManager>();
        if (gm != null && gm.Puntos >= frutasNecesarias)
        {
            // recoge el trofeo y cambia de escena
            Destroy(gameObject);

            int next = SceneManager.GetActiveScene().buildIndex + 1;
            if (next < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(next);
            else
                SceneManager.LoadScene(0);
        }
        // si aún no hay 14, no hace nada
    }
}
