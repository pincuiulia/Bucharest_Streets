﻿using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform player; // Referință la obiectul de urmărit (player-ul)
    public float smoothing = 5f; // Viteza de urmărire a camerei
    public Vector3 offset; // Offset-ul camerei față de player
    public float zoomSpeed = 2f; // Viteza de zoom
    public float minZoom = 5f; // Nivelul minim de zoom
    public float maxZoom = 15f; // Nivelul maxim de zoom

    private Camera cam;
    private Tilemap tilemap;

    void Start()
    {
        // Setează offset-ul inițial dacă nu este setat în Inspector
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }

        // Referință la componenta Camera
        cam = GetComponent<Camera>();

        // Găsește obiectul Background și preia componenta Tilemap
        tilemap = GameObject.Find("Background").GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("Background not found or missing Tilemap component.");
        }
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("CameraController: Player is not set.");
            return;
        }

        // Poziția dorită a camerei
        Vector3 targetCamPos = player.position + offset;

        // Calcularea limitelor hărții
        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = cam.aspect * camHalfHeight;

        Bounds tilemapBounds = tilemap.localBounds;

        float minX = tilemapBounds.min.x + camHalfWidth;
        float maxX = tilemapBounds.max.x - camHalfWidth;
        float minY = tilemapBounds.min.y + camHalfHeight;
        float maxY = tilemapBounds.max.y - camHalfHeight;

        // Verifică dacă limitele sunt valide
        if (minX > maxX) minX = maxX = (minX + maxX) / 2;
        if (minY > maxY) minY = maxY = (minY + maxY) / 2;

        // Poziția camerei astfel încât player-ul să fie în centru, dar să nu depășească limitele hărții
        float clampedX = Mathf.Clamp(player.position.x + offset.x, minX, maxX);
        float clampedY = Mathf.Clamp(player.position.y + offset.y, minY, maxY);
        Vector3 clampedCamPos = new Vector3(clampedX, clampedY, targetCamPos.z);

        // Interpolează poziția camerei către poziția dorită
        transform.position = Vector3.Lerp(transform.position, clampedCamPos, smoothing * Time.deltaTime);
    }

    void Update()
    {
        // Ajustează mărirea camerei
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scrollData * zoomSpeed, minZoom, maxZoom);

        // Recalculează limitele pentru noua dimensiune a camerei
        FixedUpdate();
    }
}
