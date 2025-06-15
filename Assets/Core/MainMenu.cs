using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset menu;
        [SerializeField] private CraftingRenderer craftingRenderer;

        private VisualElement main;
        [SerializeField] private string itemCodeToSpawn;
        private InventoryManager inventoryManager;

        void Awake()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            root.Q<Label>("page-label").text = "Main Menu";
            main = root.Q<VisualElement>("main");
            inventoryManager = InventoryManager.Instance;
        }

        public void Render()
        {
            main.Clear();
            menu.CloneTree(main);
            SetupUI();
        }

        private void SetupUI()
        {
            main.Q<Button>("play").RegisterCallback<ClickEvent>(evt => LoadGameScene());
            main.Q<Button>("crafting").RegisterCallback<ClickEvent>(evt => SwitchToCraftingUI());
            TextField itemCheatInput = main.Q<TextField>("item-spawn-input");
            itemCheatInput.RegisterCallback<ChangeEvent<string>>(OnTextChange);
            main.Q<Button>("item-spawn").RegisterCallback<ClickEvent>(evt => SpawnItem());
        }

        private void LoadGameScene()
        {
            SceneManager.LoadScene("SpaceCombat");
        }

        private void SwitchToCraftingUI()
        {
            craftingRenderer.Render();
        }

        private void OnTextChange(ChangeEvent<string> evt)
        {
            itemCodeToSpawn = evt.newValue;
        }

        private void SpawnItem()
        {
            Item item = ItemDatabase.Get(itemCodeToSpawn) as Item;
            if (item == null) return;
            item.ChangeQuantity(inventoryManager, 5);
        }
    }


}
