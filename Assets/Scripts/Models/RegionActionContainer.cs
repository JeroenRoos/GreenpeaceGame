using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
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
            var path = Application.dataPath + "/GameFiles/RegionActions.xml";
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
        try
        {
            var file = Application.dataPath + "/GameFiles/RegionActions.xml";
            XmlSerializer reader = new XmlSerializer(typeof(RegionActionContainer));

            using (var stream = new FileStream(file, FileMode.Open))
            {
                return reader.Deserialize(stream) as RegionActionContainer;
            }
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("RegionAction loading failed");
            return new RegionActionContainer();
        }
    }
}
