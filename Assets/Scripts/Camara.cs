using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform jugador;

    private void LateUpdate()
    {
        transform.position = new Vector3 (jugador.position.x, jugador.position.y, transform.position.z);      
    }
}
