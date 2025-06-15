using UnityEngine;
namespace Core
{

    public class SessionManager : MonoBehaviour
    {
        public static SessionManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(this);
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}