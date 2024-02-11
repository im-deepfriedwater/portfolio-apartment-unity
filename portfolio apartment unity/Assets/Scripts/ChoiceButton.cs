using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI displayText;

    private string originalMsg;

    public void GigaTest()
    {
        Debug.Log("moinkaS");
    }

    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponentInChildren<TextMeshProUGUI>();
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
