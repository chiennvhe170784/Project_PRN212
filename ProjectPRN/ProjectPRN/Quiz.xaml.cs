using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client.NativeInterop;
using ProjectPRN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ProjectPRN
{
    /// <summary>
    /// Interaction logic for _3_Quiz.xaml
    /// </summary>
    public partial class Quiz : Window
    {
        private string code;
        private string user;
        private int currentIndex = 0;

        Dictionary<int, String> mapCheck = new Dictionary<int, String>();



        public Quiz(string code, String username)
        {
            InitializeComponent();
            this.code = code;
            this.user = username;
            LoadData(currentIndex);

        }
        DataPrnquizContext context = new DataPrnquizContext();

        private double calculateMark()
        {
            double mark = 0;
            List<Question> lq = getList();
            foreach (var entry in mapCheck)
            {
                int questionIndex = entry.Key;
                string selectedAnswer = entry.Value;
                Question question = lq[questionIndex];

                if (selectedAnswer == question.CorrectAnswer)
                {
                    mark++;
                }
            }

            return mark;
        }
        private void LoadData(int a)
        {
            List<Question> listQ = getList();

            separateQ(listQ[a]);

            if (mapCheck.ContainsKey(a))
            {
                string selectedAnswer = mapCheck[a];
                switch (selectedAnswer)
                {
                    case "A":
                        rdbA.IsChecked = true;
                        break;
                    case "B":
                        rdbB.IsChecked = true;
                        break;
                    case "C":
                        rdbC.IsChecked = true;
                        break;
                    case "D":
                        rdbD.IsChecked = true;
                        break;
                }
            }
            else
            {
                ResetButtonStates();
            }

        }

        private void separateQ(Question q)
        {

            string question = q.Question1;
            string answer = q.Answer;
            string correct = q.CorrectAnswer;
            string[] parts = answer.Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Replace(".A", "").Replace(".B", "").Replace(".C", "").Replace(".D", "");
            }
            if (parts.Length >= 3)
            {
                rdbA.Content = "A " + parts[1];
                rdbB.Content = "B " + parts[2];
                rdbC.Content = "C " + parts[3];
                rdbD.Content = "D " + parts[4];
            }
            else if (parts.Length == 2)
            {
                rdbA.Content = parts[1];
                rdbB.Content = parts[2];
            }
            QuestionTextBlock.Text = question;



        }
        //private String[] splitAns(string Answer)
        //{
        //    string[] parts = Answer.Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries);
        //    for (int i = 0; i < parts.Length; i++)
        //    {
        //        parts[i] = parts[i].Replace(".A", "").Replace(".B", "").Replace(".C", "").Replace(".D", "");
        //    }
        //    return parts;

        //}
        private void SaveCurrentAnswer()
        {
            if (rdbA.IsChecked == true)
            {
                mapCheck[currentIndex] = "A";
            }
            else if (rdbB.IsChecked == true)
            {
                mapCheck[currentIndex] = "B";
            }
            else if (rdbC.IsChecked == true)
            {
                mapCheck[currentIndex] = "C";
            }
            else if (rdbD.IsChecked == true)
            {
                mapCheck[currentIndex] = "D";
            }
        }

        private void ResetButtonStates()
        {
            rdbA.IsChecked = false;
            rdbB.IsChecked = false;
            rdbC.IsChecked = false;
            rdbD.IsChecked = false;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentAnswer();
            currentIndex++;
            if (currentIndex >= getList().Count)
            {
                currentIndex = 0;
            }
            LoadData(currentIndex);

        }
        private List<Question> getList()
        {
            List<int> listQ = context.TestQuestions.Where(x => x.Code == code).Select(x => x.Qid).ToList();
            List<Question> listQuestions = context.Questions.Where(x => listQ.Contains(x.Qid)).ToList();
            return listQuestions;
        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to finish!", "finish exam", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                double markR = 10 * (double) calculateMark() / getList().Count;
                History history = new History { 
                    Uid = context.Users.Where(x => x.Username==user).Select(u => u.Uid).FirstOrDefault(),
                    Code=code,
                    Date = DateTime.Now,
                    Mark = markR.ToString("F2")
                };
                context.Add(history);
                context.SaveChanges();
                InputCode ipc = new InputCode(user);
                ipc.Show();
                this.Close();
               
            }

            //SaveCurrentAnswer();
            //MessageBox.Show(""+calculateMark());
        }
    }
}
