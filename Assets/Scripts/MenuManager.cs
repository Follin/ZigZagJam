using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button _selectedButton;

    public void StartGame() => SceneManager.LoadScene(1);
    public void Quit() => Application.Quit();

    private void Start()
    {
        Cursor.visible = false;
        _selectedButton.Select();
    }
}
