using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GraphMoveThingy : MonoBehaviour {
    //Declarations
    public GameObject moveRight;
    public GameObject moveLeft;
    public GameObject moveDown;
    public GameObject moveUp;
    public Canvas bGHolder;
    public Image bGImage;
    public Sprite bGimage2;
    SpriteRenderer self;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ((!bGimage2.Equals(null))&&(!bGImage.Equals(null)))
        {
            bGImage.sprite = bGimage2;
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
