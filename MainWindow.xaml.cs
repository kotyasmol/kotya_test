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
using Microsoft.EntityFrameworkCore; // фикс подчеркивания говна

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        string vStrConnecion = "Server=localhost; port=5432 ; user id =postgres ; password =kotya; database = ft_bd_1; ";

        public MainWindow()
        {
            InitializeComponent();
        }

        public DataTable GetData(string sql)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection vCon = new NpgsqlConnection(vStrConnecion))
            {
                vCon.Open();
                using (NpgsqlCommand vCmd = new NpgsqlCommand(sql, vCon))
                {
                    using (NpgsqlDataReader dr = vCmd.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }
            }
            return dt;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataTable dtgetdata = GetData("select * from products;");

            dgData.ItemsSource = dtgetdata.DefaultView;
        }


        private void DeleteSelectedRow()
        {
            DataRowView selectedRow = (DataRowView)dgData.SelectedItem;
            if (selectedRow != null)
            {
                int id = Convert.ToInt32(selectedRow["id"]); 
                string deleteQuery = $"DELETE FROM products WHERE id = {id}";

                using (NpgsqlConnection vCon = new NpgsqlConnection(vStrConnecion))
                {
                    vCon.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(deleteQuery, vCon))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                // Обновляем DataGrid после удаления строки
                DataTable updatedData = GetData("SELECT * FROM products");
                dgData.ItemsSource = updatedData.DefaultView;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedRow();
        }

    }
}