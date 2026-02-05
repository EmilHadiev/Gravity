using UnityEngine;

public class CylinderCapRemover : MonoBehaviour
{
    [ContextMenu(nameof(Start))]
    private void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = Instantiate(mf.sharedMesh); // Копируем меш, чтобы не испортить оригинал
        mf.mesh = mesh;

        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        var newTriangles = new System.Collections.Generic.List<int>();

        for (int i = 0; i < triangles.Length; i += 3)
        {
            // Проверяем нормаль треугольника (куда он смотрит)
            Vector3 v1 = vertices[triangles[i]];
            Vector3 v2 = vertices[triangles[i + 1]];
            Vector3 v3 = vertices[triangles[i + 2]];
            Vector3 center = (v1 + v2 + v3) / 3f;

            // Если центр треугольника находится на самом верху (0.5) или внизу (-0.5) — пропускаем его
            if (Mathf.Abs(center.y) < 0.49f)
            {
                newTriangles.Add(triangles[i]);
                newTriangles.Add(triangles[i + 1]);
                newTriangles.Add(triangles[i + 2]);
            }
        }
        mesh.triangles = newTriangles.ToArray();
        mesh.RecalculateNormals();
    }
}