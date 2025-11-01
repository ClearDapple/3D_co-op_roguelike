using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public static ScriptManager Instance { get; private set; }

    //game manager
    public GameManager gameManager { get; private set; }

    //inventory
    public Inventory inventory { get; private set; }
    public ItemEquipmentManager itemEquipmentManager { get; private set; }
    public ItemDropManager itemDropManager { get; private set; }

    //player
    public Player player { get; private set; }
    public PlayerAction playerAction { get; private set; }
    public PlayerLook playerLook { get; private set; }
    public PlayerMovement playerMovement { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        gameManager = Object.FindFirstObjectByType<GameManager>();

        inventory = Object.FindFirstObjectByType<Inventory>();
        itemEquipmentManager = Object.FindFirstObjectByType<ItemEquipmentManager>();
        itemDropManager = Object.FindFirstObjectByType<ItemDropManager>();

        player = Object.FindFirstObjectByType<Player>();
        playerAction = Object.FindFirstObjectByType<PlayerAction>();
        playerLook = Object.FindFirstObjectByType<PlayerLook>();
        playerMovement = Object.FindFirstObjectByType<PlayerMovement>();
    }
}
