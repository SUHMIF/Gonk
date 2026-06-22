using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

// SUHMIF Gonk Character Select Script

public class CharacterSelect : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] int playerNumber; // Determines who's who
    public static int[] activeCharacter = new int[4]; // Estentially a way to assign what character
    [SerializeField] GameObject[] activePrefab = new GameObject[4]; // List of the gameobjects to change and acts as where to spawn the new ones
    [SerializeField] GameObject[] character3DModels; // List of the character models the player can move through to choose

    [Header("Sound Effects")]
    public AudioSource buttonSFX; // Audio que for input being acted upon

    [Header("Rewired")]
    public int playerId;
    private Rewired.Player player;

    [Header("Player 3 & 4")]
    public static bool isPlayer3Active;
    public static bool isPlayer4Active;
    public GameObject[] joinText;

    //-----------------------------------Rewired & Status-------------------------
    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        isPlayer3Active = false;
        isPlayer4Active = false;
    }

    //-----------------------------------Input Behaviour-------------------------
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Once all players are happy, they press spacebar and it picks between the two levels for them to play
        {
            SceneManager.LoadScene(UnityEngine.Random.Range(1, 3));
        }

        if (playerNumber == 0 && Input.GetKeyDown(KeyCode.A)) // Player One Input for changing to Previous character
        {
            previousCharacter(playerNumber); // Uses the player number so code doesn't need to be repeated and works for both players
        }

        if (playerNumber == 0 && Input.GetKeyDown(KeyCode.D)) // Player One Input for changing to Next character
        {
            nextCharacter(playerNumber);
        }

        if (playerNumber == 1 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            previousCharacter(playerNumber);
        }

        if (playerNumber == 1 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextCharacter(playerNumber);
        }

        // Player 3
        if (playerNumber == 2 && player.GetButtonDown("Join") && isPlayer3Active == false)
        {
            isPlayer3Active = true;
            joinText[0].SetActive(false);
            activePrefab[playerNumber].SetActive(true);
        }

        if (playerNumber == 2 &&  player.GetButtonDown("Next") && isPlayer3Active == true)
        {
            nextCharacter(playerNumber);
        }

        if (playerNumber == 2 && player.GetButtonDown("Previous") && isPlayer3Active == true)
        {
            previousCharacter(playerNumber);
        }

        // Player 4
        if (playerNumber == 3 && player.GetButtonDown("Join") && isPlayer3Active == true && isPlayer4Active == false)
        {
            isPlayer4Active = true;
            joinText[1].SetActive(false);
            activePrefab[playerNumber].SetActive(true);
        }

        if (playerNumber == 3 && player.GetButtonDown("Next") && isPlayer4Active == true)
        {
            nextCharacter(playerNumber);
        }

        if (playerNumber == 3 && player.GetButtonDown("Previous") && isPlayer4Active == true)
        {
            previousCharacter(playerNumber);
        }
    }

    //-----------------------------------Cycling Through Characters-------------------------
    private void previousCharacter(int playerNumber) // Avoids repeated code and works for each Player!
    {
        Destroy(activePrefab[playerNumber]); // Removes previous preview model from screen
        activeCharacter[playerNumber]--; // Moves back through list of models

        if (activeCharacter[playerNumber] < 0) // Stops from breaking
        {
            activeCharacter[playerNumber] += character3DModels.Length;
        }

        activePrefab[playerNumber] = Instantiate(character3DModels[activeCharacter[playerNumber]], transform); // Spawns the new model 
        buttonSFX.Play(); // Sound cue to let player know input has worked
    }

    private void nextCharacter(int playerNumber)
    {
        Destroy (activePrefab[playerNumber]);
        activeCharacter[playerNumber] = (activeCharacter[playerNumber] + 1) % character3DModels.Length;

        activePrefab[playerNumber] = Instantiate(character3DModels[activeCharacter[playerNumber]], transform);
        buttonSFX.Play();
    }
}