using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    public GameObject obstacles, foreground;
    void Start() {
        setIsOpen(isOpen);
    }

    public void Open() {
        obstacles.SetActive(false);
        foreground.SetActive(false);
        isOpen = true;
    }
    public void Close() {
        obstacles.SetActive(true);
        foreground.SetActive(true);
        isOpen = false;
    }

    public void setIsOpen(bool newState) {
        if (newState)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Toggle() {
        setIsOpen(!isOpen);
    }
}
