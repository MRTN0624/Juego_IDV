using Raylib_cs;

public class Level
{
    // Colección de plataformas
    private List<Platform> platforms = null!;

    // Instancia de puerta de salida
    private ExitDoor exitDoor = null!; 

    // Posición del suelo
    private const int GroundY = 3720;

    // Ancho de pantalla
    private int screenWidth;

    // Alto de pantalla
    private int screenHeight;

    public Level(int screenWidth, int screenHeight)
    {
        platforms = new List<Platform>();
        exitDoor = new ExitDoor(240, 260, 60, 90);

        // Elaborar lista de plataformas
        platforms.Add(new Platform(180, 3560, 180, 20));
        platforms.Add(new Platform(400, 3400, 180, 20));
        platforms.Add(new Platform(650, 3240, 180, 20));
        platforms.Add(new Platform(400, 3080, 180, 20));
        platforms.Add(new Platform(180, 2920, 180, 20));
        platforms.Add(new Platform(400, 2760, 180, 20));
        platforms.Add(new Platform(650, 2600, 180, 20));
        platforms.Add(new Platform(400, 2440, 180, 20));
        platforms.Add(new Platform(180, 2280, 180, 20));

        // Plataformas adicionales para ampliar el recorrido vertical
        platforms.Add(new Platform(400, 2120, 180, 20));
        platforms.Add(new Platform(650, 1960, 180, 20));
        platforms.Add(new Platform(400, 1800, 180, 20));
        platforms.Add(new Platform(180, 1640, 180, 20));
        platforms.Add(new Platform(400, 1480, 180, 20));
        platforms.Add(new Platform(650, 1320, 180, 20));
        platforms.Add(new Platform(400, 1160, 180, 20));
        platforms.Add(new Platform(180, 1000, 180, 20));
        platforms.Add(new Platform(400, 840, 180, 20));
        platforms.Add(new Platform(650, 680, 180, 20));
        platforms.Add(new Platform(400, 520, 180, 20));
        platforms.Add(new Platform(180, 360, 180, 20));

        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
    }

    public void Render()
    {
        // Dibuja el suelo
        Raylib.DrawRectangle(
            0,          // Posición X
            GroundY,    // Posición Y
            screenWidth,  // Anchura
            screenHeight - GroundY, // Altura
            Color.Green // Color
        );

        // Dibuja todas las plataformas del nivel
        foreach (Platform platform in platforms)
        {
            platform.Render();
        }

        // Dibuja puerta de salida
        exitDoor.Render();
    }

    // Devuelve la colección de plataformas del nivel
    public List<Platform> GetPlatforms()
    {
        return platforms;
    }

    // Comprueba si el jugador ha alcanzado la puerta de salida
    public bool CheckExit(Player player)
    {
        return exitDoor.CheckCollision(player);
    }
}