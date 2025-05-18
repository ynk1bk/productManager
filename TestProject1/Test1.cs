using System.Reflection;
//using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductManager;
using FlaUI.UIA3;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using FlaUI.UIA3.Patterns;
using FlaUI.Core.Definitions;





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
    //    [TestMethod]
    //    public void EditProductInUI_ShouldUpdateProductList()
    //    {
    //        // Arrange
    //        var mainWindow = new MainWindow();

    //        // Используем рефлексию для доступа к приватным полям
    //        var productsField = typeof(MainWindow).GetField("products", BindingFlags.NonPublic | BindingFlags.Instance);
    //        var nameTextBoxField = typeof(MainWindow).GetField("nameTextBox", BindingFlags.NonPublic | BindingFlags.Instance);
    //        var numericUpDown1Field = typeof(MainWindow).GetField("numericUpDown1", BindingFlags.NonPublic | BindingFlags.Instance);
    //        var numericUpDown2Field = typeof(MainWindow).GetField("numericUpDown2", BindingFlags.NonPublic | BindingFlags.Instance);
    //        var productsListField = typeof(MainWindow).GetField("productsList", BindingFlags.NonPublic | BindingFlags.Instance);

    //        // Инициализация приватных полей
    //        var products = new List<Product>();
    //        productsField.SetValue(mainWindow, products);

    //        var nameTextBox = new TextBox();
    //        nameTextBoxField.SetValue(mainWindow, nameTextBox);

    //        var numericUpDown1 = new NumericUpDown();
    //        numericUpDown1Field.SetValue(mainWindow, numericUpDown1);

    //        var numericUpDown2 = new NumericUpDown();
    //        numericUpDown2Field.SetValue(mainWindow, numericUpDown2);

    //        var productsList = new ListBox();
    //        productsListField.SetValue(mainWindow, productsList);

    //        // Добавляем тестовый товар
    //        var product = new Product("Тестовый товар", 100, 10);
    //        products.Add(product);
    //        productsList.Items.Add(product.ToString());

    //        // Эмуляция выбора товара в UI
    //        productsList.SelectedIndex = 0;

    //        // Эмуляция ввода новых данных
    //        nameTextBox.Text = "Новое название";
    //        numericUpDown1.Value = 99;
    //        numericUpDown2.Value = 5;

    //        // Используем рефлексию для вызова приватного метода
    //        var editButtonClickMethod = typeof(MainWindow).GetMethod("editButton_Click", BindingFlags.NonPublic | BindingFlags.Instance);

    //        // Act
    //        editButtonClickMethod.Invoke(mainWindow, new object[] { null, null });

    //        // Assert
    //        Assert.AreEqual("Новое название", product.Name);
    //        Assert.AreEqual(99, product.Price);
    //        Assert.AreEqual(5, product.Quantity);
    //    }
    //}
}
[TestClass]
public class ProductManagerTests
{
    private Application _app;
    private UIA3Automation _automation;
    private Window _mainWindow;

    [TestInitialize]
    public void TestInitialize()
    {
        // Запуск приложения перед каждым тестом
        _app = Application.Launch(@"D:\Users\User\source\repos\productManager\ProductManager\bin\Debug\ProductManager.exe");
        _automation = new UIA3Automation();
        _mainWindow = _app.GetMainWindow(_automation);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        // Закрытие приложения после каждого теста
        _automation?.Dispose();
        _app?.Close();
    }

    [TestMethod]
    public void TestAddProduct()
    {
        // Arrange
        var nameTextBox = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("nameTextBox")).AsTextBox();
        var priceInput = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("numericUpDown1"));
        var price = priceInput.FindFirstDescendant(cf => cf.ByName("Наименование товара"));
        var quantityInput = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("numericUpDown2"));
        var quantity = quantityInput.FindFirstDescendant(cf => cf.ByName("Вертушка"));
        var addButton = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("button1")).AsButton();

        // Act
        nameTextBox.Text = "Тестовый товар";
        price.Patterns.Value.Pattern.SetValue("10");
        quantity.Patterns.Value.Pattern.SetValue("5");
        addButton.Click();
        var messageBox = _mainWindow.ModalWindows.FirstOrDefault();
        var okButton = messageBox.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton() ?? throw new Exception("кнопка  не найдена.");
        okButton.Click();
        
        // Assert
        var productsList = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("productsList")).AsListBox();
        var productText = productsList.Items.First().Text;
        Assert.IsTrue(productText.Contains("Товар: Тестовый товар, Цена: 10, Количество: 5"),"Товар не был добавлен или формат строки неверный");
    }

    [TestMethod]
    public void TestCheckProductAvailability()
    {
        // Arrange
        // Сначала добавляем товар для проверки
        TestAddProduct();
        var productsList = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("productsList")).AsListBox();
        var product = productsList.Items[0];
        var checkButton = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("button3")).AsButton();

        // Act
        product.Click();        
        checkButton.Click();
        var messageBox = _mainWindow.ModalWindows.FirstOrDefault();
        var msgText = messageBox.FindFirstDescendant(cf => cf.ByAutomationId("65535"));
        string mText = msgText.Name;

        // Assert
        StringAssert.Contains(mText, "Товар доступен!","Должен быть текст: Товар доступен!");
    }

    [TestMethod]
    public void TestDeleteProduct()
    {
        // Arrange
        // Сначала добавляем товар для удаления
        TestAddProduct();
        var productsList = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("productsList")).AsListBox();
        var deleteButton = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("button2")).AsButton();

        // Act
        productsList.Items[0].Select();
        deleteButton.Click();
        var messageBox = _mainWindow.ModalWindows.FirstOrDefault();
        var okButton = messageBox.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();
        okButton.Click();

        // Assert
        Assert.AreEqual(0, productsList.Items.Length, "Товар не был удалён");
    }
}