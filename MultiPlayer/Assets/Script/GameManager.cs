using UnityEngine;

/// <summary>
/// This script handle the Game States and the player turns and we can extend to handle whole game play as well.
/// </summary>
namespace Match
{

    public enum TEAM
    {
        RED,
        GREEN,
        BLUE,
        YELLOW
    }

    public enum SOUNDTYPE
    {
        TAP,
        TIMER
    }

    public class GameManager : MonoBehaviour
    {
        #region PUBLIC FIELDS

        [SerializeField] AudioClip tapAudio;

        [SerializeField] AudioClip timerAudio;

        /// <summary>
        /// Delegate get call when the previous player completes the turn or time out.
        /// </summary>
        /// <param name="team"></param>
        public delegate void OnNextTurn(TEAM team);

        public static event OnNextTurn onNextTurn;

        public const float turnTime = 10;

        public const int numberOfPlayes = 2;

        #endregion

        #region PRIVATE FIELDS

        static GameManager instance;

        TEAM currentTeamTurn;

        AudioSource source;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            instance = this;

            source = GetComponent<AudioSource>();

            DontDestroyOnLoad(this);
        }

        void Start()
        {
            SetTurn(TEAM.RED);
        }

        private void OnEnable()
        {
            Profile.onTurnCompleted += SetTurn;

            Cell.onTurnCompleted += SetTurn;
        }
        private void OnDisable()
        {
            Profile.onTurnCompleted -= SetTurn;

            Cell.onTurnCompleted -= SetTurn;
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Set the current turn of the game.
        /// </summary>
        /// <param name="team"></param>
        void SetTurn(TEAM team)
        {
            currentTeamTurn = team;

            if (onNextTurn != null)
            {
                onNextTurn(currentTeamTurn);
            }

        }

        #endregion

        #region PUBLIC METHODS

        public static GameManager GetInstance()
        {
            if(instance ==null)
            {
                GameObject obj = new GameObject("GameManager");

                instance = obj.AddComponent<GameManager>();
            }

            return instance;
        }

        /// <summary>
        /// Method return the current team turn.
        /// </summary>
        /// <returns></returns>
        public TEAM GetCurrentTurn()
        {
            return currentTeamTurn;
        }

        /// <summary>
        /// Play differnt sound of the game.
        /// </summary>
        /// <param name="soundType"></param>
        public void PlaySound(SOUNDTYPE soundType)
        {
            switch(soundType)
            {
                case SOUNDTYPE.TAP:

                    source.clip = tapAudio;

                    source.Play();

                    break;
                case SOUNDTYPE.TIMER:

                    source.clip = timerAudio;

                    source.Play();

                    break;

            }
        }

        public void StopSound()
        {
            source.Stop();
        }
        #endregion
    }
}
