using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Chequeo de suelo")]
    public Transform groundCheck;       // Objeto debajo de los pies
    public float groundDistance = 0.4f; // Radio del chequeo
    public LayerMask groundMask;        // Capa del suelo
    private bool isGrounded;

    [Header("Dificultad y Score")]
    public float difficultyMultiplier = 1f;      // Multiplicador de dificultad
    public float difficultyIncreaseRate = 0.01f; // Qué tan rápido aumenta la dificultad

    private Rigidbody rb;
    private HighScoreManager highScoreManager;

    [Header("Efecto de velocidad (Boost)")]
    public float boostedSpeed = 10f;    // Velocidad temporal aumentada
    public float boostDuration = 3f;    // Duración del efecto
    private float boostTimer = 0f;
    private bool isBoosted = false;
    private Vector3 originalScale;      // Para guardar el tamaño original

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Guardar el tamaño original del jugador
        originalScale = transform.localScale;

        // Buscar el HighScoreManager al iniciar
        highScoreManager = Object.FindFirstObjectByType<HighScoreManager>();
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Si está en modo boost, usa la velocidad aumentada
        float currentSpeed = isBoosted ? boostedSpeed : speed;

        // Aplica la velocidad multiplicada por la dificultad
        Vector3 velocity = move * currentSpeed * difficultyMultiplier;
        velocity.y = rb.linearVelocity.y; // Mantiene la gravedad
        rb.linearVelocity = velocity;
    }

    void Update()
    {
        // Chequea si está tocando el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Incrementa la dificultad con el tiempo
        difficultyMultiplier += Time.deltaTime * difficultyIncreaseRate;

        // Efecto de velocidad temporal (cuenta regresiva)
        if (isBoosted)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f)
            {
                isBoosted = false;
                transform.localScale = originalScale; // vuelve al tamaño original
            }
        }

        // Suma score usando ScoreManager y la dificultad
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.score += Time.deltaTime * ScoreManager.instance.scoreMultiplier * difficultyMultiplier;
        }

        // Actualiza HighScore si el manager existe
        if (highScoreManager != null && ScoreManager.instance != null)
        {
            highScoreManager.CheckHighScore(Mathf.FloorToInt(ScoreManager.instance.score));
        }
    }

    // Detecta cuando toca un pickup
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            // Activa el boost de velocidad
            isBoosted = true;
            boostTimer = boostDuration;

            // Cambia tamaño para que se note el efecto
            transform.localScale = originalScale * 1.5f;
        }
    }
}
