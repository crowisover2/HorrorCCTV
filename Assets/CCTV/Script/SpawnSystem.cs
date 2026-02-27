using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.CCTV.Script
{
	public class SpawnSystem : MonoBehaviour
	{
        [Header("Rooms")]
        [SerializeField] private List<StageRoom> cctvs;

        private GameSetting data;

        private int choosenCamera;
        private int choosenStrange;

        private int currentIndex = 0;

        public UnityAction OnActive;
        public UnityAction OnDeactive;
        public int NumCam => cctvs.Count;
        public int[] AllStrangeCountEachRoom => cctvs.Select(room => room.StrangeCount).ToArray();

        private Dictionary<int, bool> roomStatus;

        private void Start()
        {
            roomStatus = new();

            for (int i = 0; i < cctvs.Count; i++)
            {
                roomStatus.Add(i, false);
            }
        }

        private void OnEnable()
        {
            OnActive?.Invoke();
        }

        private void OnDisable()
        {
            OnDeactive?.Invoke();
        }

        public void StartSpawnSystem(GameSetting _data) {
            currentIndex = 0;
            data = _data;
            ChangeCCTV();
        }

        public async Task DoSpawnWorkAsync()
        {
            Debug.Log("Spawn System checking for available tasks...");

            while (true) // Keep looking until we find a room with a task or run out of rooms
            {
                // 1. Filter for rooms that aren't marked "Done" in our Dictionary
                List<int> availableRooms = new List<int>();
                for (int i = 0; i < cctvs.Count; i++)
                {
                    roomStatus.TryGetValue(i, out bool isFullyExhausted);
                    if (!isFullyExhausted) availableRooms.Add(i);
                }

                // 2. If no rooms have any tasks left, exit the whole process
                if (availableRooms.Count == 0)
                {
                    Debug.Log("Total Blackout: No tasks available in any CCTV room.");
                    return;
                }

                // 3. Pick a random room from the available list
                int choosenIndex = availableRooms[UnityEngine.Random.Range(0, availableRooms.Count)];

                // 4. Try to trigger a task in that room
                // Returns TRUE if a task was started
                // Returns FALSE if the "PC" has no tasks left
                bool taskWasAvailable = cctvs[choosenIndex].SpawnStrange();

                if (taskWasAvailable)
                {
                    Debug.Log($"Task successfully started in Room {choosenIndex}");
                    break; // Exit the 'while' loop because we successfully spawned something!
                }
                else
                {
                    // The room is empty! Mark it as 'Done' so we don't pick it again
                    Debug.Log($"Room {choosenIndex} is exhausted. Marking as Done.");
                    roomStatus[choosenIndex] = true;

                    // The 'while' loop will now run again, 
                    // but 'availableRooms' will be shorter this time.
                }

                // Safety yield for Unity 6 to prevent frame spikes if many rooms are empty
                await Task.Yield();
            }
        }

        public void Increase(int _modifier)
        {
            int change = currentIndex + _modifier;
            if (change < 0)
            {
                change = cctvs.Count - 1;
            }
            else if (change >= cctvs.Count)
            {
                change = 0;
            }
            currentIndex = change;

            ChangeCCTV();
        }

        private void ChangeCCTV()
        {
            for (int i = 0; i < cctvs.Count; i++)
            {
                if (currentIndex.Equals(i))
                {
                    cctvs[i].Priority = 1;
                }
                else cctvs[i].Priority = 0;
            }
        }
    }
}