using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
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
            var path = Application.dataPath + "/GameFiles/Quests.xml";
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
        try
        {
            var file = Application.dataPath + "/GameFiles/Quests.xml";
            XmlSerializer reader = new XmlSerializer(typeof(QuestContainer));

            using (var stream = new FileStream(file, FileMode.Open))
            {
                return reader.Deserialize(stream) as QuestContainer;
            }
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("Quests loading failed");
            return new QuestContainer();
        }
    }
}
