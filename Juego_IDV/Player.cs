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
    public const int PlayerSize = 50;

    // Velocidad vertical (píxeles por segundo)
    private float verticalVelocity;

    // Aceleración de la gravedad (píxeles por segundo al cuadrado)
    private const float Gravity = 900.0f;

    // Impulso inicial del salto (píxeles por segundo)
    private const float JumpSpeed = -650.0f;

    // Altura del suelo en coordenadas de pantalla
    public const float GroundY = 3720.0f;

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
    public void Update(float fixedTimeStep, List<Platform> platforms)
    {
        // Movimiento horizontal
        playerX += moveDirection * MoveSpeed * fixedTimeStep;
        
        // Movimiento horizontal de acuerdo a los límites horizontales del escenario
        playerX = Math.Clamp(playerX, 0.0f, GameLoop.WorldWidth - PlayerSize);

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

        // Guarda la posición vertical anterior para detectar aterrizajes
        float previousY = playerY;

        // Actualiza la posición vertical
        playerY += verticalVelocity * fixedTimeStep;

        // Comprueba si el jugador aterriza sobre alguna plataforma
        if (verticalVelocity >= 0.0f)
        {
            foreach (Platform platform in platforms)
            {
                // Comprueba si el jugador coincide horizontalmente con la plataforma
                bool horizontalCollision =
                    playerX + PlayerSize > platform.GetX() &&
                    playerX < platform.GetX() + platform.GetWidth();

                // Comprueba si los pies cruzan la superficie desde arriba
                bool verticalCollision =
                    previousY + PlayerSize <= platform.GetY() &&
                    playerY + PlayerSize >= platform.GetY();

                if (horizontalCollision && verticalCollision)
                {
                    // Coloca al jugador encima de la plataforma
                    playerY = platform.GetY() - PlayerSize;

                    // Detiene la caída y permite volver a saltar
                    verticalVelocity = 0.0f;
                    isGrounded = true;

                    break;
                }
            }
        }

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

    public float GetX()
    {
        return playerX;
    }

    public float GetY()
    {
        return playerY;
    }
}