using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public enum ImageFormat
{
    PNG,
    Jpeg,
    TGA
}
public static class Utilities
{
    public static bool CheckPlatform(RuntimePlatform platform)
    {
        return Application.platform == platform;
    }
    public static void SaveSerializedObject(string filename, object obj)
    {
        BinaryFormatter bf = new();
        FileStream file;
        string filepath = Application.persistentDataPath + "/" + filename;
        if (File.Exists(filepath))
        {
            file = File.Open(filepath, FileMode.Open);
        } else
        {
            file = File.Create(filepath);
        }
        bf.Serialize(file, obj);
        file.Close();
    }

    public static bool TryLoadSerializedObject(string filename, out object obj)
    {
        BinaryFormatter bf = new();
        string filepath = Application.persistentDataPath + '/' + filename;
        obj = null;
        if (File.Exists(filepath))
        {
            Debug.Log("Loading from " + filepath);
            FileStream file = File.Open(filepath, FileMode.Open);
            obj = bf.Deserialize(file);
            file.Close();
            return true;
        }
        Debug.Log("File not found: " + filepath);
        return false;
    }

    public static byte[] SerializeObject(object obj)
    {
        try
        {
            BinaryFormatter bf = new();
            using MemoryStream ms = new();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        catch (System.Runtime.Serialization.SerializationException e)
        {
            Debug.LogError("Serialization failed: " + e.Message);
            return null;
        }
        catch (System.Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
            return null;
        }
    }

    public static T DeserializeObject<T>(byte[] data)
    {
        try
        {
            BinaryFormatter bf = new();
            using MemoryStream ms = new(data);
            object obj = bf.Deserialize(ms);
            return (T)obj;
        }
        catch (System.Runtime.Serialization.SerializationException e)
        {
            Debug.LogError("Deserialization failed: " + e.Message);
            return default;
        }
        catch (System.Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
            return default;
        }
    }

    public static byte[] TakeScreenshot(ImageFormat format, Camera cam = null)
    {

        Debug.Log("TakeScreenShot: " + (cam != null) + " " + format);
        if (cam != null || format != ImageFormat.PNG)
        {
            Texture2D screenshot;

            if (cam != null)
            {
                //Debug.Log("TakeScreenShot: " + cam.pixelWidth + " " + cam.pixelHeight);
                var tmp = RenderTexture.GetTemporary(cam.pixelWidth, cam.pixelHeight);
                var cache = cam.targetTexture;
                cam.targetTexture = tmp;
                cam.Render();

                RenderTexture.active = tmp;
                screenshot = new Texture2D(tmp.width, tmp.height, TextureFormat.RGBA32, false);
                screenshot.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);

                RenderTexture.active = null;

                cam.targetTexture = cache;
                tmp.Release();
            }
            else
            {
                Debug.Log("Camera is null");
                screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
                screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                screenshot.Apply();
            }

            byte[] bytes;
            switch (format)
            {
                case ImageFormat.PNG:
                    bytes = screenshot.EncodeToPNG();
                    break;
                case ImageFormat.Jpeg:
                    bytes = screenshot.EncodeToJPG();
                    break;
                case ImageFormat.TGA:
                    bytes = screenshot.EncodeToTGA();
                    break;
                default:
                    UnityEngine.Object.DestroyImmediate(screenshot);
                    return null;
            }

            

            UnityEngine.Object.DestroyImmediate(screenshot);
            return bytes;
        }
        return null;

    }
}
