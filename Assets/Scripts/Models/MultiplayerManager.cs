using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Class containing all the multiplayer delegates
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

    public delegate void ActionReceived(string regionName, string actionName, bool[] pickedSectors);
    public static event ActionReceived StartAction;

    public delegate void EventReceived(string regionName, string eventName, double[] pickedConsequences0,
        double[] pickedConsequences1, double[] pickedConsequences2, double[] pickedTemporaryConsequences0,
        double[] pickedTemporaryConsequences1, double[] pickedTemporaryConsequences2);
    public static event EventReceived StartEvent;

    public delegate void EventChoiceMade(string regionName, string eventName, int pickedNumber);
    public static event EventChoiceMade PickEventChoice;

    public delegate void CardPlayed(string regionName, double[] cardValues, bool isGlobal);
    public static event CardPlayed PlayCard;
    
    public delegate void InvestmentMade(string investmentType);
    public static event InvestmentMade Invest;

    public delegate void BuildingMade(string regionName, string buildingID);
    public static event BuildingMade MakeBuilding;

    public delegate void MessageReceived(string message, string senderName);
    public static event MessageReceived UpdateChat;

    public delegate void PlayerLogChanged(string nl, string eng);
    public static event PlayerLogChanged UpdateLogMessage;

    public delegate void ActivityLogChanged(string nl, string eng);
    public static event ActivityLogChanged UpdateActivityLog;

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

    public static void CallStartAction(string regionName, string actionName, bool[] pickedSectors)
    {
        StartAction(regionName, actionName, pickedSectors);
    }

    public static void CallStartEvent(string regionName, string eventName, double[] pickedConsequences0,
        double[] pickedConsequences1, double[] pickedConsequences2, double[] pickedTemporaryConsequences0,
        double[] pickedTemporaryConsequences1, double[] pickedTemporaryConsequences2)
    {
        StartEvent(regionName, eventName, pickedConsequences0, pickedConsequences1, pickedConsequences2, 
            pickedTemporaryConsequences0, pickedTemporaryConsequences1, pickedTemporaryConsequences2);
    }

    public static void CallPickEventChoice(string regionName, string eventName, int pickedNumber)
    {
        PickEventChoice(regionName, eventName, pickedNumber);
    }

    public static void CallPlayCard(string regionName,  double[] cardValues, bool isGlobal)
    {
        PlayCard(regionName, cardValues, isGlobal);
    }

    public static void CallInvest(string investmentType)
    {
        Invest(investmentType);
    }

    public static void CallMakeBuilding(string regionName, string buildingID)
    {
        MakeBuilding(regionName, buildingID);
    }

    public static void CallUpdateChat(string message, string senderName)
    {
        UpdateChat(message, senderName);
    }

    public static void CallUpdateLogMessage(string nl, string eng)
    {
        UpdateLogMessage(nl, eng);
    }

    public static void CallUpdateActivity(string nl, string eng)
    {
        UpdateActivityLog(nl, eng);
    }
}
