using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Module14
{
    public class Practice
    {

        public static void Start()
        {
            Console.WriteLine("Введите текст: ");
            string text = Console.ReadLine();
            // Удаляем лишние символы и разбиваем текст на слова
            string[] words = Regex.Split(text.ToLower(), @"\W+");

            // Используем словарь для подсчета встречаемости слов
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    if (wordCount.ContainsKey(word))
                    {
                        wordCount[word]++;
                    }
                    else
                    {
                        wordCount[word] = 1;
                    }
                }
            }

            // Выводим статистику в виде таблицы
            Console.WriteLine("Слово\t\tКоличество");
            Console.WriteLine("------------------------");

            foreach (var entry in wordCount)
            {
                Console.WriteLine($"{entry.Key}\t\t{entry.Value}");
            }
        }
    }
}
