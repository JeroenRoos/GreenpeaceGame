using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MultiplayerManager
{
    public delegate void InitNextTurn();
    public static event InitNextTurn NextTurnClick;

    public delegate void NextTurnIsClicked();
    public static event NextTurnIsClicked NextTurnClicked;

    public delegate void OwnMoneyChanged(double changevalue, bool isAdded);
    public static event OwnMoneyChanged ChangeOwnMoney;

    public delegate void OtherPlayerMoneyChanged(double changevalue, bool isAdded);
    public static event OtherPlayerMoneyChanged ChangeOtherPlayerMoney;

    public delegate void ActionReceived(string RegionName, string ActionName, bool[] pickedSectors);
    public static event ActionReceived StartAction;

    public delegate void EventReceived(string regionName, string eventName, double[] pickedConsequences0,
        double[] pickedConsequences1, double[] pickedConsequences2, double[] pickedTemporaryConsequences0,
        double[] pickedTemporaryConsequences1, double[] pickedTemporaryConsequences2);
    public static event EventReceived StartEvent;

    //by local player
    public static void CallNextTurnClick()
    {
        NextTurnClick();
    }

    //by other player
    public static void CallNextTurnClicked()
    {
        NextTurnClicked();
    }

    public static void CallChangeOwnMoney(double changevalue, bool isAdded)
    {
        ChangeOwnMoney(changevalue, isAdded);
    }

    public static void CallChangeOtherPlayerMoney(double changevalue, bool isAdded)
    {
        ChangeOtherPlayerMoney(changevalue, isAdded);
    }

    public static void CallStartAction(string RegionName, string ActionName, bool[] pickedSectors)
    {
        StartAction(RegionName, ActionName, pickedSectors);
    }

    public static void CallStartEvent(string regionName, string eventName, double[] pickedConsequences0,
        double[] pickedConsequences1, double[] pickedConsequences2, double[] pickedTemporaryConsequences0,
        double[] pickedTemporaryConsequences1, double[] pickedTemporaryConsequences2)
    {
        StartEvent(regionName, eventName, pickedConsequences0, pickedConsequences1, pickedConsequences2, 
            pickedTemporaryConsequences0, pickedTemporaryConsequences1, pickedTemporaryConsequences2);
    }
}
