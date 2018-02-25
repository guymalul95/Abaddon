using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCursor : MonoBehaviour
{
    void Awake()
    {
        // Cursor.SetCursor() later :)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}