using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pong.Menu
{
    public class ChangeInput : MonoBehaviour
    {
        EventSystem system;
        [SerializeField] private Selectable firstInput;
        [SerializeField] private Button submitButton;

        // Start is called before the first frame update
        void Start()
        {
            system = EventSystem.current;
            firstInput.Select();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
            {
                SelectPrevious();
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                SelectNext();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                Submit();
            }
        }

        private void Submit()
        {
            submitButton.onClick.Invoke();
            Debug.Log("Button Passed!");
        }

        private void SelectNext()
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {
                next.Select();
            }
        }

        private void SelectPrevious()
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();

            if (previous != null)
            {
                previous.Select();
            }
        }
    }
}