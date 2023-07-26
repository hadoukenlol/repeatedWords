namespace WordsCount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                string text = File.ReadAllText("D://lms-cdn.skillfactory.ru_assets_courseware_v1_dc9cf029ae4d0ae3ab9e490ef767588f_asset-v1_SkillFactory+CDEV+2021+type@asset+block_Text1.txt");
                char[] delimiters = new char[] { ' ', '\r', '\n' };                
                var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray()); //убираем знаки препинания
                var wordsInText = noPunctuationText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries); //отделяем слова - выносим их в массив
                var set = new HashSet<string>(); // список неповторяющихся слов
                var repeatedWord = new List<string>(); // список для повторяющихся слов
                foreach ( var word in wordsInText ) {
                    if (!set.Add(word)) //проверяем 
                    {
                        //добавляем повторяющиеся слова в отдельный списое
                        repeatedWord.Add(word);
                    }                        
                }
                repeatedWord.Sort();
                
                var result = repeatedWord.GroupBy(item => item).Select(item => new { Name = item.Key, Count = item.Count() }).OrderByDescending(item => item.Count).ThenBy(item => item.Name);
                String report = String.Join(Environment.NewLine, result.Select(item => String.Format($"{item.Name} встречается {item.Count + 1} раз")));

                Console.WriteLine(report);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Все готово");
            }
        }
    }
}