using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[Serializable]
public class GameEventContainer
{
    [XmlArray("GameEvents"), XmlArrayItem("GameEvent")]
    public List<GameEvent> events { get; private set; }

    public GameEventContainer() { }

    public GameEventContainer(List<GameEvent> events)
    {
        this.events = events;
    }

    public void Save()
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(GameEventContainer));
            Debug.Log("Serializing GameEvents");
            var path = Application.dataPath + "/Resources/GameEvents.xml";
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

    public static GameEventContainer Load()
    {
        /*try
        {
            var file = Application.dataPath + "/GameFiles/GameEvents.xml";
            XmlSerializer reader = new XmlSerializer(typeof(GameEventContainer));

            using (var stream = new FileStream(file, FileMode.Open))
            {
                return reader.Deserialize(stream) as GameEventContainer;
            }
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("Event loading failed");
            return new GameEventContainer();
        }*/

        //resources reading for build
        TextAsset textAsset = (TextAsset)Resources.Load("GameEvents");
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);
        XmlSerializer serializer = new XmlSerializer(typeof(GameEventContainer));
        StringReader reader = new StringReader(xml.OuterXml);
        return serializer.Deserialize(reader) as GameEventContainer;
    }
}
