using UnityEngine;
using UnityEngine.SceneManagement;
public class BottlePuzzleController : MonoBehaviour
{
    [Header("Ссылки на бутылки (3 шт.)")]
    public Bottle[] bottles;        

    [Header("ID мини‑игры (для сохранения звёзд)")]
    public string miniGameId = "bottles";

    private Bottle selected;        
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                Bottle bottle = hit.collider.GetComponent<Bottle>();
                if (bottle != null)
                    HandleBottleTap(bottle);
            }
        }
    }

    private void HandleBottleTap(Bottle bottle)
    {
        if (selected == null)
        {
            selected = bottle;
            selected.SetHighlight(true);
            return;
        }
        if (selected == bottle)
        {
            selected.SetHighlight(false);
            selected = null;
            return;
        }
        selected.PourInto(bottle);
        selected.SetHighlight(false);
        selected = null;

        if (GoalCondition())
        {
            OnWin();
        }
    }

    [Header("Условие победы")]
    public int targetVolume = 4; 

    private bool GoalCondition()
    {
        foreach (var b in bottles)
            if (b.currentVolume == targetVolume)
                return true;
        return false;
    }

    private void OnWin()
    {
        Debug.Log("Puzzle solved!");
        GameManager.Instance.SetStars(miniGameId, 3);
        SceneManager.LoadScene(GameManager.Instance.selectScene);
    }
}
