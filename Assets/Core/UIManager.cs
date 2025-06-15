using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private MainMenu startMenu;
        [SerializeField] private UIDocument uiDocument;

        private InventoryManager inventoryManager;

        void Start()
        {
            inventoryManager = InventoryManager.Instance;
            SetupUI();
            startMenu.Render();
        }

        private void SetupUI()
        {
            VisualElement root = uiDocument.rootVisualElement;

            Button saveButton = root.Q<Button>("save");
            saveButton.RegisterCallback<ClickEvent>((evt) => SaveHandler());

            Button backButton = root.Q<Button>("home");
            backButton.RegisterCallback<ClickEvent>((evt) => BackButtonClick());
        }

        private void SaveHandler()
        {
            inventoryManager.Save();
        }

        private void BackButtonClick()
        {
            startMenu.Render();
        }
    }
}
