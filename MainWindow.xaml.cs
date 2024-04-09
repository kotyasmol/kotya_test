using Microsoft.EntityFrameworkCore.ChangeTracking;
using Npgsql;
using System.Data;
using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string vStrConnecion = "Server=localhost; port=5432 ; user id =postgres ; password =kotya; database = ft_bd_1; ";
        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;

        private void connection()
        {
            vCon = new NpgsqlConnection();
            vCon.ConnectionString = vStrConnecion;
            if (vCon.State == ConnectionState.Closed) 
            {
                vCon.Open();
            }
            vCon.Open();
        }

        public DataTable GetData(string sql)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText=sql;
            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);

            return dt;
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            connection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtgetdata = new DataTable();
            dtgetdata = GetData("select * from products;");
        }
    }
}