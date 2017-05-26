using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class GameContainer
{
    public Game game { get; private set; }

    public GameContainer() { }

    public GameContainer(Game game)
    {
        this.game = game;
    }

    public void Save()
    {
        var path = Application.persistentDataPath + "/Savestate.gd";
        using (Stream stream = File.Open(path, FileMode.OpenOrCreate))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }
    }

    public static GameContainer Load()
    {
        var path = Application.persistentDataPath + "/Savestate.gd";
        if (File.Exists(path))
        {
            using (Stream stream = File.OpenRead(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream) as GameContainer;
            }
        }
        return null;
    }

    public static string XmlSerializeToString(Game objectInstance)
    {
        var serializer = new XmlSerializer(objectInstance.GetType());
        var sb = new StringBuilder();

        using (TextWriter writer = new StringWriter(sb))
        {
            serializer.Serialize(writer, objectInstance);
        }

        return sb.ToString();
    }

    public static Game XmlDeserializeFromString(string objectData)
    {
        var serializer = new XmlSerializer(typeof(Game));
        object result;

        using (TextReader reader = new StringReader(objectData))
        {
            result = serializer.Deserialize(reader);
        }

        return result as Game;
    }
}

