using Firebase;
using Firebase.Auth;
using UnityEngine;
using TMPro; // Asegúrate de tener este using para TextMesh Pro
using Firebase.Extensions;

public class FirebaseAuthManager : MonoBehaviour
{
    public TMP_InputField emailInput;  // Cambiado a TMP_InputField
    public TMP_InputField passwordInput; // Cambiado a TMP_InputField
    public TMP_Text statusText;  // Cambiado a TMP_Tex

    private FirebaseAuth auth;

    void Start()
    {
        // Verifica y configura Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.GetAuth(app);
            statusText.text = "Firebase configurado correctamente";
        });
    }

    // Registro de nuevo usuario
    public void Register()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "Error en el registro: " + task.Exception;
                return;
            }

            FirebaseUser newUser = task.Result.User; // Accede al usuario desde AuthResul
            statusText.text = "Usuario registrado correctamente: " + newUser.DisplayName;
        });
    }

    // Login de usuario existente
    public void Login()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "Error en el login: " + task.Exception;
                return;
            }

            FirebaseUser user = task.Result.User; // Accede al usuario desde AuthResult
            statusText.text = "Usuario logueado correctamente: " + user.Email;
        });
    }

    // Cerrar sesión
    public void Logout()
    {
        auth.SignOut();
        statusText.text = "Usuario desconectado";
    }
}
