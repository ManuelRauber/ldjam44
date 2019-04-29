using UnityEngine;

namespace LdJam44.Driver
{
    public class DoorOpener : MonoBehaviour
    {
        public GameObject LeftDoor;
        public GameObject RightDoor;

        public Vector3 LeftDoorRotation;
        public Vector3 RightDoorRotation;

        public void OpenDoors()
        {
            Debug.Log("Opening Doors");
            LeftDoor.transform.rotation = Quaternion.Euler(LeftDoorRotation);
            RightDoor.transform.rotation = Quaternion.Euler(RightDoorRotation);
        }
    }
}
