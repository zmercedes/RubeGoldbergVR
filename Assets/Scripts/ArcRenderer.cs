/* Zoilo Mercedes
 * Arc renderer
 * Activates on left touchpad press, renders an arc
 */
using UnityEngine;

public class ArcRenderer : MonoBehaviour {

	// mesh components
	private Mesh mesh;

	// controller reference
	private Transform controller;
	private float lastRotation;

	// teleport target object
	public GameObject targetObject;

	// arc information
	public float meshWidth;
	public int resolution;
	public float time;
	public float speed = 10f;
	public float g = -18f;
	public Material[] materialIndicators;
	private float timeToTarget;
	private Renderer arcRend;
	private Vector3 velocity;
	private bool valid;

	void Awake(){
		// set controller reference
		controller = transform.parent.GetChild(0);

		// set material reference
		arcRend = GetComponent<Renderer>();

		// set mesh components
		mesh = GetComponent<MeshFilter>().mesh;

		// set initial y rotation of controller
		lastRotation = controller.eulerAngles.y;

		// sets appropriate rotation for arc
		transform.Rotate(new Vector3(0f, lastRotation + 90f,0f));
	}

	void Update(){
		// set point of origin to controller
		transform.position = controller.position;

		// applying y rotation
		if(controller.eulerAngles.y != lastRotation){
			transform.Rotate(new Vector3(0f, controller.eulerAngles.y - lastRotation,0f));
			lastRotation = controller.eulerAngles.y;
		}

		// disable target object and arc when tilting above 60 degrees and below -90 degrees (max and min teleport distances)
		if(controller.eulerAngles.x < 300 && controller.eulerAngles.x > 90){
			valid = false;
			timeToTarget = 0.01f;
			targetObject.SetActive(false);
		} else {
			valid = true;
			timeToTarget = time;
		}

		// set velocity to shoot forward from controller
		velocity = controller.forward * speed;

		RenderArc(ArcArray());
		SetValid();
	}

	void RenderArc(Vector3[] arcVerts){
		mesh.Clear();
		Vector3[] vertices = new Vector3[(resolution + 1) * 2];
		int[] triangles = new int[resolution * 6 * 2];
		
		for(int i = 0; i <= resolution; i++){
			// set vertices
			vertices[i*2] = new Vector3(arcVerts[i].x, arcVerts[i].y, meshWidth * 0.5f);
			vertices[i*2 +1] = new Vector3(arcVerts[i].x, arcVerts[i].y, -meshWidth * 0.5f);
		
			// set triangles
			if(i != resolution){
				triangles[i*12] = i*2;
				triangles[i*12 +1] = triangles[i*12 +4] = i*2 +1;
				triangles[i*12 +2] = triangles[i*12 +3] = (i+1) *2;
				triangles[i*12 +5] = (i+1) *2 +1;

				triangles[i*12 +6] = i*2;
				triangles[i*12 +7] = triangles[i*12 +10] = (i+1) *2;
				triangles[i*12 +8] = triangles[i*12 +9] = i*2 +1;
				triangles[i*12 +11] = (i+1) *2 +1;
			}
			
			mesh.vertices = vertices;
			mesh.triangles = triangles;
		}

		mesh.RecalculateBounds(); // must call this for mesh to be seen even when out of camera bounds
	}

	// shoots rays in a parabolic arc. upon hitting a target, gets time to target and draws the appropriate arc.
	Vector3[] ArcArray() {
		Vector3[] arcArray = new Vector3[resolution +1];
		Vector3 previousDrawPoint = controller.position;

		for (int i = 1; i <= resolution; i++) { // parabolic raycaster (neat!)
			float simulationTime = i / (float)resolution * time;
			Vector3 displacement = velocity * simulationTime + Vector3.up * g * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = controller.position + displacement;

			// raycast info
			Vector3 difference = previousDrawPoint - drawPoint;
			float length = difference.magnitude;
			RaycastHit hit;
			int mask = 1 << 8;
			
			if(Physics.Raycast(previousDrawPoint, difference.normalized, out hit, length, mask)){
				targetObject.SetActive(true);
				timeToTarget = simulationTime;
				if(hit.collider.tag == "nogo"){
					targetObject.SetActive(false);
					valid = false;
				} else if(hit.collider.tag == "platform")
					targetObject.transform.position = hit.transform.position;
				else
					targetObject.transform.position = hit.point;

				break;
			}

			previousDrawPoint = drawPoint;
		}

		previousDrawPoint = controller.position;
		for(int i = 1; i <= resolution; i++){
			float simulationTime = i / (float)resolution * timeToTarget;
			Vector3 displacement = velocity * simulationTime + Vector3.up * g * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = controller.position + displacement;

			arcArray[i] = transform.InverseTransformPoint(previousDrawPoint); // issue with calculation: local -> world points is annoying
			previousDrawPoint = drawPoint;
		}
		return arcArray;
	}

	void SetValid(){
		int i = valid ? 0 : 1;
		arcRend.sharedMaterial = materialIndicators[i];
	}
}