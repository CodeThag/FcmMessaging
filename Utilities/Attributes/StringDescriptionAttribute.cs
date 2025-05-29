namespace Utilities.Attributes;

public class StringDescriptionAttribute : Attribute
{
    public string StringDescription { get; set; }

    public StringDescriptionAttribute(string value)
    {
        this.StringDescription = value;
    }
}