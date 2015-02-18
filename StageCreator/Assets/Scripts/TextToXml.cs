// File: TextToXml
// Created by: Rigil Malubay

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  
using System.Xml;
using System.Xml.Serialization;

public class TextToXml : MonoBehaviour {
	private	XmlDocument xmlDoc = new XmlDocument();

	public void Awake()
	{
		XmlNode rootNode = xmlDoc.CreateElement("Levels");
		xmlDoc.AppendChild(rootNode);

		StreamReader sr = new StreamReader(Application.dataPath + "/" + "Resources/XML/level.txt");
		string fileContents = sr.ReadToEnd();
		sr.Close();

		XmlNode level = xmlDoc.CreateElement("Level");

		string[] lines = fileContents.Split("\n"[0]);
		int yIndex = 0;
		int xIndex = 0;
		XmlAttribute width = xmlDoc.CreateAttribute("Width");
		XmlAttribute height = xmlDoc.CreateAttribute("Height");

		char[] character = lines[0].ToCharArray();
		width.Value = character.Length.ToString();
		height.Value = lines.Length.ToString();

		level.Attributes.Append(width);
		level.Attributes.Append(height);

		foreach (string line in lines) 
		{
			char[] chars = line.ToCharArray();
			int count = 0;
			int maxCount = chars.Length - 1;
			int letterCount = 0;
			char letter;
			while(count < maxCount)
			{
				XmlNode block = xmlDoc.CreateElement("Block");
				XmlAttribute y = xmlDoc.CreateAttribute("Y");
				XmlAttribute x = xmlDoc.CreateAttribute("X");
				XmlAttribute key = xmlDoc.CreateAttribute("Key");
				XmlAttribute keyCount = xmlDoc.CreateAttribute("Unit");
				xIndex = count;
				do
				{
					letter = chars[count++];
					letterCount++;
				} while(count < maxCount && letter == chars[count]);

				key.Value = letter.ToString();
				keyCount.Value = letterCount.ToString();
				y.Value = yIndex.ToString();
				x.Value = xIndex.ToString();

				block.Attributes.Append(key);
				block.Attributes.Append(keyCount);
				block.Attributes.Append(y);
				block.Attributes.Append(x);

				letterCount = 0;
				letter = ' ';
				level.AppendChild(block);
			}
			yIndex++;
		}
		rootNode.AppendChild(level);

		xmlDoc.Save(Application.dataPath + "/Resources/XML/Levels.xml");
	}
}

