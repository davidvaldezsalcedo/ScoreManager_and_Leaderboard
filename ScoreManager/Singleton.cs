using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T instance;

    public static void  Require()
    {
        if (instance == null)
            instance = FindObjectOfType<T>();

        if (instance == null)
        {
            GameObject tmp = new GameObject();
            tmp.name = "Singleton_" + (typeof(T));
            instance = tmp.AddComponent<T>();
        }
    }

    protected void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
            SingletonAwake();
        }
        else
        {
            Destroy(this);
        }
    }

    public abstract void SingletonAwake();
}
