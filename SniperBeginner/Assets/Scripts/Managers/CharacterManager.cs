using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] Player playerPrefab;
    public Player Player { get; set; }


    private void Start() 
    {
        if(!Player)
            Instantiate(playerPrefab);
    }

}