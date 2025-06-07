public class Grid<TGridObject>
{
    #region Variables / Components

    private int _width = 0;
    private int _height = 0;
    private TGridObject[,] _gridArray;

    #endregion

    #region Properties

    public int width => _width;
    public int height => _height;
    public TGridObject[,] gridArray => _gridArray;

    #endregion
        
    #region Constructor

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;
        _gridArray = new TGridObject[width, height];
    }

    #endregion

    #region Methods
    
    public void SetValue(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
            _gridArray[x, y] = value;
    }
        
    public TGridObject GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
            return _gridArray[x, y];
            
        return default;
    }
    #endregion
}