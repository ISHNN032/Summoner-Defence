﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class DialogManager {
    public static List<Dialog> ReadDialog(string dialog_name)
    {
        MemoryStream ms = new MemoryStream(Resources.Load<TextAsset>("Dialogs/Main_01")/*string.Format("Dialogs/{0}", dialog_name))*/.bytes);
        StreamReader sr = new StreamReader(ms, Encoding.Default);

        List<Dialog> dialog = new List<Dialog>();

        while (sr.Peek() != -1)
        {
            string D_String = sr.ReadLine();
            if (D_String == "") continue;

            string[] D_data = D_String.Split('|');
            dialog.Add(new Dialog(D_data[0], D_data[1]));
            Debug.Log(D_data[0] + " | "+ D_data[1]);
        }
        return dialog;
    }
}

public struct Dialog
{
    public string name { get; internal set; }
    public string script { get; internal set; }

    public Dialog(string name, string script) : this()
    {
        this.name = name;
        this.script = script;
    }
}