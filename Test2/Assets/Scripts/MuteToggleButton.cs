using UnityEngine;
using UnityEngine.UI;

public class MuteToggleButton : MonoBehaviour
{
    public Sprite VolumeB; 
    public Sprite NoVolumeB; 
    private bool isMuted = false;
    private Image buttonImage;

    public void Start()
    {
        buttonImage = GetComponent<Image>();
        UpdateButtonVisuals();
        GetComponent<Button>().onClick.AddListener(ToggleSound);
    }

    public void ToggleSound()
    {
        isMuted = !isMuted; 
        AudioListener.volume = isMuted ? 0f : 1f;

        UpdateButtonVisuals();
    }

    private void UpdateButtonVisuals()
    {
        buttonImage.sprite = isMuted ? NoVolumeB : VolumeB;
    }
}