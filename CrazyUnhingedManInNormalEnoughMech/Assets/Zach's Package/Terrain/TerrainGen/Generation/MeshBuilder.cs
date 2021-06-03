using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zacks.Terrain
{
    public class MeshBuilder : MonoBehaviour
    {
        public bool done = false;
        List<Vector3> positions = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<Color> colors = new List<Color>();

        List<int> indices = new List<int>();

        public void Clear()
        {
            // clear all lists
            positions.Clear();
            normals.Clear();
            uvs.Clear();
            colors.Clear();
            indices.Clear();
        }

        public void AddQuad(Vector3 normal, Vector3 position, Vector3 xAxis, Vector3 yAxis)
        {
            AddQuad(normal, position, xAxis, yAxis, Color.white);
        }

        public void AddQuad(Vector3 normal, Vector3 position, Vector3 xAxis, Vector3 yAxis, Color color)
        {
            int index = positions.Count;

            positions.Add(position);
            positions.Add(position + xAxis);
            positions.Add(position + yAxis);
            positions.Add(position + xAxis + yAxis);

            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);

            colors.Add(color);
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);

            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(1, 1));

            indices.Add(index);
            indices.Add(index + 2);
            indices.Add(index + 1);

            indices.Add(index + 1);
            indices.Add(index + 2);
            indices.Add(index + 3);

        }

        public void AddQuad(Vector3 normal, Vector3 position, Vector3 xAxis, Vector3 yAxis, Color color1, Color color2, Color color3, Color color4)
        {
            int index = positions.Count;

            positions.Add(position);
            positions.Add(position + xAxis);
            positions.Add(position + yAxis);
            positions.Add(position + xAxis + yAxis);

            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);

            colors.Add(color1);
            colors.Add(color2);
            colors.Add(color3);
            colors.Add(color4);

            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(1, 1));

            indices.Add(index);
            indices.Add(index + 2);
            indices.Add(index + 1);

            indices.Add(index + 1);
            indices.Add(index + 2);
            indices.Add(index + 3);

        }

        public void AddQuad(Vector3 normal, Vector3 position1, Vector3 position2, Vector3 position3, Vector3 position4, Color color)
        {
            int index = positions.Count;

            positions.Add(position1);
            positions.Add(position2);
            positions.Add(position3);
            positions.Add(position4);

            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);

            colors.Add(color);
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);

            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(1, 1));

            indices.Add(index);
            indices.Add(index + 2);
            indices.Add(index + 1);

            indices.Add(index + 1);
            indices.Add(index + 2);
            indices.Add(index + 3);
        }

        public void CreateMesh(bool allocateMemory, GameObject loadingPanel = null, TextMeshProUGUI specifcLoadingText = null, 
            TextMeshProUGUI loadingPercent = null, Slider loadingBar = null)
        {
            if (!loadingPanel)
            {
                createMesh(allocateMemory);
                done = true;
            }
            else
            {
                StartCoroutine(createMesh(allocateMemory, loadingPanel, specifcLoadingText, loadingPercent, loadingBar));
            }
        }

        public void createMesh(bool allocateMemory)
        {
            MeshFilter mf = GetComponent<MeshFilter>();
            Mesh mesh = new Mesh();

            if (allocateMemory)
            {
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            }

            mesh.vertices = positions.ToArray();
            mesh.normals = normals.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.colors = colors.ToArray();
            mesh.SetTriangles(indices.ToArray(), 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mf.mesh = mesh;
            MeshCollider mCol = GetComponent<MeshCollider>();

            if (mCol)
            {
                mCol.sharedMesh = mf.mesh;
            }
        }

        IEnumerator createMesh(bool allocateMemory, GameObject loadingPanel = null, TextMeshProUGUI specifcLoadingText = null, 
            TextMeshProUGUI loadingPercent = null, Slider loadingBar = null)
        {
            specifcLoadingText.text = "Generating mesh";
            loadingBar.value = 0;
            loadingPercent.text = loadingBar.value * 100f + "%";
            yield return new WaitForEndOfFrame();
            MeshFilter mf = GetComponent<MeshFilter>();
            Mesh mesh = new Mesh();

            if (allocateMemory)
            {
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            }

            mesh.vertices = positions.ToArray();
            loadingBar.value = 0.125f;
            loadingPercent.text = loadingBar.value * 100f + "%";
            yield return new WaitForEndOfFrame();
            mesh.normals = normals.ToArray();
            loadingBar.value = 0.25f;
            loadingPercent.text = loadingBar.value * 100f + "%";
            yield return new WaitForEndOfFrame();
            mesh.uv = uvs.ToArray();
            loadingBar.value = 0.375f;
            loadingPercent.text = loadingBar.value * 100f + "%";
            yield return new WaitForEndOfFrame();
            mesh.colors = colors.ToArray();
            loadingBar.value = 0.5f;
            loadingPercent.text = loadingBar.value * 100f + "%";
            yield return new WaitForEndOfFrame();
            mesh.SetTriangles(indices.ToArray(), 0);
            loadingBar.value = 0.625f;
            loadingPercent.text = loadingBar.value * 100f + "%";
            yield return new WaitForEndOfFrame();
            mesh.RecalculateBounds();
            loadingBar.value = 0.75f;
            loadingPercent.text = loadingBar.value * 100f + "%";
            yield return new WaitForEndOfFrame();
            mesh.RecalculateNormals();
            loadingBar.value = 0.875f;
            loadingPercent.text = loadingBar.value * 100f + "%";
            yield return new WaitForEndOfFrame();
            mf.mesh = mesh;
            MeshCollider mCol = GetComponent<MeshCollider>();

            if (mCol)
            {
                mCol.sharedMesh = mf.mesh;
            }

            loadingBar.value = 1;
            loadingPercent.text = loadingBar.value * 100f + "%";
            yield return new WaitForEndOfFrame();
            loadingPanel.SetActive(false);
            done = true;
        }
    }
}
