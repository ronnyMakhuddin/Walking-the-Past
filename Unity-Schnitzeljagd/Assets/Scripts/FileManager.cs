using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;


public static class FileManager
{
    public static void SaveToJSON<T>(List<T> save, string filename)
    {
        Debug.Log(GetPath(filename));
        string toWrite = JsonHelper.ToJson<T>(save);
        WriteFile(GetPath(filename), toWrite);
    }

    public static List<T> ReadFromJSON<T>(string filename)
    {
        string read = ReadFile(GetPath(filename));
        if (string.IsNullOrEmpty(read) || read == "{}")
        {
            Debug.Log("Was not able to properly read from json: " + filename);
            return new List<T>();
        }
        return JsonHelper.FromJson<T>(read).ToList();
    }

    private static string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    private static void WriteFile(string path, string toWrite)
    {
        FileStream stream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(stream))
        {
            writer.Write(toWrite);
        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string read = reader.ReadToEnd();
                return read;
            }
        }
        return "";
    }
}


public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(List<T> list)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = list;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(List<T> list, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = list;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}

/*
public static class FileManager
{
    public static void SaveToJSON<T>(T save, string filename)
    {
        Debug.Log(GetPath(filename));
        string toWrite = JsonDictionary.ToJson(save);
        WriteFile(GetPath(filename), toWrite);
    }

    public static Dictionary<string, string> ReadFromJSON(string filename)
    {
        string read = ReadFile(GetPath(filename));
        if (string.IsNullOrEmpty(read) || read == "{}")
        {
            Debug.Log("Was not able to properly read from json: " + filename);
            return new Dictionary<string, string> ();
        }
        return JsonDictionary.FromJson(read);
    }

    private static string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    private static void WriteFile(string path, string toWrite)
    {
        FileStream stream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(stream))
        {
            writer.Write(toWrite);
        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string read = reader.ReadToEnd();
                return read;
            }
        }
        return "";
    }
}

public static class JsonDictionary
{
    public static Dictionary<string, string> FromJson(string json)
    {
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        return wrapper.Items;
    }

    public static string ToJson(Dictionary<string, string> dictionary)
    {
        Wrapper wrapper = new Wrapper();
        wrapper.Items = dictionary;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson(Dictionary<string, string> dictionary, bool prettyPrint)
    {
        Wrapper wrapper = new Wrapper();
        wrapper.Items = dictionary;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T<
    {
        public List<T> Items;
    }
    
}*/


