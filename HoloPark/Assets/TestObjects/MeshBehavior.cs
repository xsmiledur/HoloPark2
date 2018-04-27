using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBehavior : MonoBehaviour {

    MeshFilter mf;
    Mesh mesh;
    Vector3[] v, initV;
	// Use this for initialization
	void Start () {
        // メッシュフィルターを取得
        mf = GetComponent<MeshFilter>();
        // メッシュフィルターから対象のメッシュ情報を取得
        mesh = mf.sharedMesh;
        v = mesh.vertices;
        initV = v;
		
        // mesh.vertices に対象の頂点の３次元座標が配列で格納

        // mesh.triangles に対象のメッシュ上で，三角形になるべきものの
        // verticesにおけるIndex番号の順で配列で載せられている
        // trianglesの配列の3の倍数の長さでないと怒られる

        // (0,0)(0,1)(1,1)(1,0)という４点がvertiesに格納されているなら
        // trianglesには[0,1,2,1,2,3]と記載されていれば，正方形のメッシュになる
        // 反時計回りにしないとメッシュが裏側についてしまうことがあるので注意

	}

    // Update is called once per frame
    int t = 0;
	void Update () {
		for (int i=0; i<1; i++)
		//for (int i=0; i<v.Length; i++)
        {
            float val = Mathf.Abs(Mathf.Cos((float)t / 100f + Mathf.PI * i / 3)) / 1000f;
            v[i] = initV[i] + new Vector3(val, val, val);
        }
        mesh.vertices = v;
        mf.sharedMesh = mesh;
        ++t;
	}
}
