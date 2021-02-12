using RollingBall.Common;
using UnityEngine;

namespace RollingBall.Title
{
    public sealed class RankLoader : MonoBehaviour
    {
        [SerializeField] private RankButton[] rankButtons = default;

        private void Start()
        {
            var clearData = GetClearRankData();
            for (int i = 0; i < rankButtons.Length; i++)
            {
                if (i > 0)
                {
                    if (clearData[i - 1] > 0 && clearData[i] == -1)
                    {
                        clearData[i] = 0;
                    }
                }

                rankButtons[i].ShowRank(clearData[i]);
            }
        }

        public static int[] GetClearRankData()
        {
            return ES3.Load(Const.CLEAR_RANK_KEY, GetDefaultClearData());
        }

        public void ResetClearRank()
        {
            var clearData = GetDefaultClearData();
            Save(clearData);

            for (int i = 0; i < rankButtons.Length; i++)
            {
                rankButtons[i].ShowRank(clearData[i]);
            }
        }

        private static int[] GetDefaultClearData()
        {
            var clearData = new int[Const.MAX_STAGE_COUNT];
            clearData[0] = 0;
            for (int i = 1; i < clearData.Length; i++)
            {
                clearData[i] = -1;
            }

            return clearData;
        }

        public static int SaveClearData(int level, float clearRate)
        {
            var clearData = ES3.Load(Const.CLEAR_RANK_KEY, GetDefaultClearData());
            var clearRank = GetClearRank(clearRate);
            if (clearRank > clearData[level])
            {
                clearData[level] = clearRank;
                Save(clearData);
            }

            return clearRank;
        }

        public static void Save(int[] clearData)
        {
            ES3.Save(Const.CLEAR_RANK_KEY, clearData);
        }

        private static int GetClearRank(float clearRate)
        {
            if (clearRate <= 1.0f)
            {
                return 3;
            }

            if (clearRate <= 1.5f)
            {
                return 2;
            }

            return 1;
        }
    }
}