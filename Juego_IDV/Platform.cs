using Raylib_cs;

public class Platform
{
    private float x;
    private float y;

    private float width;

    private float height;

    public Platform(float x, float y, float width, float height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public void Render()
    {
        Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, Color.Green);
    }

    // Devuelve la posición horizontal de la plataforma
    public float GetX()
    {
        return x;
    }

    // Devuelve la posición vertical de la plataforma
    public float GetY()
    {
        return y;
    }

    // Devuelve la anchura de la plataforma
    public float GetWidth()
    {
        return width;
    }

    // Devuelve la altura de la plataforma
    public float GetHeight()
    {
        return height;
    }
}