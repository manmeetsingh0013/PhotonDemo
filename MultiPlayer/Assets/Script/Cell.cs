using UnityEngine;
using UnityEngine.UI;

namespace Match
{
    /// <summary>
    /// This script is handle the cell selection and the opration on it.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class Cell : MonoBehaviour
    {

        #region SERIALIZE FIELDS

        [SerializeField] Image selectionImage;

        #endregion

        #region PUBLIC FIELDS

        /// <summary>
        /// Delegate get call when user clicks on any cell of the grid.
        /// </summary>
        /// <param name="team"></param>
        public delegate void OnTurnCompleted(TEAM team);                

        public static event OnTurnCompleted onTurnCompleted;

        #endregion

        #region PRIAVTE FIELDS

        private bool isSelected;

        private Button button;

        GameManager gameManager;

        #endregion

        #region UNITY METHODS

        void Awake()
        {
            button = GetComponent<Button>();

            button.interactable = !isSelected;

            gameManager = GameManager.GetInstance();

        }

        #endregion

        #region PRIAVATE FIELDS

        /// <summary>
        /// Apply the team color to the grid cell.
        /// </summary>
        /// <param name="currentTeam"></param>
        private void ApplySelectionColor(TEAM currentTeam)
        {
            switch(currentTeam)
            {
                case TEAM.RED:

                    selectionImage.color = Color.red;

                    break;
                case TEAM.GREEN:

                    selectionImage.color = Color.green;

                    break;
                case TEAM.BLUE:

                    selectionImage.color = Color.blue;

                    break;
                case TEAM.YELLOW:

                    selectionImage.color = Color.yellow;

                    break;
            }

            MoveToNext(currentTeam);
        }

        /// <summary>
        /// Move to next player by chnaging the current team.
        /// </summary>
        /// <param name="team"></param>
        private void MoveToNext(TEAM team)
        {
            if (onTurnCompleted != null)
            {
                int nextTeam = (int)team + 1;

                nextTeam = (nextTeam == GameManager.numberOfPlayes) ? 0 : nextTeam;

                onTurnCompleted((TEAM)nextTeam);
            }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// This is action when user clicks on the any grid.
        /// </summary>
        public void OnClickCell()
        {
            isSelected = true;

            gameManager.PlaySound(SOUNDTYPE.TAP);

            button.interactable = !isSelected;

            selectionImage.gameObject.SetActive(true);

            ApplySelectionColor(gameManager.GetCurrentTurn());

        }

        #endregion
    }
}