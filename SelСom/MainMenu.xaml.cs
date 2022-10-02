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

                new ApplicantsTableAdapter().InsertQuery(Surname.Text, Name.Text, Patronymic.Text, SexBox.SelectedValue.ToString(), DateOfBirth.Text, Email.Text, PhoneNum.Text, "sdfsdf", "sdfsdf", NewIdPassport, NewIdAchievment, NewIdCertificate);
                UpdateApplicants();
            }
            catch { }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
