using System.Runtime.CompilerServices;
using Raylib_cs;

public class ExitDoor
{
    private float x;
    private float y;
    private float width;
    private float height;

    public ExitDoor(float x, float y, float width, float height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public void Render()
    {
        Raylib.DrawRectangle((int)x, (int)y, (int)width, (int)height, Color.Yellow);
    }

    // Comprueba si el jugador está tocando la puerta de salida
    public bool CheckCollision(Player player)
    {
        bool horizontalCollision =
            player.GetX() + Player.PlayerSize > x &&
            player.GetX() < x + width;

        bool verticalCollision =
            player.GetY() + Player.PlayerSize > y &&
            player.GetY() < y + height;

        return horizontalCollision && verticalCollision;
    }
}