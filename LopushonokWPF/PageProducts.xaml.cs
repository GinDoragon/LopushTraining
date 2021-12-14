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
using System.IO;
using System.Windows.Shapes;

namespace LopushonokWPF
{
    /// <summary>
    /// Логика взаимодействия для PageProducts.xaml
    /// </summary>
    public partial class PageProducts : Page
    {
        public PageProducts()
        {
            InitializeComponent();
            LbProducts.ItemsSource = DB.db.Product.ToList();
            TbCountAll.Text += LbProducts.Items.Count.ToString();
            CbFilter.Items.Add("Фильтрация");
            foreach (var matT in DB.db.ProductType)
            {
                CbFilter.Items.Add(matT.Title);
            }
            CbFilter.SelectedIndex = 0;

            CbSort.Items.Add("Сортировка");
            CbSort.Items.Add("По наименованию");
            CbSort.Items.Add("По номеру цеха");
            CbSort.Items.Add("По минимальной стоимости");
            CbSort.SelectedIndex = 0;
        }

        public void FindProduct()
        {
            var products = DB.db.Product.Where(x => x.Title.Contains(TbSearch.Text)).ToList();

            switch (CbSort.SelectedIndex)
            {
                case 1:
                    if (RbASC.IsChecked == true)
                    {
                        products = products.OrderBy(m => m.Title).ToList();
                    }
                    else
                        products = products.OrderByDescending(m => m.Title).ToList();
                    break;
                case 2:
                    if (RbASC.IsChecked == true)
                    {
                        products = products.OrderBy(m => m.ProductionPersonCount).ToList();
                    }
                    else
                        products = products.OrderByDescending(m => m.ProductionPersonCount).ToList();
                    break;
                case 3:
                    if (RbASC.IsChecked == true)
                    {
                        products = products.OrderBy(m => m.MinCostForAgent).ToList();
                    }
                    else
                        products = products.OrderByDescending(m => m.MinCostForAgent).ToList();
                    break;
            }

            if (CbFilter.SelectedIndex > 0)
            {
                string matType = CbFilter.SelectedItem.ToString();
                products = products.Where(x => x.ProductType.ToString() == matType).ToList();
            }
            TbCount.Text = products.Count.ToString();
            LbProducts.ItemsSource = products.ToList();
        }

        private void BtnAddMat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAddProduct(new Product()));
        }

        private void BtnDelMat_Click(object sender, RoutedEventArgs e)
        {
            var SelectedProduct = LbProducts.SelectedItem;
            try
            {
                if (MessageBox.Show("Удалить объект?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DB.db.Product.Remove((Product)SelectedProduct);
                    DB.db.SaveChanges();

                    MessageBox.Show("Объект удален");

                    NavigationService.Navigate(new PageProducts());
                }
            }
            catch
            {

            }
        }

        private void BtnEditMat_Click(object sender, RoutedEventArgs e)
        {
            var SelectedProduct = LbProducts.SelectedItem;
            NavigationService.Navigate(new PageAddProduct((Product)SelectedProduct));
        }

        private void LbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindProduct();
            if (CbSort.SelectedIndex == 0)
            {
                RbASC.IsEnabled = false;
                RbDESC.IsEnabled = false;
            }
            else
            {
                RbASC.IsEnabled = true;
                RbDESC.IsEnabled = true;
            }
        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindProduct();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindProduct();
        }

        private void RbASC_Checked(object sender, RoutedEventArgs e)
        {
            FindProduct();
        }

        private void RbDESC_Checked(object sender, RoutedEventArgs e)
        {
            FindProduct();
        }
    }
}
