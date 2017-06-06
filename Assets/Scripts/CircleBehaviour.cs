
using UnityEngine;
using Random = System.Random;

/// <summary>
/// The circle behaviour class.
/// </summary>
public class CircleBehaviour : MonoBehaviour
{
    private float timer;

    private int randNum;

    public float radius = 25f;

    /// <summary>
    /// The start function.
    /// </summary>
    private void Start()
    {
        // Get a random number for using to randomize light direction
        Random r = new Random();
        randNum = r.Next(0, 4);

        this.timer = 0;
    }

    /// <summary>
    /// The update function.
    /// </summary>
    private void Update()
    {
        timer += Time.deltaTime * 2f;

        float rotationAxis = Mathf.Cos(this.timer) * this.radius;
        float y = Mathf.Sin(this.timer) * this.radius;

        // Check the random number and determine the direction of the light to travel
        switch (this.randNum)
        {
            case 0: // Move on Z
                this.transform.parent.position = new Vector3(this.transform.parent.position.x, 0, this.transform.parent.position.z);
                this.transform.localPosition = new Vector3(rotationAxis, y, this.transform.position.z);
                if (rotationAxis < 0)
                    Destroy(this.gameObject);
                break;
            case 1: // Move on Z
                this.transform.parent.position = new Vector3(this.transform.parent.position.x, 0, this.transform.parent.position.z);
                this.transform.localPosition = new Vector3(-rotationAxis, y, this.transform.position.z);
                if (rotationAxis < 0)
                    Destroy(this.gameObject);
                break;
            case 2: // Move on X
                this.transform.parent.position = new Vector3(this.transform.parent.position.x, 0.7f, this.transform.parent.position.z);
                this.transform.position = new Vector3(this.transform.position.x, y, rotationAxis);
                if (rotationAxis < 0)
                    Destroy(this.gameObject);
                break;
            case 3: // Move on X
                this.transform.parent.position = new Vector3(this.transform.parent.position.x, 0.7f, this.transform.parent.position.z);
                this.transform.position = new Vector3(this.transform.position.x, y, -rotationAxis);
                if (rotationAxis < 0)
                    Destroy(this.gameObject);
                break;
        }
    }
}