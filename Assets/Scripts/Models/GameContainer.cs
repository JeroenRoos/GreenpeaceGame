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
}

