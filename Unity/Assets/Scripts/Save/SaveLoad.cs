using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveLoad
{
		public static void Save(SaveData data)
		{
				Debug.Log("Saving data");
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
				bf.Serialize(file, data);
				file.Close();
		}

		public static SaveData Load()
		{
				if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
				{
						Debug.Log("Loading data");
						BinaryFormatter bf = new BinaryFormatter();
						FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
						SaveData data = (SaveData)bf.Deserialize(file);
						file.Close();
						return data;
				}
				return null;
		}
}
