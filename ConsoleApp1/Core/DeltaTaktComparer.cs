namespace ConsoleApp1.Core;

public class DeltaTaktComparer : IEqualityComparer<double>
{
    private readonly double _delta;

    public DeltaTaktComparer(double delta)
    {
        _delta = delta;
    }

    public bool Equals(double x, double y)
    {
        return Math.Abs(x - y) < _delta; //Avg(X,Y)-Y
    }

    public int GetHashCode(double obj)
    {
        return 0; // avoid to use hash code for comparison
    }
}