using UnityEngine;
public class Bottle : MonoBehaviour
{
    [Header("Параметры")]
    public int maxVolume = 8;
    [Range(0, 8)] public int currentVolume = 0;

    [Header("Визуализация")]
    public Transform fill;         
    public SpriteRenderer outline;  

    #region API
    public int PourInto(Bottle target)
    {
        if (currentVolume == 0) return 0;
        if (target.IsFull()) return 0;

        int free = target.maxVolume - target.currentVolume;
        int amount = Mathf.Min(currentVolume, free);

        currentVolume -= amount;
        target.currentVolume += amount;

        UpdateVisual();
        target.UpdateVisual();
        return amount;
    }

    public bool IsFull() => currentVolume >= maxVolume;

    public void SetHighlight(bool state) => outline.enabled = state;

    public void UpdateVisual()
    {
        float ratio = (float)currentVolume / maxVolume;
        fill.localScale = new Vector3(1f, ratio, 1f);
    }
    #endregion

    private void OnValidate() => UpdateVisual();
}
