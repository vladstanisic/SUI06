using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System;
using System.IO;
using System.Text.RegularExpressions;

public static class P4DefaultValues
{
	public static readonly string P4_COMMAND;
	public static readonly string DEFAULT_SERVER = "ssl:82.102.5.111:1666";
	public static readonly string P4_LOGIN_COMMAND;
	public static readonly string P4_LOGIN_COMMAND2;

	static P4DefaultValues() {
		switch (Environment.OSVersion.Platform) {
		case PlatformID.Win32NT:
		case PlatformID.Win32S:
		case PlatformID.Win32Windows:
		case PlatformID.WinCE:
			P4_COMMAND = @"C:\Program Files\Perforce\p4.exe";
			P4_LOGIN_COMMAND = @"cmd";
			P4_LOGIN_COMMAND2 = @"";
			break;
		case PlatformID.MacOSX:
		case PlatformID.Unix:
			P4_COMMAND = @"/usr/local/bin/p4";
			P4_LOGIN_COMMAND = @"open";
			P4_LOGIN_COMMAND2 = @"/Applications/Utilities/Terminal.app";
			break;
		default:
			P4_COMMAND = "";
			UnityEngine.Debug.Log("Unknown platform");
			break;
		}	
			
	}


}

public class Command
{

	public static string Execute(string command, string p, string workingDirectory = "")
	{
		UnityEngine.Debug.Log(command + " " + p);
		ProcessStartInfo startInfo = new ProcessStartInfo(command, p);
		startInfo.WorkingDirectory = workingDirectory;
		startInfo.UseShellExecute = false;
		startInfo.RedirectStandardInput = true;
		startInfo.RedirectStandardOutput = true;
 
		Process process = new Process();
		process.StartInfo = startInfo;
		process.Start();
 
		string line = "";
		while (!process.StandardOutput.EndOfStream) {
			string output = process.StandardOutput.ReadLine();
			line = line + output;
			UnityEngine.Debug.Log(output);
		}
		process.WaitForExit();
		return line;
	}

	public static string [] Execute2(string command, string p)
	{
		ProcessStartInfo startInfo = new ProcessStartInfo(command, p);
		startInfo.UseShellExecute = false;
		startInfo.RedirectStandardInput = true;
		startInfo.RedirectStandardOutput = true;
 
		Process process = new Process();
		process.StartInfo = startInfo;
		process.Start();
 
		ArrayList lines = new ArrayList();
		while (!process.StandardOutput.EndOfStream) {
			string output = process.StandardOutput.ReadLine();
			lines.Add(output.Trim());
		}
		process.WaitForExit();
		process.Close();

		string[] retVal = new string[lines.Count];
		lines.CopyTo(retVal);
		return retVal;
	}

}

public class InitPerforceEnv : EditorWindow
{

	string server = P4DefaultValues.DEFAULT_SERVER;
	string p4command = P4DefaultValues.P4_COMMAND;
	string username = "";
	string password = "";



	[MenuItem("Custom/Perforce/Init...")]
	static void OpenInitWin()
	{
		InitPerforceEnv window = (InitPerforceEnv)EditorWindow.GetWindow(typeof(InitPerforceEnv), false, "Initialize Perforce Environment");
		window.Show();
	}

	void Awake()
	{
		username = EditorUserSettings.GetConfigValue("vcPerforceUsername");
		password = EditorUserSettings.GetConfigValue("vcPerforcePassword");
	}

	void OnGUI()
	{
		server = EditorGUILayout.TextField("Server: ", server);
		username = EditorGUILayout.TextField("Username:", username);
		password = EditorGUILayout.PasswordField("Password:", password);
		p4command = EditorGUILayout.TextField("P4 command: ", p4command);
		GUILayout.Space(20);
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Save")) {
			P4Set();
			this.Close();
		}
		GUILayout.Space(20);
		if (GUILayout.Button("Close")) {
			this.Close();
		}
		EditorGUILayout.EndHorizontal();

	}

	void P4Set()
	{
		EditorUserSettings.SetConfigValue("vcPerforceServer", server);
		Command.Execute(p4command, "set P4PORT=" + server);
		EditorUserSettings.SetConfigValue("vcPerforceUsername", username);
		Command.Execute(p4command, "set P4USER=" + username);
		Command.Execute(P4DefaultValues.P4_LOGIN_COMMAND, P4DefaultValues.P4_LOGIN_COMMAND2);
	}
}

