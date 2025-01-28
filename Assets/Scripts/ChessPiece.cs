using UnityEngine;
public abstract class ChessPiece : MonoBehaviour
{
    public Vector2Int boardPosition; // Przechowuje pozycj� bierki na planszy
    public bool isWhite; // Okre�la, czy bierka jest bia�a

    // Metoda wirtualna do obliczenia dost�pnych ruch�w dla danej bierki
    public virtual bool[,] GetAvailableMoves(ChessPiece[,] boardState)
    {
        bool[,] moves = new bool[8, 8]; // Tablica dost�pnych ruch�w
        return moves;
    }

    // Ustawia now� pozycj� bierki zar�wno w przestrzeni gry, jak i na planszy
    public virtual void SetPosition(Vector2Int newPosition) // Zmienione na virtual
    {
        boardPosition = newPosition; // Aktualizowanie pozycji bierki na planszy
        transform.position = new Vector3(newPosition.x, 0, newPosition.y); // Aktualizowanie fizycznej pozycji w 3D
    }

    // Sprawdza, czy pozycja (x, y) mie�ci si� na planszy
    protected bool IsInsideBoard(int x, int y)
    {
        return x >= 0 && x < 8 && y >= 0 && y < 8;
    }

    // Sprawdza, czy bierka mo�e si� poruszy� lub przechwyci� bierk� na danym polu
    protected bool CanMoveOrCapture(ChessPiece[,] boardState, int x, int y)
    {
        if (!IsInsideBoard(x, y)) return false; // Pozycja poza plansz�
        if (boardState[x, y] == null) return true; // Puste pole
        return boardState[x, y].isWhite != isWhite; // Zasada przechwytywania bierki przeciwnej strony
    }
}
