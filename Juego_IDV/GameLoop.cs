using Raylib_cs;

public class GameLoop
{
    // Instancia del protagonista del videojuego
    private Player player;

    // Duración de cada paso fijo de simulación (60 Hz)
    private const double FixedTimeStep = 1.0 / 60.0;

    // Límite de tiempo que puede acumularse por fotograma
    private const double MaxFrameTime = 0.25;

    // Run() -> Comienza la ejecución del videojuego
    public void Run()
    {
        // Init() -> Inicializamos la ventana
        Init();

        // Guarda el instante inicial de referencia
        double lastTime = Raylib.GetTime();

        // Acumula el tiempo pendiente de procesar en la simulación fija
        double lag = 0.0;

        // Mientras no se solicite cerrar la ventana,
        // el videojuego continúa ejecutándose
        while (!Raylib.WindowShouldClose())
        {
            // Obtiene el tiempo actual
            double currentTime = Raylib.GetTime();

            // Calcula el tiempo transcurrido desde la última iteración
            double deltaTime = currentTime - lastTime;

            // Actualiza la referencia temporal para la siguiente iteración
            lastTime = currentTime;

            // Evita procesar demasiado tiempo tras una pausa larga
            if (deltaTime > MaxFrameTime)
            {
                deltaTime = MaxFrameTime;
            }

            // Añade el tiempo transcurrido al acumulador
            lag += deltaTime;

            // Lee las entradas del jugador
            ReadInput();

            // Mientras haya tiempo suficiente para un paso fijo de simulación
            while (lag >= FixedTimeStep)
            {
                // Actualiza la simulación utilizando un intervalo constante
                FixedUpdate(FixedTimeStep);

                // Descuenta el tiempo de simulación que acabamos de procesar
                lag -= FixedTimeStep;
            }

            // Dibuja el estado actual del videojuego
            Render();
        }

        // Cierra la ventana y libera los recursos de Raylib
        Raylib.CloseWindow();
    }

    // Init() -> Inicializa la ventana del videojuego
    private void Init()
    {
        // Crea la ventana con una resolución de 1280x720
        Raylib.InitWindow(1280, 720, "Juego IDV");

        // Establece una frecuencia objetivo de 60 fotogramas por segundo
        Raylib.SetTargetFPS(60);

        // Inicializar personaje en la partida
        player = new Player(100.0f, 550.0f);
    }

    // ReadInput() -> Lee las entradas del teclado
    private void ReadInput()
    {
        // Delega la lectura de controles en player
        player.ReadInput();
    }

    // FixedUpdate() -> Actualiza la simulación con un tiempo fijo
    private void FixedUpdate(double fixedTimeStep)
    {
        // Delega la actualización del personaje en player
        player.Update((float)fixedTimeStep);
    }

    // Render() -> Dibuja los elementos del videojuego
    private void Render()
    {
        // Inicia el proceso de dibujo del fotograma
        Raylib.BeginDrawing();

        // Limpia la pantalla utilizando un fondo negro
        Raylib.ClearBackground(Color.Black);

        // Dibuja el suelo
        Raylib.DrawRectangle(
            0,          // Posición X
            600,        // Posición Y
            1280,       // Anchura
            120,        // Altura
            Color.Green // Color
        );

        // Delega el dibujo del protagonista en player
        player.Render();

        // Muestra los FPS actuales en pantalla
        Raylib.DrawFPS(1180, 680);

        // Finaliza el dibujo y presenta el fotograma
        Raylib.EndDrawing();
    }
}