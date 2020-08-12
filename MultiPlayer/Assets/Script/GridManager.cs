using UnityEngine;

/// <summary>
/// This script handle the grid's cell organization.
/// </summary>
namespace Match
{
    [RequireComponent(typeof(GridLayout))]
    public class GridManager : MonoBehaviour
    {
        #region SERIALIZE FIELDS

        [SerializeField] GameObject go_Cell;    //@contains the prefab refernce of the cell.

        [SerializeField] int numberOfCells;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            GenerateCells();    
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Create the N nunber of cells in game.
        /// </summary>
        private void GenerateCells()
        {
            for (int i = 0; i < numberOfCells; i++)
            {
                Instantiate(go_Cell, transform);
            }
        }

        #endregion
    }
}