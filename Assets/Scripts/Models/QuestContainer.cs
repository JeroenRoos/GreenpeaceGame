using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[Serializable]
public class QuestContainer
{
    [XmlArray("Quests"), XmlArrayItem("Quest")]
    public List<Quest> quests { get; private set; }

    public QuestContainer() { }

    public QuestContainer(List<Quest> quests)
    {
        this.quests = quests;
    }

    public void Save()
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(QuestContainer));
            Debug.Log("Serializing Quests");
            var path = Application.dataPath + "/Resources/Quests.xml";
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

    public static QuestContainer Load()
    {
        //resources reading for build
        TextAsset textAsset = (TextAsset)Resources.Load("Quests");
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);
        XmlSerializer serializer = new XmlSerializer(typeof(QuestContainer));
        StringReader reader = new StringReader(xml.OuterXml);
        return serializer.Deserialize(reader) as QuestContainer;
    }
}
