using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class CraftingRenderer : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset menu;
        [SerializeField] private VisualTreeAsset itemCard;
        [SerializeField] private VisualTreeAsset inventoryRow;

        private VisualElement root;
        private CraftingManager craftingManager;
        private InventoryManager inventoryManager;
        private VisualElement craftedBG;
        private Label craftedText;
        private VisualElement craftedImage;
        private ListView inventoryView;

        void Awake()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
        }

        public void Render()
        {
            craftingManager = CraftingManager.Instance;
            inventoryManager = InventoryManager.Instance;
            SetupUI();

            List<List<ResourceQuantity>> displayList = inventoryManager.resources
            .Where(r => r.Value > 0)
            .OrderByDescending(r => r.Key.Rarity)
            .Select(el => new ResourceQuantity { Ressorce = el.Key, Quantity = el.Value })
            .GroupBy(el => el.Ressorce.Rarity)
            .Select(group => group.ToList())
            .ToList();
            SetupListView(displayList);
        }

        void RefreshListView()
        {
            List<List<ResourceQuantity>> displayList = inventoryManager.resources
                .Where(r => r.Value > 0)
                .OrderByDescending(r => r.Key.Rarity)
                .Select(el => new ResourceQuantity { Ressorce = el.Key, Quantity = el.Value })
                .GroupBy(el => el.Ressorce.Rarity)
                .Select(group => group.ToList())
                .ToList();
            Debug.Log($"voidrium quantity: {displayList[0][0].Quantity}");
            inventoryView.itemsSource = displayList;
            inventoryView.Rebuild();
        }

        public void SetupListView(List<List<ResourceQuantity>> displayList)
        {
            inventoryView = root.Q<ListView>("inventory-list");

            inventoryView.itemsSource = displayList;
            inventoryView.makeItem = () => inventoryRow.Instantiate();
            inventoryView.bindItem = (element, index) =>
            {
                element.Clear();
                element.style.flexDirection = FlexDirection.Row;
                element.style.paddingBottom = 24;


                List<ResourceQuantity> itemRow = inventoryView.itemsSource[index] as List<ResourceQuantity>;

                for (int i = 0; i < itemRow.Count; i++)
                {
                    InventoryItem item = itemRow[i].Ressorce;
                    int quantity = itemRow[i].Quantity;

                    VisualElement itemElement = itemCard.Instantiate();
                    Label label = itemElement.Q<Label>("label");
                    Label quantityDisplay = itemElement.Q<Label>("quantity");
                    Button background = itemElement.Q<Button>();
                    VisualElement image = itemElement.Q<VisualElement>("image");

                    label.text = item.Name;
                    quantityDisplay.text = quantity.ToString();
                    background.style.backgroundImage = new StyleBackground(item.Rarity.Background);
                    image.style.backgroundImage = new StyleBackground(item.Icon);

                    background.RegisterCallback<ClickEvent>((evt) => InventoryClick(item));
                    element.Add(itemElement);
                }
            };
        }

        public void SetupUI()
        {
            root.Q<Label>("page-label").text = "Crafting Menu";
            VisualElement main = root.Q<VisualElement>("main");
            main.Clear();
            menu.CloneTree(main);
            Button craftButton = main.Q<Button>("craft");
            craftButton.RegisterCallback<ClickEvent>(OnCraftClik);

            VisualElement craftedDisplay = main.Q<VisualElement>("product-holder");
            craftedBG = craftedDisplay.Q<Button>();
            craftedImage = craftedDisplay.Q<VisualElement>("image");
            craftedText = craftedDisplay.Q<Label>("label");
            craftedDisplay.Q<Label>("quantity").text = "0";

        }

        public void OnCraftClik(ClickEvent e)
        {
            InventoryItem value = craftingManager.Craft();
            Debug.Log($"crafted: {value.Name}");
            value.ChangeQuantity(inventoryManager, 1);
            inventoryManager.Save();

            RefreshListView();
            craftedBG.style.backgroundImage = new StyleBackground(value.Rarity.Background);
            craftedImage.style.backgroundImage = new StyleBackground(value.Icon);
            craftedText.text = value.Name;
            root.Q<VisualElement>("container").Clear();
        }

        public VisualElement CreateItemCard(ResourceQuantity rQ)
        {
            Item item = rQ.Ressorce;

            VisualElement card = new();
            itemCard.CloneTree(card);

            Label label = card.Q<Label>("label");
            Label quantity = card.Q<Label>("quantity");
            Button background = card.Q<Button>();
            VisualElement image = card.Q<VisualElement>("image");

            label.text = item.Name;
            quantity.text = rQ.Quantity.ToString();
            background.style.backgroundImage = new StyleBackground(item.Rarity.Background);
            image.style.backgroundImage = new StyleBackground(item.Icon);

            // background.RegisterCallback<ClickEvent, Item>(InventoryClick, item);

            return card;
        }

        public void InventoryClick(InventoryItem item)
        {
            if (item is not Item)
            {
                Debug.Log($"the provided inventory item {item.Name} cannot be used as crafting material!");
                return;
            }

            Item resource = item as Item;

            Debug.Log($"item {resource.name} clicked");
            bool canBeAdded = craftingManager.AddResource(resource);
            if (!canBeAdded) return;

            if (inventoryManager.HasEnough(resource, 1))
            {

                inventoryManager.ChangeQuantity(resource, -1);
                VisualElement itemCard = CreateSelectedItemCard(new ResourceQuantity { Ressorce = resource, Quantity = 1 });
                RefreshListView();

                root.Q<VisualElement>("container").Add(itemCard);
            }
            else
            {
                Debug.Log("Not enough resources available.");
            }
        }

        public VisualElement CreateSelectedItemCard(ResourceQuantity rQ)
        {
            Item item = rQ.Ressorce;
            Debug.Log("creating item card");
            VisualElement card = new();
            itemCard.CloneTree(card);

            Label label = card.Q<Label>("label");
            Label quantity = card.Q<Label>("quantity");
            Button background = card.Q<Button>();
            VisualElement image = card.Q<VisualElement>("image");

            label.text = item.Name;
            quantity.text = rQ.Quantity.ToString();
            background.style.backgroundImage = new StyleBackground(item.Rarity.Background);
            image.style.backgroundImage = new StyleBackground(item.Icon);

            background.RegisterCallback<ClickEvent>(evt => OnSelectedClick(card, item));

            return card;
        }

        public void OnSelectedClick(VisualElement itemCard, Item resource)
        {
            root.Q<VisualElement>("container").Remove(itemCard);

            craftingManager.RemoveResource(resource);
            inventoryManager.ChangeQuantity(resource, 1);
            RefreshListView();
        }

    }
}