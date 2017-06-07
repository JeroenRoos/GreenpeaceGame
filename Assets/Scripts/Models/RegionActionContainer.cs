using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[Serializable]
public class RegionActionContainer
{
    [XmlArray("RegionActions"), XmlArrayItem("RegionAction")]
    public List<RegionAction> actions { get; private set; }

    public RegionActionContainer() { }

    public RegionActionContainer(List<RegionAction> actions)
    {
        this.actions = actions;
    }

    public void Save()
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(RegionActionContainer));
            Debug.Log("Serializing RegionActions");
            var path = Application.dataPath + "/Resources/RegionActions.xml";
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

    public static RegionActionContainer Load()
    {
        //resources reading for build
        TextAsset textAsset = (TextAsset)Resources.Load("RegionActions");
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);
        XmlSerializer serializer = new XmlSerializer(typeof(RegionActionContainer));
        StringReader reader = new StringReader(xml.OuterXml);
        return serializer.Deserialize(reader) as RegionActionContainer;
    }
}
