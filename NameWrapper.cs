public class NameWrapper
{
  public string ingameName;
  public string dataName;

  public NameWrapper(string iname, string dname)
  {
    this.ingameName = iname;
    this.dataName = dname;
  }

  public override string ToString()
  {
    return this.ingameName;
  }
}
