using Raylib_cs;

public class Player
{
    // Posición horizontal del jugador
    private float playerX;

    // Posición vertical del jugador
    private float playerY;

    // Velocidad de desplazamiento del jugador (píxeles por segundo)
    private const float MoveSpeed = 200.0f;

    // Dirección horizontal del jugador: -1 (izquierda), 0 (quieto), 1 (derecha)
    private int moveDirection;

    // Tamaño del personaje
    private const int PlayerSize = 50;

    // Velocidad vertical (píxeles por segundo)
    private float verticalVelocity;

    // Aceleración de la gravedad (píxeles por segundo al cuadrado)
    private const float Gravity = 900.0f;

    // Impulso inicial del salto (píxeles por segundo)
    private const float JumpSpeed = -550.0f;

    // Altura del suelo en coordenadas de pantalla
    private const float GroundY = 600.0f;

    // Indica si el jugador está apoyado en el suelo
    private bool isGrounded;

    // Almacena una solicitud de salto
    private bool jumpRequested;

    // Constructor -> Inicializa el personaje
    public Player(float x, float y)
    {
        playerX = x;
        playerY = y;

        moveDirection = 0;
        verticalVelocity = 0.0f;

        isGrounded = playerY + PlayerSize >= GroundY;
        jumpRequested = false;
    }

    // ReadInput() -> Lee las entradas del personaje
    public void ReadInput()
    {
        moveDirection = 0;

        if (Raylib.IsKeyDown(KeyboardKey.D) ||
            Raylib.IsKeyDown(KeyboardKey.Right))
        {
            moveDirection += 1;
        }

        if (Raylib.IsKeyDown(KeyboardKey.A) ||
            Raylib.IsKeyDown(KeyboardKey.Left))
        {
            moveDirection -= 1;
        }

        // Registra la pulsación del salto
        if (Raylib.IsKeyPressed(KeyboardKey.W) ||
           Raylib.IsKeyPressed(KeyboardKey.Up))
        {
            jumpRequested = true;
        }
    }

    // Update() -> Actualiza el movimiento del personaje
    public void Update(float fixedTimeStep)
    {
        // Movimiento horizontal
        playerX += moveDirection * MoveSpeed * fixedTimeStep;

        // Inicia el salto únicamente desde el suelo
        if (jumpRequested && isGrounded)
        {
            verticalVelocity = JumpSpeed;
            isGrounded = false;
        }

        // Consume la solicitud de salto
        jumpRequested = false;

        // Aplica la gravedad
        verticalVelocity += Gravity * fixedTimeStep;

        // Actualiza la posición vertical
        playerY += verticalVelocity * fixedTimeStep;

        // Detecta la colisión con el suelo al caer
        if (verticalVelocity >= 0.0f &&
            playerY + PlayerSize >= GroundY)
        {
            playerY = GroundY - PlayerSize;
            verticalVelocity = 0.0f;
            isGrounded = true;
        }
    }

    // Render() -> Dibuja al jugador
    public void Render()
    {
        // Dibuja al personaje como un cuadrado azul
        Raylib.DrawRectangle(
            (int)playerX,
            (int)playerY,
            PlayerSize,
            PlayerSize,
            Color.Blue
        );
    }
}