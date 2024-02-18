using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class NavMeshClickEvent : UnityEvent<Ray> { }

public class EventManager : Singleton<EventManager>
{
    public NavMeshClickEvent NavMeshClickEvent;
    public UnityEvent ShowDialogue;
    public UnityEvent HideDialogue;
    public UnityEvent PlayIntro;
}
