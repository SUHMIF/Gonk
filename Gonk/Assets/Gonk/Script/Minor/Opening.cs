using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public GameObject openingCanvas;
    public GameObject openingModel;

    public GameObject characterSelectCanvas;
    public GameObject characterSelectModel;

    public AudioSource menuSFX;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            changeCanvas();
            menuSFX.Play();
        }
    }

    private void changeCanvas()
    {
        openingCanvas.SetActive(false);
        openingModel.SetActive(false);

        characterSelectCanvas.SetActive(true);
        characterSelectModel.SetActive(true);
    }
}