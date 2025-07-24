using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HanoiController : MonoBehaviour
{
    [Header("Столбцы башни (3 шт.)")]
    public Transform[] towers; 

    [Header("Диски (в порядке от самого маленького к самому большому)")]
    public GameObject[] disks;

    [Header("Мини-игра ID")]
    public string miniGameId = "hanoi";

    private Stack<GameObject>[] stacks = new Stack<GameObject>[3];

    private GameObject selectedDisk = null;
    private int selectedTower = -1;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
            stacks[i] = new Stack<GameObject>();

        for (int i = disks.Length - 1; i >= 0; i--)
        {
            stacks[0].Push(disks[i]);
            SetDiskPosition(disks[i], 0);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {
                for (int i = 0; i < towers.Length; i++)
                {
                    if (hit.collider.transform == towers[i])
                    {
                        HandleTowerClick(i);
                        break;
                    }
                }
            }
        }
    }

    private void HandleTowerClick(int towerIndex)
    {
        if (selectedDisk == null)
        {
            if (stacks[towerIndex].Count > 0)
            {
                selectedDisk = stacks[towerIndex].Peek();
                selectedTower = towerIndex;

                HighlightDisk(selectedDisk, true);
            }
        }
        else
        {
            if (towerIndex == selectedTower)
            {

                HighlightDisk(selectedDisk, false);
                selectedDisk = null;
                selectedTower = -1;
                return;
            }

            if (CanMove(selectedDisk, towerIndex))
            {
                stacks[selectedTower].Pop();
                stacks[towerIndex].Push(selectedDisk);
                SetDiskPosition(selectedDisk, towerIndex);
            }

            HighlightDisk(selectedDisk, false);
            selectedDisk = null;
            selectedTower = -1;

            if (CheckWin())
            {
                OnWin();
            }
        }
    }

    private bool CanMove(GameObject disk, int toTower)
    {
        if (stacks[toTower].Count == 0)
            return true;

        GameObject topDisk = stacks[toTower].Peek();
        return disk.transform.localScale.x < topDisk.transform.localScale.x;
    }

    private void SetDiskPosition(GameObject disk, int towerIndex)
    {
        int stackHeight = stacks[towerIndex].Count;
        Vector3 basePos = towers[towerIndex].position;
        disk.transform.position = new Vector3(basePos.x, basePos.y + 0.3f * stackHeight, 0);
    }

    private void HighlightDisk(GameObject disk, bool highlight)
    {
        SpriteRenderer sr = disk.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = highlight ? Color.yellow : Color.white;
        }
    }

    private bool CheckWin()
    {
        return stacks[2].Count == disks.Length;
    }

    private void OnWin()
    {
        Debug.Log("Hanoi puzzle solved!");

        GameManager.Instance.SetStars(miniGameId, 3);
        SceneManager.LoadScene(GameManager.Instance.selectScene);
    }
}
