using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{

    public class Startup : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private SessionManager sessionManager;
        [SerializeField] private UIDocument uIDocument;
        [SerializeField] private CraftingManager craftingManager;


        void Awake()
        {
            Instantiate(mainCamera);
            ItemDatabase.BuildIndex();
            Instantiate(inventoryManager);
            Instantiate(sessionManager);
            Instantiate(craftingManager);
            Instantiate(uIDocument);
        }
    }
}