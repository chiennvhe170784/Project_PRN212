using ProjectPRN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

namespace ProjectPRN
{
    /// <summary>
    /// Interaction logic for EditQuestion.xaml
    /// </summary>
    public partial class EditQuestion : Window
    {
        private int qidSelect;
        public EditQuestion()
        {
            InitializeComponent();
            LoadData();
        }
        DataPrnquizContext context = new DataPrnquizContext();
        public void LoadData()
        {
            var data = context.Questions.Select(x => new
            {
                x.Qid,
                x.Question1,
                x.Answer,
                x.CorrectAnswer,
                x.Status
            }).Where(x=>x.Status==1).ToList();
            dgQuiz.ItemsSource = data;
        }

        private void SLCQuestion(object sender, SelectionChangedEventArgs e)
        {
            if (dgQuiz.SelectedItem == null)
            {
                return;
            }
            var q = dgQuiz.SelectedItem as dynamic;
            if (q != null)
            {
                string[] splitA = splitAns(q.Answer);
                txtQuestion.Text = q.Question1;
                if (splitA.Length > 3)
                {
                    txtAnswerA.Text = splitA[1];
                    txtAnswerB.Text = splitA[2];
                    txtAnswerC.Text = splitA[3];
                    txtAnswerD.Text = splitA[4];
                }
                if (splitA.Length <= 3)
                {
                    txtAnswerA.Text = splitA[1];
                    txtAnswerB.Text = splitA[2];
                }
                string correct = q.CorrectAnswer;
                qidSelect = q.Qid;
                switch (correct)
                {
                    case "A":
                        A.IsChecked = true; break;

                    case "B":
                        B.IsChecked = true; break;

                    case "C":
                        C.IsChecked = true; break;

                    case "D":
                        D.IsChecked = true; break;
                    default:
                        A.IsChecked = B.IsChecked = C.IsChecked = D.IsChecked = false;
                        break;
                }
            }

        }
        private String[] splitAns(string Answer)
        {
            string[] parts = Answer.Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Replace(".A", "").Replace(".B", "").Replace(".C", "").Replace(".D", "");
            }
            return parts;

        }
        private String addAnswer(string[] Ans)
        {
            string result = null;
            for (int i = 0; i < Ans.Length; i++)
            {
                char asciiChar = (char)(i + 65);
                result += "." + asciiChar + ")" + Ans[i] + "  ";
            }
            return result;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string question = txtQuestion.Text.Trim();
                string a = txtAnswerA.Text;
                string b = txtAnswerB.Text;
                string c = txtAnswerC.Text;
                string d = txtAnswerD.Text;
                string correct;

                if (A.IsChecked == true) { correct = "A"; }
                else if (B.IsChecked == true) { correct = "B"; }
                else if (C.IsChecked == true) correct = "C";
                else if (D.IsChecked == true) correct = "D";
                else { MessageBox.Show("Please choose correct answer"); return; }

                string[] aa = new string[] { a, b, c, d };
                string answer = addAnswer(aa);
                Question q = new Question()
                {
                    Question1 = question,
                    Answer = answer,
                    CorrectAnswer = correct
                };
                var data = context.Questions.FirstOrDefault(x => x.Question1.Trim().ToLower().Equals(question.ToLower()));
                if (data == null)
                {
                    context.Questions.Add(q);
                    context.SaveChanges();
                    MessageBox.Show("Add success");
                }
                else
                {
                    MessageBox.Show("Question already exist!");
                    return;
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtAnswerA.Clear();
            txtAnswerB.Clear();
            txtAnswerC.Clear();
            txtAnswerD.Clear();
            txtQuestion.Clear();
 
            A.IsChecked = B.IsChecked = C.IsChecked = D.IsChecked = false;

            qidSelect = -1;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgQuiz.SelectedItem == null)
            {
                MessageBox.Show("Please select item!");
                return;
            }
            Question data = context.Questions.FirstOrDefault(x => x.Qid==qidSelect);

            if (data != null)
            {
                var result = MessageBox.Show("Remove this question?", "remove", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (MessageBoxResult.OK == result )
                {
                    data.Status = 0;
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Delete success!");
                }
            }
            else
            {
                MessageBox.Show("Question not contains can't delete!");
                return;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgQuiz.SelectedItem == null)
            {
                MessageBox.Show("Please select item!");
                return;
            }
            Question data = context.Questions.FirstOrDefault(x => x.Qid == qidSelect);

            if (data != null)
            {
                var result = MessageBox.Show("Remove this question?", "remove", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (MessageBoxResult.OK == result)
                {
                    string[] aa = new string[] { txtAnswerA.Text, txtAnswerB.Text, txtAnswerC.Text, txtAnswerD.Text };
                    data.Question1=txtQuestion.Text;
                    data.Answer= addAnswer(aa);
                    string correct;

                    if (A.IsChecked == true) { correct = "A"; }
                    else if (B.IsChecked == true) { correct = "B"; }
                    else if (C.IsChecked == true) correct = "C";
                    else if (D.IsChecked == true) correct = "D";
                    else { MessageBox.Show("Please choose correct answer"); return; }
                    data.CorrectAnswer= correct;

                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Delete success!");
                }
            }
            else
            {
                MessageBox.Show("Question not contains can't delete!");
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateTest cr = new CreateTest();
            cr.Show();
            this.Close();
        }
    }
}
