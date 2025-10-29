using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("--- UI ---")]
    public TMP_Text textoPuntos;

    [Header("--- Frutas ---")]
    public Transform grupoComida;

    private int puntos;
    private int totalFrutas;

    public bool TodasFrutasRecogidas => puntos >= totalFrutas;

    void Start()
    {
        if (grupoComida == null)
        {
            GameObject go = GameObject.Find("Comida");
            if (go != null) grupoComida = go.transform;
        }

        totalFrutas = (grupoComida != null) ? grupoComida.childCount : 0;
        puntos = 0;
        ActualizaUI();
    }

    public int Puntos => puntos;  // <- para que el trofeo pueda leer el contador

    public void sumaPuntos()
    {
        // Cambio: quitar Clamp
        puntos += 1;
        ActualizaUI();
    }

    private void ActualizaUI()
    {
        if (textoPuntos != null)
        {
            textoPuntos.text = puntos.ToString();
        }
    }
}

