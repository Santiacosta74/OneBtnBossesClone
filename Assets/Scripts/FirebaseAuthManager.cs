using Firebase;
using Firebase.Auth;
using UnityEngine;
using TMPro;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

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
                // Manejo de errores con mensajes espec�ficos
                FirebaseException firebaseEx = task.Exception?.InnerExceptions?[0] as FirebaseException;
                AuthError errorCode = (firebaseEx != null) ? (AuthError)firebaseEx.ErrorCode : (AuthError)(-1);  // Usamos -1 como valor gen�rico

                switch (errorCode)
                {
                    case AuthError.InvalidEmail:
                        statusText.text = "Correo electr�nico inv�lido.";
                        break;
                    case AuthError.WeakPassword:
                        statusText.text = "La contrase�a es demasiado d�bil.";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        statusText.text = "El correo electr�nico ya est� en uso.";
                        break;
                    default:
                        statusText.text = "Error en el registro: " + task.Exception?.Message;
                        break;
                }
                return;
            }

            FirebaseUser newUser = task.Result.User; // Accede al usuario desde AuthResult
            SceneManager.LoadScene("LevelSelectScene");
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
                // Manejo de errores con mensajes espec�ficos
                FirebaseException firebaseEx = task.Exception?.InnerExceptions?[0] as FirebaseException;
                AuthError errorCode = (firebaseEx != null) ? (AuthError)firebaseEx.ErrorCode : (AuthError)(-1);  // Usamos -1 como valor gen�rico

                switch (errorCode)
                {
                    case AuthError.InvalidEmail:
                        statusText.text = "Correo electr�nico inv�lido.";
                        break;
                    case AuthError.WrongPassword:
                        statusText.text = "Contrase�a incorrecta.";
                        break;
                    case AuthError.UserNotFound:
                        statusText.text = "Usuario no encontrado.";
                        break;
                    default:
                        statusText.text = "Error en el login: " + task.Exception?.Message;
                        break;
                }
                return;
            }

            FirebaseUser user = task.Result.User; // Accede al usuario desde AuthResult
            SceneManager.LoadScene("LevelSelectScene");
            statusText.text = "Usuario logueado correctamente: " + user.Email;
        });
    }
}
