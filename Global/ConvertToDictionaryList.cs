namespace WebMVC2.Global
{
    public static class ConvertToDictionaryList
    {
        public static List<Dictionary<string, string>> Convert<T>(List<T> list)
        {
            var dictionaryList = new List<Dictionary<string, string>>();

            foreach (var item in list)
            {
                var dict = new Dictionary<string, string>();
                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    var value = property.GetValue(item)?.ToString() ?? string.Empty;
                    dict[property.Name] = value;
                }

                dictionaryList.Add(dict);
            }

            return dictionaryList;
        }
    }
}
