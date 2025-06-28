using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public CameraMover cameraMover;

    public Transform mainMenuPanel;
    public Transform inventoryPanel;
    public Transform deckSelectPanel;
    public Transform battlePanel;
    public Transform settingsPanel;
    public Transform resultPanel;

    public void GoToMainMenu() => cameraMover.MoveToPanel(mainMenuPanel);
    public void GoToInventory() => cameraMover.MoveToPanel(inventoryPanel);
    public void GoToDeckSelect() => cameraMover.MoveToPanel(deckSelectPanel);
    public void GoToBattle() => cameraMover.MoveToPanel(battlePanel);
    public void GoToSettings() => cameraMover.MoveToPanel(settingsPanel);
    public void GoToResult() => cameraMover.MoveToPanel(resultPanel);
    public void RestartGame()
    {
        GameManager.Instance.ResetGame(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
