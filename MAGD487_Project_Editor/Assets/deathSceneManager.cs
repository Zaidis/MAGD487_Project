using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class deathSceneManager : MonoBehaviour
{
    public GameObject[] objects;
    private void Awake() {
        //deletes player and main canvas
        Destroy(FindObjectOfType<PlayerMovement>().gameObject);
        Destroy(FindObjectOfType<InventoryManager>().transform.parent.gameObject);
    }
    private void Start() {
        GameObject[] newObjects = FindObjectsOfType<GameObject>();
        int num = 0;
        while(newObjects.Length != objects.Length) {
            bool noBueno = false;
            for (int j = 0; j < objects.Length; j++) {
                if (newObjects[num] != objects[j]) {
                    noBueno = true;
                }
                else {
                    noBueno = false;
                    num++;
                }
            }
            if (noBueno) {
                Destroy(newObjects[num].gameObject);
            }
        }
    }

    public void MainMenu() {
        SceneManager.LoadScene(0); //main menu
    }
}
