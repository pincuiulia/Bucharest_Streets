using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform player; // Referinta la obiectul de urmarit (player-ul)
    public float smoothing = 5f; // Viteza de urmarire a camerei
    public Vector3 offset; // Offset-ul camerei fata de player
    public float zoomSpeed = 2f; // Viteza de zoom
    public float minZoom = 5f; // Nivelul minim de zoom
    public float maxZoom = 15f; // Nivelul maxim de zoom

    private Camera cam;
    private Tilemap tilemap;

    void Start()
    {
        // Setam offset-ul initial daca nu este setat in Inspector
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }

        // Referinta la componenta Camera
        cam = GetComponent<Camera>();

        // Gaseste obiectul Ground si preia componenta Tilemap
        tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("Ground not found or missing Tilemap component.");
        }
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("CameraController: Player is not set.");
            return;
        }

        // Pozitia dorita a camerei
        Vector3 targetCamPos = player.position + offset;
        Debug.Log($"Target Camera Position: {targetCamPos}");

        // Calcularea limitelor hartii
        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = cam.aspect * camHalfHeight;
        Debug.Log($"Camera Half Width: {camHalfWidth}, Camera Half Height: {camHalfHeight}");

        Bounds tilemapBounds = tilemap.localBounds;
        Debug.Log($"Tilemap Bounds: {tilemapBounds}");

        float minX = tilemapBounds.min.x + camHalfWidth;
        float maxX = tilemapBounds.max.x - camHalfWidth;
        float minY = tilemapBounds.min.y + camHalfHeight;
        float maxY = tilemapBounds.max.y - camHalfHeight;

        Debug.Log($"Clamping Values: minX={minX}, maxX={maxX}, minY={minY}, maxY={maxY}");

        // Verificam daca limitele sunt valide
        if (minX > maxX) minX = maxX = (minX + maxX) / 2;
        if (minY > maxY) minY = maxY = (minY + maxY) / 2;

        float clampedX = Mathf.Clamp(targetCamPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetCamPos.y, minY, maxY);
        Vector3 clampedCamPos = new Vector3(clampedX, clampedY, targetCamPos.z);

        Debug.Log($"Clamped Camera Position: {clampedCamPos}");

        // Interpolam pozitia camerei catre pozitia dorita
        transform.position = Vector3.Lerp(transform.position, clampedCamPos, smoothing * Time.deltaTime);
    }

    void Update()
    {
        // Ajustam marirea camerei
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scrollData * zoomSpeed, minZoom, maxZoom);

        // Recalculam limitele pentru noua dimensiune a camerei
        FixedUpdate();
    }
}