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
using Microsoft.EntityFrameworkCore;



namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private readonly DbContextOptionsBuilder<KotyaDbContext> optionsBuilder = new DbContextOptionsBuilder<KotyaDbContext>();
        public MainWindow()
        {
            InitializeComponent();
            optionsBuilder.UseNpgsql("Server=localhost; port=5432 ; user id =postgres ; password =kotya; database = ft_bd_1; ");
            using (var context = new KotyaDbContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
            }
        }

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            List<Product> products;
            using (var context = new KotyaDbContext(optionsBuilder.Options))
            {
                products = await context.Products.ToListAsync();
            }
            dgData.ItemsSource = products;
        }

        private async void delete_button_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = (Product)dgData.SelectedItem;
            if (selectedProduct != null) 
            {

                using (var context = new KotyaDbContext(optionsBuilder.Options))
                {
                    context.Products.Remove(selectedProduct);
                    await context.SaveChangesAsync();
                }
                var products = (List<Product>)dgData.ItemsSource;
                products.Remove(selectedProduct);
                dgData.ItemsSource = null;
                dgData.ItemsSource = products;
            }

        }
        private async void RefreshData()
        {
            using (var context = new KotyaDbContext(optionsBuilder.Options))
            {
                var products = await context.Products.ToListAsync();
                dgData.ItemsSource = products;
            }
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();
            RefreshData();

        }
    }
}