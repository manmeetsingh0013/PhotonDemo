using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script handle the player settings and player turns.
/// </summary>

namespace Match
{
    [RequireComponent(typeof(Image))]
    public class Profile : MonoBehaviour
    {
        #region SERIALIZE FIELDS

        [SerializeField] Image timerImage;

        [SerializeField] Text playerName;

        [SerializeField]  TEAM currentTeam;

        #endregion

        #region PUBLIC FIELDS

        public delegate void OnTurnCompleted(TEAM team);

        public static event OnTurnCompleted onTurnCompleted;

        #endregion

        #region PRIVATE FIELDS

        float time = 0;

        bool isTurnCompleted;

        bool isMyTurn = false;

        float thresoldTimer = 5;

        bool isThresholdTimer = false;

        Image profilePic;

        GameManager gameManager;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            profilePic = GetComponent<Image>();
        }

        private void Start()
        {
            gameManager = GameManager.GetInstance();

            if(PlayerPrefs.HasKey(PlayerName.playerPrefNameKey))
            {
                playerName.text = PlayerPrefs.GetString(PlayerName.playerPrefNameKey);
            }
            else
            {
                Debug.Log("Name not found");
            }


        }

        private void OnEnable()
        {
            GameManager.onNextTurn += PlayTurn;
        }

        private void OnDisable()
        {
            GameManager.onNextTurn -= PlayTurn;
        }

        private void Update()
        {
            if (isMyTurn)
            {
                CalculateTime();
            }
        }
        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Method handle the states of turns.
        /// </summary>
        /// <param name="team"></param>
        private void PlayTurn(TEAM team)
        {

            ExitTurn();

           if(team.Equals(currentTeam))
            {
                EnterTurn();
            }
        }


        /// <summary>
        /// Simply Exits from the turn.
        /// </summary>
        private void ExitTurn()
        {
            isMyTurn = false;

            profilePic.color = Color.gray;

            timerImage.fillAmount = 1;

            isTurnCompleted = false;

        }

        /// <summary>
        /// Method handle when having own turn.
        /// </summary>
        private void EnterTurn()
        {
            isMyTurn = true;

            isThresholdTimer = false;

            profilePic.color = Color.white;

            time = 0;
        }

        /// <summary>
        /// Calculate the time to play to the player turn.
        /// </summary>
        private void CalculateTime()
        {
            if (!isTurnCompleted)
            {
                if (time < GameManager.turnTime)
                {
                    time += Time.deltaTime;

                    timerImage.fillAmount = (float)(GameManager.turnTime - time) / GameManager.turnTime;

                    if ((float)(GameManager.turnTime - time) < thresoldTimer && !isThresholdTimer)
                    {
                        isThresholdTimer = true;

                        gameManager.PlaySound(SOUNDTYPE.TIMER);
                    }

                }
                else
                {
                    // When time completed.
                    isTurnCompleted = true;

                    if(onTurnCompleted !=null)
                    {
                        int nextTeam = (int)currentTeam + 1;

                        nextTeam = (nextTeam == GameManager.numberOfPlayes) ? 0 : nextTeam;

                        onTurnCompleted((TEAM)nextTeam);
                    }
                }
            }

        }

        #endregion

        #region PUBLIC METHODS

  

        #endregion
    }
}