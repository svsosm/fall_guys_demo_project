using UnityEngine;

namespace Opponents.Character
{
    public class Characters: MonoBehaviour
    {
        private string characterName;
        private float positionZ;
        private float finishPosZ = 13.35f;
        private bool isFinish = false;

        private void Awake()
        {
            characterName = gameObject.name;
            positionZ = transform.position.z;
        }

        private void Update()
        {
            if(!isFinish)
            {
                //finish game
                if (transform.position.z >= finishPosZ)
                {
                    RankingManager.Instance.IncrementFinishedPlayerCount();
                    RankingManager.Instance.keyValues.Remove(characterName);
                    isFinish = true;
                }
                else
                {
                    RankingManager.Instance.keyValues[characterName] = transform.position.z;
                }
            }

        }

        public string GetCharacterName()
        {
            return this.characterName;
        }

        public float GetPositionZ()
        {
            return this.positionZ;
        }


    }
}