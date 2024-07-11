using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using ProjectPRN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
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
    /// Interaction logic for _5_CreateTest.xaml
    /// </summary>
    public partial class CreateTest : Window
    {
        private List<int> listQID = new List<int>();
        public CreateTest()
        {
            InitializeComponent();

            LoadData();
        }


        private void LoadData()
        {
            try
            {
                var data = context.Questions.Select(q => new
                {
                    q.Qid,
                    q.Question1,
                    q.Answer,
                    q.CorrectAnswer,

                }).ToList();
                QuestionListBox.ItemsSource = data;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        DataPrnquizContext context = new DataPrnquizContext();

        private void dataQuiz1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
     
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
           
            if (sender is CheckBox checkBox)
            {
                if (checkBox.Tag is int questionId)
                {
                    if (!txtCode.Text.IsNullOrEmpty() && !txtName.Text.IsNullOrEmpty())
                    {
                        if (checkBox.IsChecked == true && (context.TestQuestions.FirstOrDefault(x => x.Code == txtCode.Text && x.Qid == questionId) == null))
                        {
                            MessageBox.Show($"Đã chọn câu hỏi có Id: {questionId}");
                            if (!listQID.Contains(questionId)) { listQID.Add(questionId); }
                        }
                        

                    }
                    else
                    {
                        checkBox.IsChecked = false;
                        MessageBox.Show("You must input code and name of test");
                    }

                }
            }
            else
            {

                Console.WriteLine("CheckBox is not checked or Tag is not int.");
            }
            numberQ.Text = listQID.Count.ToString();
        }

        private void AddQuestion(int id)
        {
            if (context.Questions.FirstOrDefault(q => q.Qid == id) != null)
            {

                var existingTest = context.Tests.FirstOrDefault(t => t.Code == txtCode.Text);


                if (existingTest != null)
                {
                    //update size cua bai test
                    existingTest.Size = context.TestQuestions.Count(x => x.Code == existingTest.Code);
                    existingTest.TestName = txtName.Text;
                    context.SaveChanges();

                }
                else
                {
                    // Nếu không tồn tại, tạo mới một bài test
                    existingTest = new Test()
                    {
                        Code = txtCode.Text,
                        TestName = txtName.Text,
                    };
                    context.Tests.Add(existingTest);
                    context.SaveChanges();

                }


                TestQuestion tq = new TestQuestion()
                {
                    Qid = id,
                    Code = txtCode.Text,
                };
                if (context.TestQuestions.FirstOrDefault(x => x.Qid == tq.Qid && x.Code == tq.Code) == null)
                {
                    context.TestQuestions.Add(tq);
                    MessageBox.Show(tq.Code);
                   

                }
             
             


            }
            else
            {
                Console.WriteLine("Add fail!", "Null Question", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        //private void RemoveQ(int id)
        //{
        //    if (txtCode.Text.IsNullOrEmpty() && txtName.Text.IsNullOrEmpty()) return;


        //    if (context.Questions.FirstOrDefault(q => q.Qid == id) != null)
        //    {


        //        var Tq = context.TestQuestions.FirstOrDefault(x => x.Qid == id && x.Code == txtCode.Text);
        //        if (Tq != null)
        //        {
        //            context.TestQuestions.Remove(Tq);
        //            context.SaveChanges();
        //            numberQ.Text = context.TestQuestions.Count(x => x.Code == txtCode.Text).ToString();
        //        }
        //        else
        //        {
        //            return;
        //        }

        //    }
        //    else
        //    {
        //        Console.WriteLine("Add fail!", "Null Question", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //}
        private void removeT()
        {
            context.Remove(context.TestQuestions.Where(x => x.Code == txtCode.Text).ToList());
            context.SaveChanges();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (txtCode.Text.IsNullOrEmpty() && txtName.Text.IsNullOrEmpty()) return;
            if (sender is CheckBox checkBox)
            {
                if (checkBox.Tag is int questionId)
                {

                    if (checkBox.IsChecked == false)
                    {

                        MessageBox.Show($"Đã bỏ chọn câu hỏi có Id: {questionId}");
                        listQID.Remove(questionId);
                    }

                }
            }
            numberQ.Text = listQID.Count.ToString();
        }

        private void RbtnChecked(object sender, RoutedEventArgs e)
        {
            if (numberQ == null)
            {
                return;
            }
            RadioButton rdbtn = (RadioButton)sender;
            if (rdbtn.IsChecked != null)
            {
                if (rdbtn == ChooseQuestion)
                {
                    numberQ.Clear();
                    numberQ.IsReadOnly = true;

                }
                else
                {
                    numberQ.IsReadOnly = false;
                }
            }

        }

        private void clickTBLX(object sender, RoutedEventArgs e)
        {
            if (QuestionListBox.SelectedItem == null)
            {
                return;
            }
            var q = QuestionListBox.SelectedItem as dynamic;
            answerQ.Text = q.Answer;

        }

        private List<Question> getRandomQ(int numQ)
        {
            List<Question> l1 = context.Questions.ToList();
            Random rd = new Random();
            List<Question> result = l1.OrderBy(i => rd.Next()).Take(numQ).ToList();

            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
           
            try
            {
               
               


                if (context.Tests.FirstOrDefault(x => x.Code == txtCode.Text) != null)
                {
                    MessageBox.Show("Code test da ton tai!", "Duplicated key", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if (context.Tests.FirstOrDefault(x => x.TestName == txtName.Text) != null)
                {
                    MessageBox.Show("Name test da ton tai!", "Duplicated key", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
              
                
                if (ChooseQuestion.IsChecked==true)
                {

                    foreach (var id in listQID)
                    {
                        AddQuestion(id);
                    }
                    context.SaveChanges();
                    MessageBox.Show("Create test success!");
                }else if (RandomQuestion.IsChecked==true) {
                    List<Question> listQ1 = getRandomQ(Convert.ToInt32(numberQ.Text));
                    foreach (var question in listQ1) {
                        AddQuestion(question.Qid);
                    }
                    context.SaveChanges();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu vào cơ sở dữ liệu: {ex.Message}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
     
            
            Login lg = new Login();
            lg.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

    }
}
