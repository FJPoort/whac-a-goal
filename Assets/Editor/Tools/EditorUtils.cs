using System.IO;
using UnityEditor;
using UnityEngine;

public static class EditorUtils
{
	[MenuItem("ProjectTools/Clear Storage")]
	public static void ClearStorage()
	{
		if(EditorUtility.DisplayDialog("Clear storage", "Do you want to remove all save files?", "Yes", "No"))
		{
			DirectoryInfo saveDir = new DirectoryInfo(Application.persistentDataPath);

			foreach(FileInfo fileInfo in saveDir.GetFiles())
			{
				fileInfo.Delete();
			}

			foreach(DirectoryInfo directoryInfo in saveDir.GetDirectories())
			{
				directoryInfo.Delete(true);
			}
		}
	}
}