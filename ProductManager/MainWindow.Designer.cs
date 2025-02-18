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
    }
}
