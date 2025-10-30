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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Buscar el HighScoreManager al iniciar
        highScoreManager = Object.FindFirstObjectByType<HighScoreManager>();
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Aplica la velocidad multiplicada por la dificultad
        Vector3 velocity = move * speed * difficultyMultiplier;
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
}
