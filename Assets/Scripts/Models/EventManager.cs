using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class EventManager
{
    // Event Actions
    public delegate void GameStart();
    public static event GameStart NewGame;

    public delegate void MonthChanged();
    public static event MonthChanged ChangeMonth;

    public delegate void ActivePopup();
    public static event ActivePopup PopupIsActive;

    public delegate void DisabledPopup();
    public static event DisabledPopup PopupIsDisabled;

    public delegate void FirstCardGained();
    public static event FirstCardGained FirstCardIsGained;

    public delegate void GameIsSaved();
    public static event GameIsSaved SaveGame;


    public static void CallNewGame()
    {
        NewGame();
    }

    public static void CallChangeMonth()
    {
        ChangeMonth();
    }

    public static void CallPopupIsActive()
    {
        PopupIsActive();
    }

    public static void CallPopupIsDisabled()
    {
        PopupIsDisabled();
    }

    public static void CallSaveGame()
    {
        SaveGame();
    }

}
