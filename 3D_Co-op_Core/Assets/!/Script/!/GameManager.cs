using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //inventory
    public Inventory inventory { get; private set; }
    public EquipmentManager equipmentManager { get; private set; }

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
        inventory = Object.FindFirstObjectByType<Inventory>();
        equipmentManager = Object.FindFirstObjectByType<EquipmentManager>();
        player = Object.FindFirstObjectByType<Player>();
        playerAction = Object.FindFirstObjectByType<PlayerAction>();
        playerLook = Object.FindFirstObjectByType<PlayerLook>();
        playerMovement = Object.FindFirstObjectByType<PlayerMovement>();
    }
}
