using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using OpenCTM;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		readTest ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void readTest ()
	{
		FileStream file = new FileStream ("Assets/Resources/brunnen.ctm", FileMode.Open);
		CtmFileReader reader = new CtmFileReader (file);
		
		OpenCTM.Mesh m = reader.decode ();
		UnityEngine.Mesh um = new UnityEngine.Mesh ();
	
		m.checkIntegrity ();
	
		List<Vector3> Vertices = new List<Vector3> ();

		for (int i=0; i < m.getVertexCount (); i++)
			Vertices.Add (new Vector3(m.vertices[(i * 3)], m.vertices[(i * 3) + 1], m.vertices[(i * 3) + 2]));

		List<Vector2> UV = new List<Vector2> ();

		for (int i=0; i < m.texcoordinates[0].values.Length / 2; i++)
			UV.Add (new Vector2(m.texcoordinates[0].values[(i*2)], m.texcoordinates[0].values[(i*2)+1]));

		um.vertices = Vertices.ToArray ();
		um.triangles = m.indices.Clone () as int[];
		um.uv = UV.ToArray ();

		um.RecalculateBounds ();
		um.RecalculateNormals ();

		GameObject go = new GameObject ();
		MeshFilter mf = go.AddComponent<MeshFilter> ();
		MeshRenderer mr = go.AddComponent<MeshRenderer> ();
		mr.material = new Material(Shader.Find("Diffuse"));
		mf.mesh = um;

	

	}
}


