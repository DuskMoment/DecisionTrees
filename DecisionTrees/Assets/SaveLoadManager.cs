using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager
{
    public void save(DTreeData data, string name)
    {
        //save the data to a file using this video https://www.youtube.com/watch?v=GoHYSOFiZHc&list=PLujKcy4gXDJXOAvJQgJlCIn3BnZk7wyZp
        FileStream file = new FileStream(Application.persistentDataPath + "/" + name + ".dat", FileMode.OpenOrCreate);

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        //add the try catch
        try
        {
            binaryFormatter.Serialize(file, data);
        }
        catch(SerializationException e) 
        {
            Debug.LogError("serialization error " +  e.Message);
        }
        finally { file.Close(); }
      

        file.Close();

    }

    public DTreeData load(string name)
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/" + name + ".dat", FileMode.OpenOrCreate);

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        DTreeData data = (DTreeData)binaryFormatter.Deserialize(file);

        file.Close();

        return data;
    }

   
}
