using UnityEngine;

public class ChessGameManager : MonoBehaviour
{
    public ChessPiece[,] boardState = new ChessPiece[8, 8];  // Tablica stanu planszy (bierek)
    private ChessPiece selectedPiece; // Wybrana bierka
    public bool isWhiteTurn = true;  // Sprawdzanie, kt�rej stronie nale�y tura
    public GameObject _Camera;

    void Start()
    {
        // Przyk�ad inicjalizacji gry - np. umieszczanie bierek
        // Mo�esz to dostosowa� do swojego kodu
    }

    // Funkcja do wyboru bierki (klikni�cie na bierk�)
    public void SelectPiece(ChessPiece piece, Vector2Int position)
    {
        if ((isWhiteTurn && piece.isWhite) || (!isWhiteTurn && !piece.isWhite))
        {
            selectedPiece = piece;
            Debug.Log("Wybrano bierk�: " + piece.name + " na pozycji: " + position);
        }
    }

    // Funkcja do wykonania ruchu bierk� (klikni�cie na pole)
    public void MovePiece(Vector2Int targetPosition)
    {
        if (selectedPiece != null)
        {
            // Sprawdzamy, czy ruch jest dozwolony
            bool[,] availableMoves = selectedPiece.GetAvailableMoves(boardState);

            if (availableMoves[targetPosition.x, targetPosition.y])
            {
                // Ruch dozwolony - ustawiamy now� pozycj� bierki
                selectedPiece.SetPosition(targetPosition);

                // Prze��czamy tur�
                isWhiteTurn = !isWhiteTurn;

                // Resetujemy wybran� bierk�
                selectedPiece = null;

                _Camera.GetComponent<CameraSettings>().RotateAroundBoard();


            }
            else
            {
                Debug.Log("Niedozwolony ruch na to pole.");
            }
        }
        else
        {
            Debug.Log("Nie wybrano bierki.");
        }
    }
}
