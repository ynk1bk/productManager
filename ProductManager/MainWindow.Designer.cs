using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProductManager
{
    public partial class MainWindow : Form
    {
        private List<Product> products = new List<Product>();

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                string name = nameTextBox.Text;
                decimal price = numericUpDown1.Value;
                int quantity = (int)numericUpDown2.Value;

                if (price <= 0)
                {
                    MessageBox.Show("Цена должна быть положительной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (quantity < 0)
                {
                    MessageBox.Show("Количество не может быть отрицательным!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                Product product = new Product(name, price, quantity);
                products.Add(product);
                MessageBox.Show("Товар добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateProductList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (productsList.SelectedIndex >= 0)
                {
                    products.RemoveAt(productsList.SelectedIndex);
                    MessageBox.Show("Товар удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateProductList();
                }
                else
                {
                    MessageBox.Show("Выберите товар для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (productsList.SelectedIndex >= 0)
                {
                    Product product = products[productsList.SelectedIndex];
                    string message = product.IsAvailable() ? "Товар доступен!" : "Товар недоступен!";
                    MessageBox.Show(message, "Статус", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Выберите товар для проверки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (productsList.SelectedIndex >= 0)
                {
                    // Получаем выбранный товар
                    Product selectedProduct = products[productsList.SelectedIndex];

                    // Обновляем данные товара из текстовых полей
                    selectedProduct.Name = nameTextBox.Text;
                    selectedProduct.Price = numericUpDown1.Value;
                    selectedProduct.Quantity = (int)numericUpDown2.Value;

                    // Обновляем список товаров
                    UpdateProductList();
                    MessageBox.Show("Товар отредактирован!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Выберите товар для редактирования!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void productsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (productsList.SelectedIndex >= 0)
            {
                Product selectedProduct = products[productsList.SelectedIndex];
                nameTextBox.Text = selectedProduct.Name;
                numericUpDown1.Value = selectedProduct.Price;
                numericUpDown2.Value = selectedProduct.Quantity;
            }
        }

        private void UpdateProductList()
        {
            productsList.Items.Clear();
            foreach (var product in products)
            {
                productsList.Items.Add(product.ToString());
            }
        }

        private TextBox nameTextBox;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private Label label2;
        private Label label3;
        private Button button1;
        private ListBox productsList;
        private Label label4;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
