
var life:int = 3;
private var vitesse:float = 0.05;
var cameraPlayer:GameObject;

function Start () 
{
	if(!GetComponent.<NetworkView>().isMine)
	{
		Destroy(cameraPlayer);
		this.enabled = false;
	}
}

function Update () 
{
	Deplacement();
}

function Deplacement()
{
	if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
		transform.Translate(Vector3.forward * vitesse);
	else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		transform.Translate(Vector3.back * vitesse);
	if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
		transform.Rotate(Vector3.left * 2);
	else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		transform.Rotate(Vector3.right * 2);
		
	if(Input.GetKeyDown(KeyCode.Space))
		GetComponent.<Rigidbody>().AddForce(Vector3.up * 350);
}