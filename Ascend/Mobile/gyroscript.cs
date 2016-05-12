using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gyroscript : MonoBehaviour {

    private Vector3 attitude;
    private Vector3 rotRate;

    //gravity y is for tilt activation of Perspective Shift, gravity x is for height increase or decrease for the same purpose
    public Camera MobileCamera;
	[SerializeField] private Vector3 gravity;
	[SerializeField] private bool heldUp;
    [SerializeField] public static bool isShifted;
	private bool buttonsEnabled = true;
	public bool isConnected;
	public GameObject NetworkManager;
	[SerializeField] public GameObject PerspectiveShiftBackground;
	[SerializeField] public GameObject PauseAndQuitPanel;
	bool ForcedOutOfPerspectiveShift = false;


    void Start()
    {
		heldUp = false;
        isShifted = false;
		isConnected = false;
    }

    void FixedUpdate()
    {
        Input.gyro.enabled = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        attitude = Input.gyro.attitude.eulerAngles;
        rotRate = Input.gyro.rotationRateUnbiased;
        gravity = Input.gyro.gravity;

		//Player shifted/is holding up the mobile device
		if (gravity.y <= -.8f && !ForcedOutOfPerspectiveShift)
        {
			if (heldUp == false){
				Debug.Log ("in heldup false");
                NetworkManager.GetComponent<MobileSenderFunctions>().StartGyroMessage();
				heldUp = true;
				Debug.Log ("Gravity shifted up");
				PerspectiveShiftBackground.SetActive(false);
				PauseAndQuitPanel.SetActive(false);
				MobileCamera.enabled = true;
			}

            isShifted = true;
        }

		//Player lowered the device
        if (gravity.y >= -.65f)
        {
			Debug.Log("Gravity shifted down");
			ForcedOutOfPerspectiveShift = false;
			if (heldUp == true){
				Debug.Log("in help up true");
				NetworkManager.GetComponent<MobileSenderFunctions>().StopGyroMessage();
				heldUp = false;
				PerspectiveShiftBackground.SetActive(true);
				PauseAndQuitPanel.SetActive(true);
				MobileCamera.enabled = false;

			}
            isShifted = false;
        }

        if (isShifted == true) {

			float initX = Input.gyro.rotationRateUnbiased.x;
			float initY = Input.gyro.rotationRateUnbiased.y;
			this.transform.Rotate (new Vector3 (-initX, -initY, 0));
			this.transform.rotation = Quaternion.Euler (this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, 0);

			if (buttonsEnabled) {
				foreach (GameObject _button in GameObject.Find("NetworkManager").GetComponent<MobileParserFunctions>().OptionButtonsList) {
					_button.SetActive (false);
					Debug.Log ("passing through raising device");
					buttonsEnabled = false;

				}
			}
		} else {
			if (!buttonsEnabled) {
				foreach (GameObject _button in GameObject.Find("NetworkManager").GetComponent<MobileParserFunctions>().OptionButtonsList) {
					_button.SetActive(true);
					Debug.Log ("passing through lowering device");
					buttonsEnabled = true;

				}

			}
		}

    }
	public void ForceOutOfPerspectiveShift(){
		if (heldUp == true){
			Debug.Log("in help up true");
			NetworkManager.GetComponent<MobileSenderFunctions>().StopGyroMessage();
			heldUp = false;
			PerspectiveShiftBackground.SetActive(true);
			PauseAndQuitPanel.SetActive(true);
			MobileCamera.enabled = false;
			ForcedOutOfPerspectiveShift=true;
			
		}
		isShifted = false;
	}

    public void SendEndingRotation()
    {
        string endRot = this.transform.rotation.x + "+" + this.transform.rotation.y + "+" + this.transform.rotation.z;
		Debug.Log (endRot);
    }

}
