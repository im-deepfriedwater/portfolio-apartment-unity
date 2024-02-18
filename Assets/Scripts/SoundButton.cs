using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private Color soundHoverEnter;

    [SerializeField]
    private Color soundHoverExit;

    [SerializeField]
    private Sprite soundOn;

    [SerializeField]
    private Sprite soundOff;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void ToggleMute()
    {
        AudioListener.pause = !AudioListener.pause;
        image.sprite = !AudioListener.pause ? soundOn : soundOff;
    }
}
