using UnityEngine;

public class FerryItem : MonoBehaviour
{
    public string itemName; 
    public bool isOnBoat = false;
    public bool isOnRightShore = false;

    private Vector3 startPosition;
    private Transform currentParent;

    private void Start()
    {
        startPosition = transform.position;
        currentParent = transform.parent;
    }

    public void ResetToStart()
    {
        transform.SetParent(currentParent);
        transform.position = startPosition;
        isOnBoat = false;
        isOnRightShore = false;
    }

    public void MoveWithBoat(bool toRightShore)
    {
        isOnRightShore = toRightShore;
        isOnBoat = true;
        Transform boat = GameObject.Find("Boat").transform;
        transform.SetParent(boat);
        transform.localPosition = new Vector3(0, 0.5f, 0);
    }

}
