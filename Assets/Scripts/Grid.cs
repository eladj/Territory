using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    [SerializeField]
    public float square_size = 1f;
	public int num_squares_x = 10;
	public int num_squares_y = 10;
	private Mesh mesh;
	private Vector3[] vertices;
    
	private void Awake () {
		Generate();
	}

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / square_size);
        int yCount = Mathf.RoundToInt(position.y / square_size);
        int zCount = Mathf.RoundToInt(position.z / square_size);

        Vector3 result = new Vector3(
            (float)xCount * square_size,
            (float)yCount * square_size,
            (float)zCount * square_size);

        result += transform.position;

        return result;
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     for (float x = -num_squares_x/2; x < num_squares_x/2; x += size)
    //     {
    //         for (float y = -num_squares_y/2; y < num_squares_y/2; y += size)
    //         {
    //             var point = GetNearestPointOnGrid(new Vector3(x, y, 0f));
    //             Gizmos.DrawSphere(point, 0.1f);
    //         }                
    //     }
    // }

	private void OnDrawGizmos () {
		if (vertices == null) {
			return;
		}		
		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}

	private void Generate () {
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";

        int xSize = (int) square_size;
        int ySize = (int) square_size;
		vertices = new Vector3[(num_squares_x + 1) * (num_squares_y + 1)];
		Vector2[] uv = new Vector2[vertices.Length];
		Vector4[] tangents = new Vector4[vertices.Length];
		Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
		for (int i = 0, y = 0; y <= num_squares_y; y++) {
			for (int x = 0; x <= num_squares_x; x++, i++) {
				vertices[i] = new Vector3(x, y);
				uv[i] = new Vector2((float)x / num_squares_x, (float)y / num_squares_y);
				tangents[i] = tangent;
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.tangents = tangents;

		int[] triangles = new int[num_squares_x * num_squares_y * 6];
		for (int ti = 0, vi = 0, y = 0; y < num_squares_y; y++, vi++) {
			for (int x = 0; x < num_squares_x; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + num_squares_x + 1;
				triangles[ti + 5] = vi + num_squares_x + 2;
			}
		}
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
	}    
}
