using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public InventoryUI inventoryUI;

    public void ShowMainMenu()
    {
        cameraManager.GoToMainMenu();
    }

    public void ShowInventory()
    {
        cameraManager.GoToInventory();
        inventoryUI.PopulateInventory();
    }

    public void ShowSettings()
    {
        cameraManager.GoToSettings();
    }

    public void ShowDeckSelect()
    {
        cameraManager.GoToDeckSelect();
    }

    public void ShowBattle()
    {
        cameraManager.GoToBattle();
    }

    public void ShowResult()
    {
        cameraManager.GoToResult();
    }
}
