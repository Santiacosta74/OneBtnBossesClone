using Firebase;
using UnityEngine;

public class CheckFirebase : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase est� configurado correctamente.");
            }
            else
            {
                Debug.LogError($"Error en la configuraci�n de Firebase: {task.Result}");
            }
        });
    }
}
