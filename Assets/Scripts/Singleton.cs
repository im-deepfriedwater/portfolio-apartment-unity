using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    private static T _instance;
    public static T Instance 
    { 
        get
        {
            if (_instance == null)
            {
                Debug.LogError($"{typeof(T)} is missing");
            }

            return _instance;
        } 
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning($"Duplicate singleton found {name}");
            Destroy(this);
        }
        else 
        {
            _instance = this as T;
        }
        
        AwakeInit();
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public virtual void AwakeInit() {}
}
