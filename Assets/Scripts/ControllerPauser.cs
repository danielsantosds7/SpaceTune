using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerPauser : MonoBehaviour
{


    #region VARIAVEIS
    [Header("STATUS")]
    [SerializeField] private statusGamer statusGamerJogo;

    [Header("SETTINGS")]
    public string nameScenaStartGame;

    [Header("ATRIBUIR MENUS")]
    [SerializeField] private MenusPauser MenusPauser = new MenusPauser();

    [SerializeField] private enum statusGamer { RUNTIME, PAUSAR , SETTINGS}


    private bool checkdata;
    private statusGamer memoryStatus;

    #endregion
    
    void Update()
    {
        ReceiverInputsKeys();
        ControlladorAll();
    }


    #region CONTROLLER ALL

    private void ControlladorAll()
    {
        if (memoryStatus == statusGamerJogo)
            return;


        switch (statusGamerJogo)
        {
            case statusGamer.RUNTIME:
                ClearDataMenuAndClose();
                Time.timeScale = 1;
                break;
            case statusGamer.PAUSAR:
                ClearDataMenuAndClose();
                MenusPauser.ActtiveOrDesactiveMenus("MenuPauserAll", true);
                Time.timeScale = 0;
                break;
            case statusGamer.SETTINGS:
                ClearDataMenuAndClose();
                MenusPauser.ActtiveOrDesactiveMenus("MenuPauserAll", true);
                MenusPauser.ActtiveOrDesactiveMenus("MenuSettings", true);
                Time.timeScale = 0;
                break;

        }



    }

    private void ClearDataMenuAndClose()
    {
        if (checkdata)
            return;

        MenusPauser.ActtiveOrDesactiveMenus("MenuPauserAll", false);
        MenusPauser.ActtiveOrDesactiveMenus("MenuSettings", false);
        checkdata = true;

    }

    private void ReceiverInputsKeys()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (statusGamerJogo == statusGamer.RUNTIME)
                ChagerStatus(1);
            else
                ChagerStatus(0);

        }
    }

    #endregion


    #region VOIDS PUBLICS
    public void ChagerStatus(int newStatsuGamer)
    {
        checkdata = false;
        memoryStatus = statusGamerJogo;
        statusGamerJogo = (statusGamer)newStatsuGamer;
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(nameScenaStartGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

}


[System.Serializable] public class MenusPauser
{
    [SerializeField] private List<AlMenusClass> alMenusClasses = new List<AlMenusClass>();


    public void ActtiveOrDesactiveMenus(string nameMenu,bool isActive)
    {
        bool isMenu(AlMenusClass menu)
        {
            return menu.CheckIquals(nameMenu);
        }

        AlMenusClass currectMenu = alMenusClasses.Find(isMenu);

        if (currectMenu == null)
            throw new System.Exception("ERRO O MENU ESTA FALTADO, OU EU NAO ATRIBUI, OU EU COLOQUEI UM NOME ERRADO");


        currectMenu.ActiveorDesactiveMenu(isActive);

    }


}

[System.Serializable] public class AlMenusClass
{
    [SerializeField] private string nameMenu;
    [SerializeField] private GameObject menu;

    public void ActiveorDesactiveMenu(bool isActive)
    {
        menu.SetActive(isActive);
    }

    public bool CheckIquals(string name)
    {
        return nameMenu.Equals(name);
    }

}