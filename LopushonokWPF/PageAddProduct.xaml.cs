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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LopushonokWPF
{
    /// <summary>
    /// Логика взаимодействия для PageAddProduct.xaml
    /// </summary>
    public partial class PageAddProduct : Page
    {
        Product product ;
        public PageAddProduct(Product product)
        {
            InitializeComponent();
            this.product = product;
            cbType.ItemsSource = DB.db.ProductType.ToList();
            CheckProduct();
        }
        public void CheckProduct()
        {
            if (product.ArticleNumber != null || !String.IsNullOrEmpty(product.ArticleNumber))
            {
                tbTitle.Text = product.Title;
                tbArticle.Text = product.ArticleNumber.ToString();
                tbCount.Text = product.ProductionWorkshopNumber.ToString();
                tbDescription.Text = product.Description.ToString();
                tbPerson.Text = product.ProductionPersonCount.ToString();
                tbPrice.Text = product.MinCostForAgent.ToString();

                if (product.ProductType == null)
                {
                    imgMaterial.Source = new BitmapImage(new Uri("../products/picture.png", UriKind.Relative));
                    cbType.SelectedIndex = 0;
                }
                else
                {
                    imgMaterial.Source = new BitmapImage(new Uri(product.ValidImage, UriKind.Relative));
                    cbType.SelectedItem = product.ProductType;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTitle.Text) || string.IsNullOrWhiteSpace(tbArticle.Text) ||
                string.IsNullOrWhiteSpace(tbCount.Text) || string.IsNullOrWhiteSpace(tbPerson.Text) ||
                string.IsNullOrWhiteSpace(tbPrice.Text) ||
                cbType.SelectedItem == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                product.Title = tbTitle.Text;
                product.ArticleNumber = tbArticle.Text;
                product.Description = tbDescription.Text;
                product.ProductionPersonCount = int.Parse(tbPerson.Text);
                product.ProductionWorkshopNumber = int.Parse(tbCount.Text);
                product.MinCostForAgent = int.Parse(tbPrice.Text, System.Globalization.NumberStyles.Any);
                product.ProductType = (ProductType)cbType.SelectedItem;
                product.Image = null;
                if (product.ID == 0)
                {
                    DB.db.Product.Add(product); 
                }
                DB.db.SaveChanges();

                NavigationService.Navigate(new PageProducts());
            }
        }

        private void DelMat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
