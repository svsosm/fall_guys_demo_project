using UnityEngine;

namespace Painting.Wall
{

    public class Wall : MonoBehaviour
    {
        //Components
        private Camera camera;
        private Mesh mesh;

        //variables
        private const float rayLength = 10f;

        //List and Arrays
        private int[] triangles;
        private Vector3[] vertices;
        private Color[] colorArray;

        //tags and layers
        private const string paintWallTag = "PaintWall";

        private void Awake()
        {
            camera = Camera.main;
        }

        private void Start()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            vertices = mesh.vertices;
            triangles = mesh.triangles;


            // create new colors array where the colors will be created
            colorArray = new Color[vertices.Length];

            for (int k = 0; k < vertices.Length; k++)
            {
                colorArray[k] = Color.white;
            }
            mesh.colors = colorArray;
        }



        private void Update()
        {
            if(InputManager.Instance.ScreenDrag())
            {
                //create ray, hit wall and change color red
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, rayLength))
                {
                    if (hit.transform.CompareTag(paintWallTag))
                    {
                        if (hit.triangleIndex >= 0)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (colorArray[triangles[hit.triangleIndex * 3 + i]] != Color.red)  //multiplatication 3 for triangles
                                {
                                    colorArray[triangles[hit.triangleIndex * 3 + i]] = Color.red;
                                    GameManager.Instance.IncrementPaintedVerticeCount();
                                }
                            }
                            mesh.colors = colorArray;
                        }
                    }
                }
            }
        }
    }

}