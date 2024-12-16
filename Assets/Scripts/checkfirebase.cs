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
                Debug.Log("Firebase está configurado correctamente.");
            }
            else
            {
                Debug.LogError($"Error en la configuración de Firebase: {task.Result}");
            }
        });
    }
}
