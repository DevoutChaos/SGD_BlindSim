using UnityEngine;
using System.Collections;

public class GraphMoveThingy : MonoBehaviour {
    //Declarations
    public GameObject moveRight;
    public GameObject moveLeft;
    public GameObject moveDown;
    public GameObject moveUp;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight.SetActive(true);
            this.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeft.SetActive(true);
            this.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveUp.SetActive(true);
            this.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDown.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
