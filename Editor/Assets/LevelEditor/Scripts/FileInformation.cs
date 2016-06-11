using System.Net.Sockets;
using UnityEngine;

public class FileInformation
{
	public enum FileType
	{
		UNKNOWN,
		TXT,
		XML
	}

	public string Path;
	public string Filename;
	public FileType Type;

	public FileInformation(string path)
	{
		Path = path;
		Filename = GetFilename();
		Type = GetFileType();
	}

	private FileType GetFileType()
	{
		string[] components = Path.Split('.');

		switch (components [components.Length - 1]) 
		{
		case "TXT":
		case "txt":
			return FileType.TXT;
		case "XML":
		case "xml":
			return FileType.XML;
		default:
			return FileType.UNKNOWN;
		}
	}

	private string GetFilename()
	{
		string[] components = Path.Split('/');

		if (components.Length == 0) 
		{
			return string.Empty;
		}
		else
		{
			return components[components.Length - 1];
		}
	}
}