using Raylib_cs;

public class GameLoop
{
    // Duracion de cada paso fijo de simulacion (60 Hz)
    private const double FixedTimeStep = 1.0 / 60.0;
    // Run() -> Comienza la ejecucion
    public void Run()
    {
        // Init() -> Inicializamos ventana
        Init();
        
        // Guarda el instante inicial de referencia.
        double lastTime = Raylib.GetTime();

        // Acumula el tiempo pendiente de procesar en la simulacion fija
        double lag = 0.0;

        // Mientras no se solicite que la ventana se cierre
        // entonces, se ejecuta el juego
        while (!Raylib.WindowShouldClose())
        {
            // Calcula el tiempo transcurrido desde la ultima iteracion
            double currentTime = Raylib.GetTime();
            double deltaTime = currentTime - lastTime;

            // Actualiza la referencia temporal para la siguiente iteracion
            lastTime = currentTime;

            // Anade el tiempo transcurrido al acumulador
            lag += deltaTime;

            // Leer entrada
            ReadInput();
            // Actualiza la logica con tiempo variable
            Update(deltaTime);

            // Mientras haya tiempo suficiente para un paso fijo de simulacion
            while (lag >= FixedTimeStep)
            {
                // Actualiza la simulacion utilizando un intervalo constante
                FixedUpdate(FixedTimeStep);

                // Descuenta el tiempo de simulacion que acabamos de procesar
                lag -= FixedTimeStep;
            }

            // Dibuja el estado actual del videojuego.
            Render();
        }

        Raylib.CloseWindow(); // Se cierra ventana
    }
    
    // Init() -> Inicializar ventana del juego
    private void Init()
    {
         // Crea la ventana del videojuego
        Raylib.InitWindow(1280, 720, "Mi videojuego");

        // Establece una frecuencia objetivo de 15 fotogramas por segundo
        Raylib.SetTargetFPS(15);
    }
    
    // Lectura de entradas
    private void ReadInput()
    {
        // Comprueba si el usuario acaba de pulsar la barra espaciadora.
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            Console.WriteLine("Has pulsado ESPACIO");
        }
    }

    private void Update(double deltaTime)
    {
        // Actualiza la lógica del videojuego utilizando
        // un intervalo de tiempo variable
    }

    private void FixedUpdate(double fixedTimeStep)
    {
        // Aqui implementaremos la logica de simulacion fija.
    }

    private void Render()
    {
        // Inicia el proceso de dibujo del fotograma
        Raylib.BeginDrawing();

        // Limpia la pantalla utilizando un color de fondo
        Raylib.ClearBackground(Color.Black);

        // Aqui dibujaremos los elementos del videojuego

        // Finaliza el dibujo y presenta el fotograma
        Raylib.EndDrawing();
    }
}