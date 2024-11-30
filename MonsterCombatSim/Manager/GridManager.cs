namespace MonsterCombatSim.Manager;


// Each instance of GridManager in Main takes a
// Grid as parameter and creates a list of random
// GripPositions records that can be accessed through
// GetGridPositions()

public class GridManager
{
    //private Grid _grid;
    private List<Grid.GridPostion> _gridPositions;

    public GridManager()
    {
        //_grid = grid ?? throw new ArgumentNullException(nameof(grid));
        _gridPositions = new List<Grid.GridPostion>();
        
        GenerateListOfGridPositions();
        ShuffleGridPositions();
    }
    
    private void GenerateListOfGridPositions()
    {
        _gridPositions.Clear();
        for (int row = 0; row < Grid.MaxRow; row++)
        {
            for (int col = 0; col < Grid.MaxCol; col++)
            {
                _gridPositions.Add(new Grid.GridPostion(row,col));
            }
        }
    }

    private void ShuffleGridPositions()
    {
        for (int i = 0; i < _gridPositions.Count; i++)
        {
            int randomIndex = GetRandom(i, _gridPositions.Count);

            (_gridPositions[i], _gridPositions[randomIndex]) = (_gridPositions[randomIndex], _gridPositions[i]);
        }
    }

    public List<Grid.GridPostion> GetGridPositions()
    {
        return new List<Grid.GridPostion>(_gridPositions);
    }
}