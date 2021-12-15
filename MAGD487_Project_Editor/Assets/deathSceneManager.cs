using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class deathSceneManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject[] newObjects;
    private void Awake() {
        //deletes player and main canvas
       // Destroy(FindObjectOfType<PlayerMovement>().gameObject);
       // Destroy(FindObjectOfType<InventoryManager>().transform.parent.gameObject);
    }
    private void Start() {
        newObjects = FindObjectsOfType<GameObject>();
        int num = 0;
        /*while(newObjects.Length != objects.Length) {
            bool bueno = false;
            for (int j = 0; j < objects.Length; j++) {
                if (newObjects[num] == objects[j]) {
                    bueno = true;
                    break;
                }
            }
            if (!bueno) {
                Destroy(newObjects[num].gameObject);
            } else {
                num++;
            }
        } */

        foreach(GameObject temp in newObjects) {
            bool bueno = false;
            for (int j = 0; j < objects.Length; j++) {
                if (temp == objects[j]) {
                    bueno = true;
                    break;
                }
            }
            if (!bueno) {
                Destroy(temp.gameObject);
            } else {
                continue;
            }
        }
    }

    public void MainMenu() {
        SceneManager.LoadScene(0); //main menu
    }
}
