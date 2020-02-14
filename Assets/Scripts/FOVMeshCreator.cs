using UnityEngine;

public class FOVMeshCreator : MonoBehaviour
{
    [SerializeField] float fieldOfViewLength = 1.5f;
    [SerializeField] float angle = 55f;
    [SerializeField] int segments = 10;
    [SerializeField] Material meshMaterial;

    private Mesh _fieldOfViewMesh;
    private Vector3[] _vertices;
    private Vector3[] _normals;
    private Vector2[] _uvs;
    private int[] _triangles;
    private float _individualSegmentAngle;
    
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>().material = meshMaterial;
        _fieldOfViewMesh = gameObject.GetComponent<MeshFilter>().mesh;

        BuildMesh();
    }

    void BuildMesh()
    {
        _fieldOfViewMesh.Clear();

        // Initialising the arrays
        _vertices = new Vector3[segments * 3];
        _uvs = new Vector2[segments * 3];
        _triangles = new int[segments * 3];
        _normals = new Vector3[segments * 3];

        // Initialising the vertices at the origin
        for (int i = 0; i < _vertices.Length; i++)
        {
            _vertices[i] = new Vector3(0, 0, 0);
            _normals[i] = Vector3.up; // As we face the screen top-down, we do not want the lightning changing the color of the mesh
        }

        _individualSegmentAngle = angle * 2 / segments;

        float angleBetweenVertices = 90.0f - angle;

        // Creating the vertices
        for (int i = 1; i < _vertices.Length; i += 3)
        {
            
            // It starts from 1 and add + 3 to the index because the starting vertex is always the origin
            
            _vertices[i] = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * angleBetweenVertices) * fieldOfViewLength, // x coordinate -> cos
                0, // y coordinate at origin
                Mathf.Sin(Mathf.Deg2Rad * angleBetweenVertices) * fieldOfViewLength); // z coordinate -> sin

            angleBetweenVertices += _individualSegmentAngle;

            _vertices[i + 1] = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angleBetweenVertices) * fieldOfViewLength, // x coordinate -> cos
                0, // y coordinate at origin 
                Mathf.Sin(Mathf.Deg2Rad * angleBetweenVertices) * fieldOfViewLength); // z coordinate -> sin
        }
        
        // Generate the UVs
        for (int i = 0; i < _uvs.Length; i++)
        {
            _uvs[i] = new Vector2(_vertices[i].x, _vertices[i].z);
        }

        // Create the triangles
        for (int i = 0; i < _triangles.Length; i += 3)
        {
            _triangles[i] = 0;
            _triangles[i + 1] = i + 2;
            _triangles[i + 2] = i + 1;
        }
        
        // Add the components to our Mesh
        _fieldOfViewMesh.vertices = _vertices;
        _fieldOfViewMesh.uv = _uvs;
        _fieldOfViewMesh.triangles = _triangles;
        _fieldOfViewMesh.normals = _normals;
    }
}