using Microsoft.Extensions.Options;
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
using Microsoft.EntityFrameworkCore;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly DbContextOptionsBuilder<KotyaDbContext> optionsBuilder = new DbContextOptionsBuilder<KotyaDbContext>();
        public Window1()
        {
            InitializeComponent();
        }

        private async void okbtn_Click(object sender, RoutedEventArgs e)
        {
            Product newProduct = new Product
            {
                Name = tName.Text, price = decimal.Parse(tPrice.Text), Id = int.Parse(tId.Text)
            };
            using (var context = new KotyaDbContext(optionsBuilder.Options))
            {
                context.Products.Add(newProduct);
                await context.SaveChangesAsync();
            }
            Close();
        }
    }
}
