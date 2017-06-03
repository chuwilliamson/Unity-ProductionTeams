// Convert the 2D position of the mouse into a
// 3D position.  Display these on the game window.

using UnityEngine;

public class DebugUI : MonoBehaviour
{
    void OnGUI()
    {
        var c = Camera.main;
        var e = Event.current;
        var mousePos = new Vector2
        {
            x = e.mousePosition.x,
            y = c.pixelHeight - e.mousePosition.y
        };

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.

        var p = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, c.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + c.pixelWidth + ":" + c.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + p.ToString("F3"));
        GUILayout.EndArea();
    }
}