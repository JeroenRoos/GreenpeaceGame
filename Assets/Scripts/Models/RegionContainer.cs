using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[Serializable]
public class RegionContainer
{
    [XmlArray("Regions"), XmlArrayItem("Region")]
    public List<Region> regions { get; private set; }

    public RegionContainer() { }

    public RegionContainer(List<Region> regions)
    {
        this.regions = regions;
    }

    public void Save()
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(RegionContainer));
            Debug.Log("Serializing Regions");
            var path = Application.dataPath + "/GameFiles/Regions.xml";
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

    public static RegionContainer Load()
    {
        try
        {
            var file = Application.dataPath + "/GameFiles/Regions.xml";
            XmlSerializer reader = new XmlSerializer(typeof(RegionContainer));

            using (var stream = new FileStream(file, FileMode.Open))
            {
                return reader.Deserialize(stream) as RegionContainer;
            }
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("Region loading failed");
            return new RegionContainer();
        }
    }
}

