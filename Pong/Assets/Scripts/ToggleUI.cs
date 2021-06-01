using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] private GameObject toggleObject;
    [SerializeField] private GameObject[] otherContainers;

    public void ToggleGameObject() //Called by buttons
    {
        toggleObject.SetActive(!toggleObject.activeSelf);

        foreach (GameObject item in otherContainers)
        {
            item.SetActive(false);
        }
    }
}
