public static class ExtensionMethods 
{
    public static int ToInt(this bool b)
    {
        return b ? 1 : -1;
    } 
    
    public static int ToInt01(this bool b)
    {
        return b ? 1 : 0;
    }
}
