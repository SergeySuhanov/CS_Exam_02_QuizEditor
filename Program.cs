using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CS_Exam_02_QuizEditor
{
    public class Question
    {
        public string QID;
        public string Discipline;
        public string QuestionText;
        public string[] AnswerText;
        public bool[] IsCorrect;
        
        public Question()
        {
            AnswerText = new string[4];
            IsCorrect = new bool[4];
        }

        
        
    }

    class Editor
    {
        public string FolderPath;
        Question Question;

        public Editor()
        {
            FolderPath = @"C:\CS_Exam_02_Quiz\Questions";
        }

        public int CheckFreeQID(string testID)
        {
            string[] Questions = Directory.GetFiles(FolderPath);
            string checkID;
            for (int i = 0; i < Questions.Length; i++)
            {
                checkID = Questions[i].Substring(FolderPath.Length, 4);
                if (checkID == testID)
                {
                    return i;
                }
            }
            return -1;
        }

        public void SaveQuestion(Question question)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(Question));
            try
            {
                using (Stream fStream = File.Create($"{FolderPath}\\{question.QID}_{question.Discipline}.xml"))
                {
                    xmlFormat.Serialize(fStream, question);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Editor editor = new Editor();

            Console.WriteLine("Редактор вопросов");
            Console.WriteLine("1 - Создать новый вопрос");
            Console.WriteLine("2 - Изменить существующий");
            int editChoice = Int32.Parse(Console.ReadLine());

            switch (editChoice)
            {
                case 1:
                    Question newQuestion = new Question();
                    Console.WriteLine("Редактор вопросов");
                    Random rnd = new Random();
                    int randomNum = rnd.Next(1, 10000);
                    string s = String.Format("{0:D4}", randomNum);
                    Console.WriteLine($"s is {s}");
                    while (editor.CheckFreeQID(s) != -1)
                    {
                        randomNum = rnd.Next(1, 10000);
                        s = String.Format("{0:D4}", randomNum);
                    }
                    newQuestion.QID = s;

                    Console.WriteLine("Выберите дисциплину");
                    Console.WriteLine("1 - История");
                    Console.WriteLine("2 - География");
                    Console.WriteLine("3 - Животные");
                    int inp = Int32.Parse(Console.ReadLine());
                    switch (inp)
                    {
                        case 1:
                            newQuestion.Discipline = "History";
                            break;
                        case 2:
                            newQuestion.Discipline = "Geography";
                            break;
                        case 3:
                            newQuestion.Discipline = "Animals";
                            break;
                        default:
                            break;
                    }

                    Console.WriteLine("Введите текст вопроса: ");
                    newQuestion.QuestionText = Console.ReadLine();
                    for (int i = 0; i < newQuestion.AnswerText.Length; i++)
                    {
                        Console.WriteLine($"Введите вариант ответа №{i+1}: ");
                        newQuestion.AnswerText[i] = Console.ReadLine();
                        Console.WriteLine($"Данный ответ верный?");
                        Console.WriteLine($"Верный - нажмите 1");
                        Console.WriteLine($"Неверный - нажмите любую другую клавишу");

                        int tf = Int32.Parse(Console.ReadLine());
                        switch (tf)
                        {
                            case 1:
                                newQuestion.IsCorrect[i] = true;
                                break;
                            default:
                                newQuestion.IsCorrect[i] = false;
                                break;
                        }
                    }
                    

                    editor.SaveQuestion(newQuestion);
                    Console.WriteLine("Вопрос сохранён в файл");
                    break;
                default:
                    break;
            }
            Console.ReadLine();
        }
    }
}
