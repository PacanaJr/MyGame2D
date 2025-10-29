using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void Creditos()
    {
        SceneManager.LoadScene(3);
    }

    public void Salir()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
}
