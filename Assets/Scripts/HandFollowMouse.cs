using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollowMouse : MonoBehaviour
{
    public Transform character; // Reference to the character's transform
    float handDistance = 0.4f; // Distance from the character to the hand

    public Texture2D targetCursor;

    private void Awake()
    {
        Cursor.SetCursor(targetCursor, Vector2.zero, CursorMode.Auto);
        character = transform.parent.transform;
    }

    void Update()
    {
        // Get the mouse position in the world
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 if your game is 2D

        // Calculate the direction from the character to the mouse
        Vector3 direction = (mousePosition - character.position).normalized;

        // Calculate the new hand position
        Vector3 handPosition = character.position + direction * handDistance;
        //handPosition = new Vector2(handPosition.x, handPosition.y * 0.75f +0.25f);

        // Update the hand's position
        transform.position = handPosition;
    }
}
