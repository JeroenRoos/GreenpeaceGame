using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[Serializable]
public class BuildingContainer
{
    [XmlArray("Buildings"), XmlArrayItem("Building")]
    public List<Building> buildings { get; private set; }

    public BuildingContainer() { }

    public BuildingContainer(List<Building> buildings)
    {
        this.buildings = buildings;
    }

    public void Save()
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(BuildingContainer));
            Debug.Log("Serializing buildings");
            var path = Application.dataPath + "/Resources/Buildings.xml";
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

    public static BuildingContainer Load()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Buildings");
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);
        XmlSerializer serializer = new XmlSerializer(typeof(BuildingContainer));
        StringReader reader = new StringReader(xml.OuterXml);
        return serializer.Deserialize(reader) as BuildingContainer;
    }
}
