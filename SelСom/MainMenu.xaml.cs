using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SelСom.DataSet_SelComTableAdapters;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace SelСom
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();

            SexBox.Items.Add("М");
            SexBox.Items.Add("Ж");

            StatusBox.Items.Add("В обработке");
            StatusBox.Items.Add("Конкурс");
            StatusBox.Items.Add("Зачислен");
            StatusBox.Items.Add("Отклонен");

            UpdateApplicants();
            UpdatePassports();
            UpdateCertificates();
            UpdateAchievements();
        }

        private void UpdateApplicants()
        {
            ApplicantsViewTableAdapter adapter = new ApplicantsViewTableAdapter();
            DataSet_SelCom.ApplicantsViewDataTable table = new DataSet_SelCom.ApplicantsViewDataTable();
            adapter.Fill(table);

            ApplicantsGrid.ItemsSource = table;
        }

        private void UpdatePassports()
        {
            PassportsTableAdapter adapter = new PassportsTableAdapter();
            DataSet_SelCom.PassportsDataTable table = new DataSet_SelCom.PassportsDataTable();
            adapter.Fill(table);

            //ApplicantsGrid.ItemsSource = table;
        }

        private void UpdateCertificates()
        {
            CertificatesTableAdapter adapter = new CertificatesTableAdapter();
            DataSet_SelCom.CertificatesDataTable table = new DataSet_SelCom.CertificatesDataTable();
            adapter.Fill(table);

            //ApplicantsGrid.ItemsSource = table;
        }


        private void UpdateAchievements()
        {
            AchievementsTableAdapter adapter = new AchievementsTableAdapter();
            DataSet_SelCom.AchievementsDataTable table = new DataSet_SelCom.AchievementsDataTable();
            adapter.Fill(table);

            //ApplicantsGrid.ItemsSource = table;
        }

       

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new PassportsTableAdapter().InsertQuery(SeriesPass.Text, NumPass.Text, PassIssueDate.Text, DivCode.Text, Issued.Text, "sdfsdf", "sdfsdf");
                int NewIdPassport = Convert.ToInt32(new ApplicantsTableAdapter().GetPassportInsertedID(SeriesPass.Text, NumPass.Text));

                new AchievementsTableAdapter().InsertQuery(gto.IsChecked ?? false, olimp.IsChecked ?? false);
                int NewIdAchievment = Convert.ToInt32(new ApplicantsTableAdapter().GetAchievmentInsertedID());

                new CertificatesTableAdapter().InsertQuery(NumSert.Text, SertIssueDate.Text, Education.Text, "sdfsdf", "sdfsdf", Convert.ToDecimal(GPA.Text), SertificateOrig.IsChecked ?? false);
                int NewIdCertificate = Convert.ToInt32(new ApplicantsTableAdapter().GetSertificateInsertedID(NumSert.Text));
                MessageBox.Show(NewIdCertificate.ToString());

                new ApplicantsTableAdapter().InsertQuery(Surname.Text, Name.Text, Patronymic.Text, SexBox.SelectedValue.ToString(), DateOfBirth.Text, Email.Text, PhoneNum.Text, "sdfsdf", StatusBox.SelectedValue.ToString(), NewIdPassport, NewIdAchievment, NewIdCertificate);
                UpdateApplicants();

                int NewIdApplicant = Convert.ToInt32(new ApplicantsTableAdapter().GetApplicantInsertedID(NewIdPassport));

                var chekedContents = ApplicantSpecBox.Items.OfType<ComboBoxItem>().Where(cbi => (((StackPanel)cbi.Content).Children[0].GetType() == typeof(CheckBox)) &
                                                                        (((CheckBox)((StackPanel)cbi.Content).Children[0]).IsChecked == true)
                                                                        ).Select(cbi => new { ((CheckBox)((StackPanel)cbi.Content).Children[0]).Content }.ToString());
                List<string> chekedContentsLst = chekedContents.ToList();

                for (int i = 0; i <= chekedContentsLst.Count-1; i++)
                {
                    new Applicants_SpecialtiesTableAdapter().InsertQuery(NewIdApplicant, chekedContentsLst[i].Substring(12, 8)); 
                }

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StatusBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void ApplicantsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int ApplicantId = Convert.ToInt32((ApplicantsGrid.SelectedItem as DataRowView).Row.ItemArray[0].ToString());

                var datarow = new ApplicantsTableAdapter().GetNormalApplicantView(ApplicantId);

                Surname.Text = datarow.Rows[0]["Surname"].ToString();
                Name.Text = datarow.Rows[0]["Name"].ToString();
                Patronymic.Text = datarow.Rows[0]["Patronymic"].ToString();
                PhoneNum.Text = datarow.Rows[0]["Phone_Number"].ToString();
                DateOfBirth.SelectedDate = Convert.ToDateTime(datarow.Rows[0]["Birth_Date"].ToString());
                Email.Text = datarow.Rows[0]["Email"].ToString();
                SexBox.SelectedValue = datarow.Rows[0]["Sex"].ToString();
                SeriesPass.Text = datarow.Rows[0]["Series"].ToString();
                NumPass.Text = datarow.Rows[0]["Number"].ToString();
                DivCode.Text = datarow.Rows[0]["Division_Code"].ToString();
                PassIssueDate.SelectedDate = Convert.ToDateTime(datarow.Rows[0]["Issue_Date_Passport"].ToString());
                Issued.Text = datarow.Rows[0]["Issued"].ToString();
                NumSert.Text = datarow.Rows[0]["Number_Sert"].ToString();
                SertIssueDate.SelectedDate = Convert.ToDateTime(datarow.Rows[0]["Issue_Date_Sertificate"].ToString());
                GPA.Text = datarow.Rows[0]["GPA"].ToString();
                Education.Text = datarow.Rows[0]["Educational_Institution"].ToString();
                gto.IsChecked = Convert.ToBoolean(datarow.Rows[0]["Gold_GTO"]);
                olimp.IsChecked = Convert.ToBoolean(datarow.Rows[0]["Olympic_Medalist"]);
                SertificateOrig.IsChecked = Convert.ToBoolean(datarow.Rows[0]["Certificate_Original"]);
                StatusBox.SelectedValue = datarow.Rows[0]["Status"].ToString();

                chB090207.IsChecked = false;
                chB090201.IsChecked = false;
                chB090206.IsChecked = false;

                var AppSpec = new Applicants_SpecialtiesTableAdapter().GetApplicantSpec(ApplicantId);

                for (int i = 0; i < AppSpec.Rows.Count; i++)
                {
                    switch (AppSpec.Rows[i][2].ToString())
                    {
                        case "09.02.07":
                            chB090207.IsChecked = true;
                            break;
                        case "09.02.01":
                            chB090201.IsChecked = true;
                            break;
                        case "09.02.06":
                            chB090206.IsChecked = true;
                            break;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
