using UnityEngine;
using System.Collections;

public class GraphMoveThingy : MonoBehaviour {
    //Declarations
    public GameObject moveRight;
    public GameObject moveLeft;
    public GameObject moveDown;
    public GameObject moveUp;
    public GameObject bGHolder;
    public Texture bGImage;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ((!bGHolder.Equals(null))&&(!bGImage.Equals(null)))
        {
            bGHolder.GetComponent<GUITexture>().texture = bGImage;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!moveRight.Equals(null))
            {
                moveRight.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!moveLeft.Equals(null))
            {
                moveLeft.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!moveUp.Equals(null))
            {
                moveUp.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!moveDown.Equals(null))
            {
                moveDown.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}
