﻿using System;
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

    public delegate void ActivatedBackgroundMusic();
    public static event ActivatedBackgroundMusic PlayBackgroundMusic;

    public delegate void ButtonIsClicked();
    public static event ButtonIsClicked PlayButtonClickSFX;

    public delegate void ButtonIsHovered();
    public static event ButtonIsHovered PlayButtonHoverSFX;
    
    public delegate void IsNewturn();
    public static event IsNewturn PlayNewTurnStartSFX;

    public delegate void GameIsLeft();
    public static event GameIsLeft LeaveGame;

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

    public static void CallPlayBackgroundMusic()
    {
        PlayBackgroundMusic();
    }

    public static void CallPlayButtonClickSFX()
    {
        PlayButtonClickSFX();
    }

    public static void CallPlayButtonHoverSFX()
    {
        PlayButtonHoverSFX();
    }
    public static void CallPlayNewTurnStartSFX()
    {
        PlayNewTurnStartSFX();
    }

    public static void CallLeaveGame()
    {
        LeaveGame();
    }
}
