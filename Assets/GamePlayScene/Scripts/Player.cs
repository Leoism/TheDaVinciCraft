public class Player
{
  public int Points;
  public string Name = "Default";
  public string Type = "Default";
  public Player Clone(int? newPoints = null, string newName = null, string newType = null)
  {
    return new Player
    {
      Points = newPoints ?? Points,
      Name = newName ?? Name,
      Type = newType ?? Type
    };
  }
}
