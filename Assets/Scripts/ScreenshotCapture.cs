using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenshotCapture : MonoBehaviour
{
    [SerializeField]
    private Camera cam = null;

    public void TakeScrenshot()
    {
        string filename = string.Format("Assets/Output/Screenshot_{0}.png", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        TakeTransparentScreenshot(cam, Screen.width, Screen.height, filename);
    }

    public void TakeTransparentScreenshot(Camera cam, int width, int height, string savePath)
    {
        var bak_cam_targetTexture = cam.targetTexture;
        var bak_cam_clearFlags = cam.clearFlags;
        var bak_RenderTexture_active = RenderTexture.active;

        var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
        var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
        var grab_area = new Rect(0, 0, width, height);

        RenderTexture.active = render_texture;
        cam.targetTexture = render_texture;
        cam.clearFlags = CameraClearFlags.SolidColor;

        cam.backgroundColor = Color.clear;
        cam.Render();
        tex_transparent.ReadPixels(grab_area, 0, 0);
        tex_transparent.Apply();

        byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
        File.WriteAllBytes(savePath, pngShot);

        cam.clearFlags = bak_cam_clearFlags;
        cam.targetTexture = bak_cam_targetTexture;
        RenderTexture.active = bak_RenderTexture_active;
        RenderTexture.ReleaseTemporary(render_texture);
        Destroy(tex_transparent);
    }
}
