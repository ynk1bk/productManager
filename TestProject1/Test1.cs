using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManager;

[TestClass]
public class ProductTests
{
    [TestMethod]
    public void EditProductName_ShouldUpdateName()
    {
        // Arrange
        var product = new Product("Тестовый товар", 100, 10);

        // Act
        product.Name = "Новое название";

        // Assert
        Assert.AreEqual("Новое название", product.Name);
    }

    [TestMethod]
    public void EditProductPrice_ShouldUpdatePrice()
    {
        // Arrange
        var product = new Product("Тестовый товар", 100, 10);

        // Act
        product.Price = 200;

        // Assert
        Assert.AreEqual(200, product.Price);
    }
    [TestMethod]
    public void EditProductQuantity_ShouldUpdateQuantity()
    {
        // Arrange
        var product = new Product("Тестовый товар", 100, 10);

        // Act
        product.Quantity = 20;

        // Assert
        Assert.AreEqual(20, product.Quantity);
    }
    [TestMethod]
    public void EditProduct_ToString_ShouldReturnCorrectString()
    {
        // Arrange
        var product = new Product("Тестовый товар", 100, 10);

        // Act
        product.Name = "Новое название";
        product.Price = 200;
        product.Quantity = 5;

        // Assert
        Assert.AreEqual("Товар: Новое название, Цена: 200, Количество: 5", product.ToString());
    }

    [TestMethod]
    public void EditProduct_IsAvailable_ShouldReturnCorrectStatus()
    {
        // Arrange
        var product = new Product("Тестовый товар", 100, 10);

        // Act
        product.Quantity = 0;

        // Assert
        Assert.IsFalse(product.IsAvailable());
    }
    [TestMethod]
    public void EditProductInUI_ShouldUpdateProductList()
    {
        // Arrange
        var mainWindow = new MainWindow();

        // Используем рефлексию для доступа к приватным полям
        var productsField = typeof(MainWindow).GetField("products", BindingFlags.NonPublic | BindingFlags.Instance);
        var nameTextBoxField = typeof(MainWindow).GetField("nameTextBox", BindingFlags.NonPublic | BindingFlags.Instance);
        var numericUpDown1Field = typeof(MainWindow).GetField("numericUpDown1", BindingFlags.NonPublic | BindingFlags.Instance);
        var numericUpDown2Field = typeof(MainWindow).GetField("numericUpDown2", BindingFlags.NonPublic | BindingFlags.Instance);
        var productsListField = typeof(MainWindow).GetField("productsList", BindingFlags.NonPublic | BindingFlags.Instance);

        // Инициализация приватных полей
        var products = new List<Product>();
        productsField.SetValue(mainWindow, products);

        var nameTextBox = new TextBox();
        nameTextBoxField.SetValue(mainWindow, nameTextBox);

        var numericUpDown1 = new NumericUpDown();
        numericUpDown1Field.SetValue(mainWindow, numericUpDown1);

        var numericUpDown2 = new NumericUpDown();
        numericUpDown2Field.SetValue(mainWindow, numericUpDown2);

        var productsList = new ListBox();
        productsListField.SetValue(mainWindow, productsList);
      
        // Добавляем тестовый товар
        var product = new Product("Тестовый товар", 100, 10);
        products.Add(product);
        productsList.Items.Add(product.ToString());

        // Эмуляция выбора товара в UI
        productsList.SelectedIndex = 0;

        // Эмуляция ввода новых данных
        nameTextBox.Text = "Новое название";
        numericUpDown1.Value = 99;
        numericUpDown2.Value = 5;

        // Используем рефлексию для вызова приватного метода
        var editButtonClickMethod = typeof(MainWindow).GetMethod("editButton_Click", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        editButtonClickMethod.Invoke(mainWindow, new object[] { null, null });

        // Assert
        Assert.AreEqual("Новое название", product.Name);
        Assert.AreEqual(99, product.Price);
        Assert.AreEqual(5, product.Quantity);
    }
}