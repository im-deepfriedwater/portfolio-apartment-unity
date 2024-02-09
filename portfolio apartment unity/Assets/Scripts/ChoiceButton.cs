using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Text displayText;

    private string originalMsg;

    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        originalMsg = displayText.text;
        displayText.text = $"<u>{displayText.text}</u>";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        displayText.text = originalMsg;
    }
}
