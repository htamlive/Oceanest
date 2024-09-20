using UnityEngine;
using System.IO;
using System.Collections;
using SFB;
using System;

public static class SteganographyScreenshot
{
    public static IEnumerator CaptureAndEmbedData(Camera cam)
    {
        yield return new WaitForEndOfFrame();
        byte[] image = Utilities.TakeScreenshot(ImageFormat.PNG, cam);
        //yield return new WaitForSeconds(1f);

        var chosen_folders = StandaloneFileBrowser.OpenFolderPanel("Choose Folder", "", false);
        if (chosen_folders.Length == 0)
        {
            yield break;
        }
        string folder = chosen_folders[0];

        string screenshotPath = Path.Combine(folder, "screenshot.png");
        
        
        //File.WriteAllBytes(screenshotPath, image);


        
        yield return new WaitForSeconds(1f);

        Texture2D screenshot = LoadTexture(image);

        var gameData = GameDataManager.GetGameData();
        byte[] dataBytes = Utilities.SerializeObject(gameData);
        EmbedData(screenshot, dataBytes);

        SaveTextureAsPNG(screenshot, screenshotPath);
    }

    private static Texture2D LoadTexture(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        return LoadTexture(fileData);
    }

    private static Texture2D LoadTexture(byte[] fileData) {
        Texture2D texture = new(2, 2);
        texture.LoadImage(fileData);
        return texture;

    }

    private static void EmbedData(Texture2D texture, byte[] dataBytes)
    {
        if(dataBytes.Length * 8 > texture.width * texture.height)
        {
            Debug.LogError("Data is too large to embed in the texture");
            return;
        }

        Color32[] pixels = texture.GetPixels32();

        for (int i = 0; i < dataBytes.Length; i++)
        {
            byte dataByte = dataBytes[i];
            for (int bit = 0; bit < 8; bit++)
            {
                int pixelIndex = i * 8 + bit;
                Color32 pixel = pixels[pixelIndex];
                pixel.a = (byte)((pixel.a & 0xFE) | ((dataByte >> bit) & 1));
                pixels[pixelIndex] = pixel;
            }
        }

        texture.SetPixels32(pixels);
        texture.Apply();
    }

    public static byte[] ExtractDataFromImage(string filePath)
    {
        Texture2D texture = LoadTexture(filePath);
        Color32[] pixels = texture.GetPixels32();
        byte[] dataBytes = new byte[pixels.Length / 8];

        for (int i = 0; i < dataBytes.Length; i++)
        {
            byte dataByte = 0;
            for (int bit = 0; bit < 8; bit++)
            {
                int pixelIndex = i * 8 + bit;
                Color32 pixel = pixels[pixelIndex];
                dataByte |= (byte)((pixel.a & 1) << bit);
            }
            dataBytes[i] = dataByte;
        }

        return dataBytes;
    }

    private static void SaveTextureAsPNG(Texture2D texture, string filePath)
    {
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }

    public static T ExtractGameDataFromImage<T>(string filePath)
    {
        byte[] dataBytes = ExtractDataFromImage(filePath);
        return Utilities.DeserializeObject<T>(dataBytes);
    }





    internal static GameData LoadData()
    {
        var choosen_paths = StandaloneFileBrowser.OpenFilePanel("Choose Image", "", "png", false);

        if (choosen_paths.Length == 0)
        {
            return null;
        }

        string filePath = choosen_paths[0];

        return ExtractGameDataFromImage<GameData>(filePath);
    }
}
