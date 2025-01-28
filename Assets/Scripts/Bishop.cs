using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bishop : ChessPiece
{
    public override bool[,] GetAvailableMoves(ChessPiece[,] boardState)
    {
        bool[,] moves = new bool[8, 8];

        // Cztery kierunki po przek�tnej
        CheckLine(boardState, moves, 1, 1);   // Prawo-g�ra
        CheckLine(boardState, moves, 1, -1);  // Prawo-d�
        CheckLine(boardState, moves, -1, 1);  // Lewo-g�ra
        CheckLine(boardState, moves, -1, -1); // Lewo-d�

        return moves;
    }

    private void CheckLine(ChessPiece[,] boardState, bool[,] moves, int xDirection, int yDirection)
    {
        int x = boardPosition.x;
        int y = boardPosition.y;

        while (IsInsideBoard(x + xDirection, y + yDirection))
        {
            x += xDirection;
            y += yDirection;

            if (boardState[x, y] == null)
            {
                moves[x, y] = true;
            }
            else
            {
                if (boardState[x, y].isWhite != isWhite)
                {
                    moves[x, y] = true;
                }
                break;
            }
        }
    }
}
