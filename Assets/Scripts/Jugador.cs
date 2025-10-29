using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    private float movimientoX;
    public float velocidad = 2;
    private Rigidbody2D rb2d;

    [Header("--- Salto ---")]
    public float fuerzaSalto = 2;

    [Header("--- CompruebaSuelo ---")]
    private bool estaEnSuelo;
    public LayerMask layerSuelo;
    private float radioEsferaTocaSuelo = 0.1f;
    public Transform compruebaSuelo;

    [Header("--- Animaciones ---")]
    public Animator animator;

    [Header("--- Efectos ---")]
    public AudioSource audioSource;
    public AudioClip clipComida;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb2d.linearVelocity = new Vector2(movimientoX * velocidad, rb2d.linearVelocity.y);

        if (movimientoX == 0)
        {
            animator.SetBool("estaCorriendo", false);
        }
    }

    private void FixedUpdate()
    {
        estaEnSuelo = Physics2D.OverlapCircle(compruebaSuelo.position, radioEsferaTocaSuelo, layerSuelo);
        animator.SetBool("estaSaltando", estaEnSuelo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Comida"))
        {
            FindAnyObjectByType<GameManager>()?.sumaPuntos();
            if (audioSource && clipComida) audioSource.PlayOneShot(clipComida);
            Destroy(collision.gameObject);
            return;
        }

        if (collision.transform.CompareTag("SueloMuerte") || collision.transform.CompareTag("Enemigo"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        if (collision.transform.CompareTag("Trofeo"))
        {
            var gm = FindAnyObjectByType<GameManager>();
            if (gm != null && gm.TodasFrutasRecogidas)
            {
                Destroy(collision.gameObject);

                int next = SceneManager.GetActiveScene().buildIndex + 1;
                if (next < SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadScene(next);
                else
                    SceneManager.LoadScene(0);
            }
        }
    }

    private void OnMove(InputValue inputMovimiento)
    {
        animator.SetBool("estaCorriendo", true);
        movimientoX = inputMovimiento.Get<Vector2>().x;
        if (movimientoX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movimientoX), 1, 1);
        }
    }

    private void OnJump(InputValue inputJump)
    {
        if (estaEnSuelo)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, fuerzaSalto);
        }
    }
}
