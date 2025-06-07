// 6/6/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public Renderer targetRenderer;
    public float horizontalSpeed = 0.2f;
    public int trackCount = 3;
    public float verticalStep = 1.0f / 3.0f;

    private Vector2 uvOffset = Vector2.zero;
    public int currentTrack = 2;

    void Start()
    {
        if (targetRenderer == null)
        {
            Debug.LogError("Target Renderer is not assigned!");
        }
        else
        {
            verticalStep = 1.0f / trackCount;
        }
    }

    void Update()
    {
        if (targetRenderer != null)
        {
            uvOffset.x += horizontalSpeed * Time.deltaTime;

            // Update the vertical offset based on the current track
            uvOffset.y = currentTrack * verticalStep;

            // Set the texture offset for URP materials
            targetRenderer.material.SetTextureOffset("_BaseMap", uvOffset);
        }
    }
}
