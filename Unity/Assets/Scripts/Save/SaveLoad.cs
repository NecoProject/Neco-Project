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
				MemoryStream m = new MemoryStream();
				bf.Serialize(m, data);
				PlayerPrefs.SetString("SavedGame", Convert.ToBase64String(m.GetBuffer()));
				//FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
				//bf.Serialize(file, data);
				//file.Close();
		}

		public static SaveData Load()
		{
				SaveData save = null;
				String data = PlayerPrefs.GetString("SavedGame");
				if (!String.IsNullOrEmpty(data))
				{
						BinaryFormatter bf = new BinaryFormatter();
						MemoryStream m = new MemoryStream(Convert.FromBase64String(data));
						save = bf.Deserialize(m) as SaveData;
				}
				return save;
		}

		public static bool HasSavedGame()
		{
				String data = PlayerPrefs.GetString("SavedGame");
				return !String.IsNullOrEmpty(data);
		}
}
