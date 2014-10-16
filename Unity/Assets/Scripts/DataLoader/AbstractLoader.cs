using UnityEngine;
using System.Collections;
using System.IO;
using System;


public abstract class AbstractLoader
{
		private const string RESOURCE_FOLDER = "Data";
		private const string SEPARATOR = ";";

		protected string[] header;

		protected void Load()
		{
				string filePath = RESOURCE_FOLDER + "/" + File();
				TextAsset asset = (TextAsset)Resources.Load(filePath, typeof(TextAsset));
				string fileContents = asset.text;

				string[] lines = fileContents.Split("\n"[0]);

				// Populate the monsters
				header = lines[0].Split(SEPARATOR[0]);
				//Debug.Log("Header is " + String.Join(", ", header));
				for (int i = 1; i < lines.Length; i++)
				{
						string line = lines[i];
						if (line.Trim().Length > 0)
						{
								//Debug.Log(line);
								string[] data = line.Split(SEPARATOR[0]);
								LoadItem(data);
						}
				}
		}

		protected abstract string File();

		protected abstract void LoadItem(string[] data);

		protected string getValue(string[] data, string tag)
		{
				//Debug.Log("Getting tag: " + tag + " from data " + String.Join(", ", data));
				int tagIndex = Array.IndexOf(header, tag);
				//Debug.Log("Tag index is " + tagIndex);
				string result = data[tagIndex];
				//Debug.Log("Result is " + result);
				return result.Trim();
		}
}
