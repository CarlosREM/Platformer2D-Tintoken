using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowerObjective : MonoBehaviour
{
    [SerializeField] GameObject flowerMint;
    
    [SerializeField] GameObject flowerCream;

    [SerializeField] int currentLevel;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Objective")) {
            flowerMint.SetActive(true);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Finish") && flowerMint.activeSelf) {
            GetComponent<PlayerPlatformerController>().NoInputH = true;
            flowerMint.SetActive(false);
            flowerCream.SetActive(true);

            Invoke("ChangeScene", 3);
        }
    }

    void ChangeScene() {
        string sceneName = "Level" + (currentLevel+1);
        SceneManager.LoadScene(sceneName);
    }
}
