using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] private GameObject toggleObject;

    public void ToggleGameObject() //Called by buttons
    {
        toggleObject.SetActive(!toggleObject.activeSelf);
    }
}
