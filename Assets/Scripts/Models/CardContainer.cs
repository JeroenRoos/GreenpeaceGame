using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[Serializable]
public class CardContainer
{
    [XmlArray("Cards"), XmlArrayItem("Card")]
    public List<Card> cards { get; private set; }

    public CardContainer() { }

    public CardContainer(List<Card> cards)
    {
        this.cards = cards;
    }

    public void Save()
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(CardContainer));
            Debug.Log("Serializing Cards");
            var path = Application.dataPath + "/Resources/Cards.xml";
            FileStream file = File.Create(path);
            writer.Serialize(file, this);
            file.Close();
            Debug.Log("Serialization finished");
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public static CardContainer Load()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Cards");
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);
        XmlSerializer serializer = new XmlSerializer(typeof(CardContainer));
        StringReader reader = new StringReader(xml.OuterXml);
        return serializer.Deserialize(reader) as CardContainer;
    }
}
