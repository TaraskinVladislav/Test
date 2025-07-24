using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FerryPuzzleController : MonoBehaviour
{
    public Transform leftShore;
    public Transform rightShore;
    public Transform boat;

    public Transform boatLeftPos;
    public Transform boatRightPos;

    public List<FerryItem> allItems;

    public string miniGameId = "ferry_puzzle";

    private bool isBoatOnLeft = true;

    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        isBoatOnLeft = true;
        boat.position = boatLeftPos.position;

        foreach (var item in allItems)
        {
            item.ResetToStart();
        }
    }

    public void MoveBoat()
    {
        if (GetBoatPassengers().Count > 1)
            return; // максимум 1 пассажир

        isBoatOnLeft = !isBoatOnLeft;
        boat.position = isBoatOnLeft ? boatLeftPos.position : boatRightPos.position;

        // перевезти всех пассажиров вместе с лодкой
        //foreach (var item in GetBoatPassengers())
        //{
        //    item.MoveWithBoat(boat.position);
        //}

        // Проверка условий
        if (CheckLoseCondition())
        {
            Debug.Log("Проигрыш!");
            ResetGame();
        }

        if (CheckWinCondition())
        {
            Debug.Log("Победа!");
            GameManager.Instance.SetStars(miniGameId, 3);
            SceneManager.LoadScene(GameManager.Instance.selectScene);
        }
    }

    public List<FerryItem> GetBoatPassengers()
    {
        List<FerryItem> passengers = new List<FerryItem>();
        foreach (var item in allItems)
        {
            if (item.isOnBoat)
                passengers.Add(item);
        }
        return passengers;
    }

    public bool CheckWinCondition()
    {
        foreach (var item in allItems)
        {
            if (!item.isOnRightShore)
                return false;
        }
        return true;
    }

    public bool CheckLoseCondition()
    {
        List<string> left = new List<string>();
        List<string> right = new List<string>();

        foreach (var item in allItems)
        {
            if (item.isOnBoat) continue;

            if (item.isOnRightShore)
                right.Add(item.itemName);
            else
                left.Add(item.itemName);
        }

        if ((isBoatOnLeft && CheckConflict(right)) ||
            (!isBoatOnLeft && CheckConflict(left)))
        {
            return true;
        }

        return false;
    }

    private bool CheckConflict(List<string> items)
    {
        if (items.Contains("Wolf") && items.Contains("Goat"))
            return true;
        if (items.Contains("Goat") && items.Contains("Cabbage"))
            return true;
        return false;
    }
}
