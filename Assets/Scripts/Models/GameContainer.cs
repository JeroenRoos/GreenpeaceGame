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
        /*try
        {
            XmlSerializer writer = new XmlSerializer(typeof(GameContainer));
            Debug.Log("Saving game...");
            var path = Application.dataPath + "/Saves/Savestate.xml";
            FileStream file = File.Create(path);
            writer.Serialize(file, this);
            file.Close();
            Debug.Log("Saving complete");
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
        }*/

        Debug.Log(Application.persistentDataPath + "/Savestate.gd");
        var path = Application.persistentDataPath + "/Savestate.gd";
        using (Stream stream = File.Open(path, FileMode.OpenOrCreate))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }
    }

    public static GameContainer Load()
    {
        /*try
        {
            var file = Application.dataPath + "/Saves/Savestate.xml";
            XmlSerializer reader = new XmlSerializer(typeof(GameContainer));

            using (var stream = new FileStream(file, FileMode.Open))
            {
                return reader.Deserialize(stream) as GameContainer;
            }
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("loading game failed");
            return new GameContainer();
        }*/
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

