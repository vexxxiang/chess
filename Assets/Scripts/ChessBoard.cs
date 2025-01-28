using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public GameObject squarePrefab; // Prefab pola szachownicy
    public float squareSize = 1f;   // Rozmiar pojedynczego pola

    private GameObject[,] board = new GameObject[8, 8];
    public ChessGameManager gameManager;

    void Start() 
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int z = 0; z < 8; z++) // Zmieniamy y na z (poziom planszy)
            {
                Vector3 position = new Vector3(x * squareSize, -0.05f, z * squareSize); // Pozycja na szachownicy
                board[x, z] = Instantiate(squarePrefab, position, Quaternion.identity, transform);

                // Kolorowanie p�l szachownicy
                Renderer renderer = board[x, z].GetComponent<Renderer>();
                renderer.material.color = (x + z) % 2 == 0 ? Color.white : Color.black; // Kolor naprzemienny
            }
        }
    }

    void Update()
    {
        // Wykrywanie klikni�cia lewym przyciskiem myszy
        if (Input.GetMouseButtonDown(0)) // 0 oznacza lewy przycisk myszy
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Tworzymy promie� od pozycji myszy
            RaycastHit hit; // Zmienna do przechowywania wyniku raycasta

            // Je�li promie� trafi� w jakikolwiek obiekt
            if (Physics.Raycast(ray, out hit))
            {
                // Sprawdzamy pozycj�, w kt�r� trafi� raycast
                Vector3 hitPosition = hit.point;

                // Obliczamy najbli�sze pole, bior�c pod uwag� rozmiar planszy
                Vector2Int clickedPosition = new Vector2Int(
                    Mathf.RoundToInt(hitPosition.x / squareSize), 
                    Mathf.RoundToInt(hitPosition.z / squareSize)
                );

                // Upewniamy si�, �e klikni�cie mie�ci si� w granicach planszy
                if (clickedPosition.x >= 0 && clickedPosition.x < 8 && clickedPosition.y >= 0 && clickedPosition.y < 8)
                {
                    ChessPiece clickedPiece = hit.collider.GetComponent<ChessPiece>(); 

                    if (clickedPiece != null)
                    {
                        // Je�li klikni�to bierk�
                        Debug.Log("Klikni�to figur�: " + clickedPiece.name);
                        gameManager.SelectPiece(clickedPiece, clickedPosition); // Wybieramy bierk�
                    }
                    else
                    {
                        // Je�li klikni�to puste pole
                        Debug.Log("Klikni�to puste pole: " + clickedPosition);
                        gameManager.MovePiece(clickedPosition); // Przemieszczamy bierk�
                    }
                }
                else
                {
                    Debug.Log("Klikni�cie poza plansz�: " + clickedPosition);
                }
            }
        }
    }
}
