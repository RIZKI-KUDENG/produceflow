namespace ProduceFlow.Application.Common.Attributes;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CachedAttribute : Attribute
{
    public int ExpiredMinutes {get;}
    public string CustomKey { get; set; }

    public CachedAttribute(int expiredMinutes = 5, string customKey = "")
    {
        ExpiredMinutes = expiredMinutes;
        CustomKey = customKey;
    }
}