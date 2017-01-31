namespace RBD
{
    public static class FbsInportStringExtension
    {
        public static string toFbsName(this string str)
        {
            return str != null ? str.ToLower().Replace("ё", "е").Replace(" ", "") : null;
        }

        public static string toFbsDoc(this string str)
        {
            return str != null ? str.ToLower().Replace(" ", "") : null;
        }
    }
}