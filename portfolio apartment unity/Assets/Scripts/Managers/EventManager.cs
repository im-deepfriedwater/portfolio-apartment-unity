using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class NavMeshClickEvent : UnityEvent<Ray> { }

public class EventManager : Singleton<EventManager>
{
    public NavMeshClickEvent NavMeshClickEvent;
    public UnityEvent showDialogue;
    public UnityEvent hideDialogue;
    public UnityEvent playIntro;
}
