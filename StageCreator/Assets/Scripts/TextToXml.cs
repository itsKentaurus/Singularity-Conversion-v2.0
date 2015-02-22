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
		float yIndex = 0;
		float xIndex = 0;
		XmlAttribute width = xmlDoc.CreateAttribute("OffSetX");
		XmlAttribute height = xmlDoc.CreateAttribute("OffSetY");

		char[] character = lines[0].ToCharArray();
		width.Value = ((character.Length - 1)/2).ToString();
		height.Value = (lines.Length / 2).ToString();

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
				xIndex = count;
				do
				{
					letter = chars[count++];
					letterCount++;
				} while(count < maxCount && letter == chars[count]);

				if (letter != '.')
				{
					XmlNode block = xmlDoc.CreateElement("Block" + letter);
					XmlAttribute y = xmlDoc.CreateAttribute("Y");
					XmlAttribute x = xmlDoc.CreateAttribute("X");
					XmlAttribute keyCount = xmlDoc.CreateAttribute("Unit");

					y.Value = (yIndex).ToString();
					x.Value = (xIndex + (count - xIndex) / 2f).ToString();
					keyCount.Value = letterCount.ToString();
					
					block.Attributes.Append(y);
					block.Attributes.Append(x);
					block.Attributes.Append(keyCount);

					level.AppendChild(block);
				}

				letterCount = 0;
				letter = ' ';
			}
			yIndex++;
		}
		rootNode.AppendChild(level);

		xmlDoc.Save(Application.dataPath + "/Resources/XML/Levels.xml");
		Debug.Log("SAVED");
	}
}

