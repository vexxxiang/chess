public class Pawn : ChessPiece
{
    public override bool[,] GetAvailableMoves(ChessPiece[,] boardState)
    {
        bool[,] moves = new bool[8, 8];

        // Normalny ruch do przodu (je�li pole jest puste)
        int direction = isWhite ? 1 : -1;  // Okre�lamy kierunek (w g�r� lub w d�)
        if (IsInsideBoard(boardPosition.x, boardPosition.y + direction) && boardState[boardPosition.x, boardPosition.y + direction] == null)
        {
            moves[boardPosition.x, boardPosition.y + direction] = true;

            // Pierwszy ruch: mo�e przesun�� si� o 2 pola do przodu (je�li oba pola s� puste)
            if ((isWhite && boardPosition.y == 1) || (!isWhite && boardPosition.y == 6))
            {
                // Sprawdzamy, czy oba pola s� puste, aby pionek m�g� si� poruszy� o 2 pola
                if (boardState[boardPosition.x, boardPosition.y + direction] == null && boardState[boardPosition.x, boardPosition.y + 2 * direction] == null)
                {
                    moves[boardPosition.x, boardPosition.y + 2 * direction] = true;
                }
            }
        }

        // Atak na skos (je�li jest przeciwnik)
        if (IsInsideBoard(boardPosition.x + 1, boardPosition.y + direction) && boardState[boardPosition.x + 1, boardPosition.y + direction] != null &&
            boardState[boardPosition.x + 1, boardPosition.y + direction].isWhite != isWhite)
        {
            moves[boardPosition.x + 1, boardPosition.y + direction] = true;
        }

        if (IsInsideBoard(boardPosition.x - 1, boardPosition.y + direction) && boardState[boardPosition.x - 1, boardPosition.y + direction] != null &&
            boardState[boardPosition.x - 1, boardPosition.y + direction].isWhite != isWhite)
        {
            moves[boardPosition.x - 1, boardPosition.y + direction] = true;
        }

        return moves;
    }
}