public class NewPerforceWorkspace : EditorWindow
{

	

	string server = P4DefaultValues.DEFAULT_SERVER;
	string p4command = P4DefaultValues.P4_COMMAND;
	string username = "";
	string password = "";
	string workspace = "";
	string projectRoot = "";
	string streamName = "";

	public string[] streamNames = null;
	public string[] escapedStreamNames = null;
	public int streamsIndex = 0;



	[MenuItem("Custom/Perforce/Workspace...")]
	static void Init()
	{
		NewPerforceWorkspace window = (NewPerforceWorkspace)EditorWindow.GetWindow(typeof(NewPerforceWorkspace), false, "New Perforce Workspace");
		window.Show();
	}

	void Awake()
	{
		GetPerforceSettings();
		if (server == "" || username == "" || password == "") {
			EditorUtility.DisplayDialog("Error", "Perforce is not initialized.\nRun Custom->Perforce->Init...", "Close");
		}
		GetStreams();
		//for(int n=0; n<streamNames.Length; n++) {
		//	UnityEngine.Debug.Log(n.ToString() + " - " + streamNames[n]);
		//}
		projectRoot = Path.GetDirectoryName(Application.dataPath);
	}

	void  GetPerforceSettings()
	{
		username = EditorUserSettings.GetConfigValue("vcPerforceUsername");
		password = EditorUserSettings.GetConfigValue("vcPerforcePassword");
		workspace = EditorUserSettings.GetConfigValue("vcPerforceWorkspace");
		string tmpServer = EditorUserSettings.GetConfigValue("vcPerforceServer");
		if (tmpServer != "") {
			server = tmpServer;
		}

	}

	void  SetPerforceSettings()
	{
		
     
	}

	void OnGUI()
	{
		EditorGUILayout.BeginVertical();

		//password = EditorGUILayout.PasswordField("Password: ", password);
		streamsIndex = EditorGUILayout.Popup("Streams: ", streamsIndex, escapedStreamNames);
		workspace = EditorGUILayout.TextField("Workspace name: ", workspace);
		p4command = EditorGUILayout.TextField("P4 command: ", p4command);

		GUILayout.Space(20);
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Create")) {
			streamName = streamNames[streamsIndex];
			if (workspace == "" || streamName == "") {
				EditorUtility.DisplayDialog("Error", "Workspace and Stream both need values.", "Close");
				return;
			}
			string p4ignore = projectRoot + "/Assets/Editor/p4ignore-unity";
			if (Application.platform == RuntimePlatform.WindowsEditor) {
				// fix the path to use backslashes....
			}
			Command.Execute(p4command, "set P4IGNORE=" + p4ignore);
			Command.Execute(p4command, "set P4CLIENT=" + workspace);
			Command.Execute(p4command, "client -S " + streamName, projectRoot);
			EditorUserSettings.SetConfigValue("vcPerforceWorkspace", workspace);
			this.Close();

		}

		GUILayout.Space(20);
		if (GUILayout.Button("Refresh")) {
			GetPerforceSettings();
			GetStreams();
		}

		GUILayout.Space(20);
		if (GUILayout.Button("Close")) {
			this.Close();
		}

		EditorGUILayout.EndHorizontal();

		GUILayout.Space(20);
		EditorGUILayout.LabelField("Server: ", server);
		EditorGUILayout.LabelField("Username:", username);
		EditorGUILayout.LabelField("Workspace root:", projectRoot);
		EditorGUILayout.EndVertical();
		this.Repaint();
	}



	bool P4Init()
	{
		return true;

	}

  

	private string [] GetStreams()
	{
		string[] streamsRaw = Command.Execute2(p4command, "streams");
		streamNames = new string[streamsRaw.Length];
		escapedStreamNames = new string[streamsRaw.Length];
		for (int n = 0; n < streamsRaw.Length; n++) {
			string i = streamsRaw[n].Split(' ')[1];
			streamNames[n] = i;
			// A popup does not allow us to have slashes in the options, so replace with something that looks like a slash
			escapedStreamNames[n] = i.Replace("/", "\u2215");
		}
		return streamNames;
	}



}
