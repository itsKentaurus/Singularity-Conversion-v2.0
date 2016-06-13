using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Container")]
public class Container<T> where T : BlockInformation, new()
{
	private const string PATH = "Assets/Resources/XML";

	[XmlArray("Block")]
	[XmlArrayItem("Information")]
	public List<T> m_Blocks = new List<T>();

	public int Count
	{
		get { return m_Blocks.Count; }
	}

	public T ElementAt(int index)
	{
		if (index < 0 || index > m_Blocks.Count)
		{
			return null;
		}

		T block = m_Blocks[index];

		return block;
	}

	public T GetElement(string key)
	{
		for (int i = 0; i < m_Blocks.Count; ++i)
		{
			if (m_Blocks[i].m_Key.Equals(key))
			{
				return m_Blocks[i];
			}
		}

		return null;
	}

	public T Add(string key)
	{
		T block = new T();
		block.m_Key = key;
		m_Blocks.Add(block);
		return block;
	}

	public bool Contains(string key)
	{
		for (int i = 0; i < m_Blocks.Count; ++i)
		{
			if (m_Blocks[i].m_Key.Equals(key))
			{
				return true;
			}
		}

		return false;
	}

	public void Save(string filename)
	{
		var serializer = new XmlSerializer(typeof(Container<T>));
		using (var stream = new FileStream(Path.Combine(PATH, filename), FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}

	public static Container<T> Load(string filename)
	{
		var serializer = new XmlSerializer(typeof(Container<T>));
		using (var stream = new FileStream(Path.Combine(PATH, filename), FileMode.Open))
		{
			return serializer.Deserialize(stream) as Container<T>;
		}
	}
}