using Raylib_cs;
using System.Numerics;

public class GameLoop
{
    // Instancia del protagonista del videojuego
    private Player player = null!;

    // La instancia del nivel
    private Level level = null!;

    // La cámara
    private Camera2D camera; 

    // Duración de cada paso fijo de simulación (60 Hz)
    private const double FixedTimeStep = 1.0 / 60.0;

    // Límite de tiempo que puede acumularse por fotograma
    private const double MaxFrameTime = 0.25;

    // Ancho de pantalla
    private const int ScreenWidth = 1080;

    // Alto de pantalla
    private const int ScreenHeight = 1920;    

    // Límites del mundo
    public const int WorldWidth = 1080;
    public const int WorldHeight = 3840;

    // Indica si el jugador ha completado el nivel
    private bool levelCompleted = false;

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
        // Crea la ventana con una resolución de 720×900
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Juego IDV");

        // Establece una frecuencia objetivo de 60 fotogramas por segundo
        Raylib.SetTargetFPS(60);

        // Inicializar personaje en la partida
        player = new Player(100.0f, Player.GroundY - Player.PlayerSize);

        // Inicializar el nivel
        level = new Level(WorldWidth, WorldHeight);

        // Inicializar la cámara
        camera = new Camera2D();

        camera.Offset = new Vector2(ScreenWidth / 2.0f, ScreenHeight / 2.0f);

        camera.Rotation = 0.0f;
        
        camera.Zoom = 1.1f;

        // Sitúa la cámara correctamente antes del primer fotograma
        UpdateCamera();
    }

    // ReadInput() -> Lee las entradas del teclado
    // ReadInput() -> Lee las entradas del teclado
    private void ReadInput()
    {
        // Permite reiniciar el nivel cuando se ha completado
        if (levelCompleted)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.R))
            {
                RestartLevel();
            }

            return;
        }

        // Delega la lectura de controles en player
        player.ReadInput();
    }

    // FixedUpdate() -> Actualiza la simulación con un tiempo fijo
    private void FixedUpdate(double fixedTimeStep)
    {
        // Detiene la simulación cuando se completa el nivel
        if (levelCompleted)
        {
            return;
        }

        // Actualiza al jugador utilizando las plataformas del nivel
        player.Update((float)fixedTimeStep, level.GetPlatforms());

        // Comprueba si el jugador ha alcanzado la puerta
        if (level.CheckExit(player))
        {
            levelCompleted = true;
        }

        // Actualizar objetivo de la cámara
        UpdateCamera();
    }

    // UpdateCamera() -> Sigue al jugador sin mostrar zonas fuera del nivel
    private void UpdateCamera()
    {
        // Calcula la mitad del área visible teniendo en cuenta el zoom
        float halfVisibleWidth = ScreenWidth / (2.0f * camera.Zoom);
        float halfVisibleHeight = ScreenHeight / (2.0f * camera.Zoom);

        // Obtiene el centro del jugador
        float playerCenterX = player.GetX() + Player.PlayerSize / 2.0f;
        float playerCenterY = player.GetY() + Player.PlayerSize / 2.0f;

        // Limita el objetivo de la cámara a los bordes del mundo
        float targetX = Math.Clamp(playerCenterX, halfVisibleWidth, WorldWidth - halfVisibleWidth);
        float targetY = Math.Clamp(playerCenterY, halfVisibleHeight, WorldHeight - halfVisibleHeight);

        // Centra la cámara sobre el objetivo limitado
        camera.Target = new Vector2(targetX, targetY);
    }

    // Render() -> Dibuja los elementos del videojuego
    private void Render()
    {
        // Inicia el proceso de dibujo del fotograma
        Raylib.BeginDrawing();

        // Limpia la pantalla utilizando un fondo negro
        Raylib.ClearBackground(Color.Black);

        // Se dibujan elementos
        Raylib.BeginMode2D(camera);         

        // Se dibujan las plataformas del nivel
        level.Render();

        // Delega el dibujo del protagonista en player
        player.Render();

        // Se dejan de dibujar elementos
        Raylib.EndMode2D();

        // Muestra los FPS actuales en pantalla
        Raylib.DrawText(
            $"{Raylib.GetFPS()} FPS",
            ScreenWidth - 90,
            ScreenHeight - 30,
            20,
            Color.White
        );

        // Muestra el mensaje de victoria cuando se completa el nivel
        if (levelCompleted)
        {
            string victoryText = "¡Nivel completado!";
            string restartText = "Pulsa R para reiniciar el nivel";

            int victoryFontSize = 50;
            int restartFontSize = 30;

            // Calcula la posición horizontal para centrar los textos
            int victoryX = (ScreenWidth - Raylib.MeasureText(victoryText, victoryFontSize)) / 2;
            int restartX = (ScreenWidth - Raylib.MeasureText(restartText, restartFontSize)) / 2;

            // Dibuja un fondo oscuro semitransparente
            Raylib.DrawRectangle(
                0,
                0,
                ScreenWidth,
                ScreenHeight,
                new Color(0, 0, 0, 180)
            );

            // Dibuja el mensaje de victoria
            Raylib.DrawText(
                victoryText,
                victoryX,
                ScreenHeight / 2 - 60,
                victoryFontSize,
                Color.Yellow
            );

            // Dibuja las instrucciones para reiniciar
            Raylib.DrawText(
                restartText,
                restartX,
                ScreenHeight / 2 + 20,
                restartFontSize,
                Color.White
            );
        }

        // Finaliza el dibujo y presenta el fotograma
        Raylib.EndDrawing();
    }

    // RestartLevel() -> Reinicia el nivel y devuelve al jugador al inicio
    private void RestartLevel()
    {
        // Crea nuevamente al jugador en su posición inicial
        player = new Player(100.0f, Player.GroundY - Player.PlayerSize);

        // Reestablece el estado de la partida
        levelCompleted = false;

        // Actualiza la cámara para mostrar la posición inicial
        UpdateCamera();
    }
}