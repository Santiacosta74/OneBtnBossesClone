using Firebase;
using Firebase.Auth;
using UnityEngine;
using Firebase.Extensions;

public class FirebaseConfig : MonoBehaviour
{
    private FirebaseAuth auth;

    void Start()
    {
        // Inicializa Firebase sin la base de datos
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.GetAuth(app);
            Debug.Log("Firebase Authentication configurado correctamente");
        });
    }
}
