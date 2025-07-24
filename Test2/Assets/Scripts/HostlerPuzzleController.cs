using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostlerPuzzleController : MonoBehaviour
{
    public GameObject[] allKnights;
    public Transform[] boardPositions; 
    public string miniGameId = "hostler_puzzle";

    private Dictionary<GameObject, int> currentPositions = new Dictionary<GameObject, int>();
    private GameObject selectedKnight = null;

    private readonly Vector2Int[] knightMoves = new Vector2Int[]
    {
        new Vector2Int(1, 2), new Vector2Int(2, 1),
        new Vector2Int(2, -1), new Vector2Int(1, -2),
        new Vector2Int(-1, -2), new Vector2Int(-2, -1),
        new Vector2Int(-2, 1), new Vector2Int(-1, 2)
    };

    void Start()
    {
        InitializeBoard();
    }

    void InitializeBoard()
    {
        currentPositions.Clear();
        for (int i = 0; i < allKnights.Length; i++)
        {
            currentPositions[allKnights[i]] = i;
            allKnights[i].transform.position = boardPositions[i].position;
        }
    }

    public void OnKnightClicked(GameObject knight)
    {
        if (selectedKnight == null)
        {
            selectedKnight = knight;
        }
        else
        {
            if (IsValidMove(selectedKnight, knight))
            {
                SwapKnights(selectedKnight, knight);
                if (CheckWinCondition())
                {
                    Debug.Log("Победа в задаче Конюх!");
                    GameManager.Instance.SetStars(miniGameId, 3);
                    SceneManager.LoadScene(GameManager.Instance.selectScene);
                }
            }

            selectedKnight = null;
        }
    }

    bool IsValidMove(GameObject fromKnight, GameObject toKnight)
    {
        int fromIndex = currentPositions[fromKnight];
        int toIndex = currentPositions[toKnight];

        Vector2Int fromCoord = IndexToCoord(fromIndex);
        Vector2Int toCoord = IndexToCoord(toIndex);

        Vector2Int diff = toCoord - fromCoord;

        foreach (var move in knightMoves)
        {
            if (diff == move)
                return true;
        }

        return false;
    }

    void SwapKnights(GameObject a, GameObject b)
    {
        int indexA = currentPositions[a];
        int indexB = currentPositions[b];

        a.transform.position = boardPositions[indexB].position;
        b.transform.position = boardPositions[indexA].position;

        currentPositions[a] = indexB;
        currentPositions[b] = indexA;
    }

    bool CheckWinCondition()
    {

        for (int i = 0; i < allKnights.Length; i++)
        {
            var knight = allKnights[i];
            var index = currentPositions[knight];

            if (knight.name.Contains("White") && index > 2)
                return false;

            if (knight.name.Contains("Black") && index < 6)
                return false;
        }

        return true;
    }

    Vector2Int IndexToCoord(int index)
    {
        return new Vector2Int(index % 3, index / 3);
    }
}
