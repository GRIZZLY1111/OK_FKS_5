using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pract5
{
    public class SeleniumSiteTests
    {
        IWebDriver _webDriver = new ChromeDriver();
        [Fact]
        public void VhodPage()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            const string Title = "Сервис заметок";
            Assert.Equal(Title, _webDriver.Title);
            _webDriver.Close();
        }
        [Fact]
        public void ClickButton()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            const string Title = "Сервис заметок";
            IWebElement RegButton = _webDriver.FindElement(By.Id("registerTab"));
            RegButton.Click();
            Thread.Sleep(2000);
            IWebElement LoginButton = _webDriver.FindElement(By.Id("loginTab"));
            LoginButton.Click();
            Thread.Sleep(2000);
            Assert.Equal(Title, _webDriver.Title);
            _webDriver.Close();
        }
        [Fact]
        public void Auth()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            const string WText = "Здравствуйте, grizzly!";
            IWebElement RegButton = _webDriver.FindElement(By.Id("registerTab"));
            RegButton.Click();
            Thread.Sleep(2000);
            IWebElement LoginButton = _webDriver.FindElement(By.Id("loginTab"));
            LoginButton.Click();
            IWebElement NameInput = _webDriver.FindElement(By.Id("authUsername"));
            NameInput.SendKeys("grizzly");
            IWebElement PasswordInput = _webDriver.FindElement(By.Id("authPassword"));
            PasswordInput.SendKeys("Dragon");
            Thread.Sleep(2000);
            IWebElement AuthButton = _webDriver.FindElement(By.Id("authSubmit"));
            AuthButton.Click();
            Thread.Sleep(2000);
            IWebElement WelcomeText = _webDriver.FindElement(By.Id("welcomeText"));
            Assert.Equal(WText, WelcomeText.Text);
            _webDriver.Close();
        }
        [Fact]
        public void Exit()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            const string Title = "Сервис заметок";
            IWebElement RegButton = _webDriver.FindElement(By.Id("registerTab"));
            RegButton.Click();
            Thread.Sleep(2000);
            IWebElement LoginButton = _webDriver.FindElement(By.Id("loginTab"));
            LoginButton.Click();
            IWebElement NameInput = _webDriver.FindElement(By.Id("authUsername"));
            NameInput.SendKeys("grizzly");
            Thread.Sleep(2000);
            IWebElement PasswordInput = _webDriver.FindElement(By.Id("authPassword"));
            PasswordInput.SendKeys("Dragon");
            Thread.Sleep(2000);
            IWebElement AuthButton = _webDriver.FindElement(By.Id("authSubmit"));
            AuthButton.Click();
            Thread.Sleep(2000);
            IWebElement ExitButton = _webDriver.FindElement(By.Id("logoutBtn"));
            ExitButton.Click();
            Thread.Sleep(2000);
            Assert.Equal(Title, _webDriver.Title);
            _webDriver.Close();
        }
        [Fact]
        public void ErrorAuth()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzl");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            IWebElement message = _webDriver.FindElement(By.Id("message"));

            Assert.Equal("Неверный логин или пароль.\r\n×", message.Text);
            _webDriver.Close();
        }
        [Fact]
        public void SuccesExit()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            _webDriver.FindElement(By.Id("logoutBtn")).Click();
            Thread.Sleep(2000);
            IWebElement message = _webDriver.FindElement(By.Id("message"));

            Assert.Equal("Вы вышли из системы.\r\n×", message.Text);
            _webDriver.Close();
        }
        [Fact]
        public void ReRegistration()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("registerTab")).Click();
            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            IWebElement message = _webDriver.FindElement(By.Id("message"));

            Assert.Equal("Пользователь с таким логином уже существует.\r\n×", message.Text);
            _webDriver.Close();
        }
        [Fact]
        public void CreateNote()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            _webDriver.FindElement(By.Id("noteTitle")).SendKeys("Вторая заметка");
            _webDriver.FindElement(By.Id("noteContent")).SendKeys("Заполнение автотестом");
            _webDriver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(2000);
            IWebElement message = _webDriver.FindElement(By.Id("message"));

            Assert.Equal("Заметка создана.\r\n×", message.Text);
            _webDriver.Close();
        }
        [Fact]
        public void DeleteNote()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            _webDriver.FindElement(By.Id("notesList")).Click();
            _webDriver.FindElement(By.Id("deleteBtn")).Click();
            IAlert alert = _webDriver.SwitchTo().Alert();
            Thread.Sleep(2000);
            alert.Accept();
            Thread.Sleep(2000);
            IWebElement message = _webDriver.FindElement(By.Id("message"));

            Assert.Equal("Заметка удалена.\r\n×", message.Text);
            _webDriver.Close();
        }
        [Fact]
        public void ToChangeNote()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            IWebElement NoteTitle = _webDriver.FindElement(By.Id("noteTitle"));
            NoteTitle.SendKeys("Вторая заметка");
            IWebElement NoteContent = _webDriver.FindElement(By.Id("noteContent"));
            NoteContent.SendKeys("Заполнение автотестом");
            IWebElement SaveBtn = _webDriver.FindElement(By.Id("saveBtn"));
            SaveBtn.Click();
            Thread.Sleep(2000);
            NoteTitle.Clear();
            NoteContent.Clear();
            NoteTitle.SendKeys("Измененная заметка");
            NoteContent.SendKeys("Изменили контент заметки автотестом");
            SaveBtn.Click();
            _webDriver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(2000);
            IWebElement message = _webDriver.FindElement(By.Id("message"));

            Assert.Equal("Заметка обновлена.\r\n×", message.Text);
            _webDriver.Close();
        }
        [Fact]
        public void ToShareNote()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            _webDriver.FindElement(By.Id("notesList")).Click();
            _webDriver.FindElement(By.Id("shareUsername")).SendKeys("qwerty123");
            _webDriver.FindElement(By.Id("shareBtn")).Click();
            Thread.Sleep(2000);
            IWebElement message = _webDriver.FindElement(By.Id("message"));

            Assert.Equal("Доступ успешно выдан.\r\n×", message.Text);
            _webDriver.Close();
        }

        [Fact]
        public void RevokeAccessNote()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            _webDriver.FindElement(By.Id("notesList")).Click();
            _webDriver.FindElement(By.Id("shareUsername")).SendKeys("qwerty123");
            _webDriver.FindElement(By.Id("shareBtn")).Click();
            Thread.Sleep(2000);
            _webDriver.FindElement(By.XPath("//*[@id=\"sharedUsersList\"]/li/button")).Click();
            IAlert alert = _webDriver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
            IWebElement message = _webDriver.FindElement(By.Id("message"));

            Assert.Equal("Доступ пользователя отозван.\r\n×", message.Text);
            _webDriver.Close();
        }
        [Fact]
        public void SearchNote()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            IWebElement Search = _webDriver.FindElement(By.Id("searchInput"));
            Search.SendKeys("Вторая");
            Thread.Sleep(2000);
            Assert.True(Search.Displayed);
            _webDriver.Close();
        }

        [Fact]
        public void ErrorToShareNote()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            _webDriver.FindElement(By.Id("notesList")).Click();
            _webDriver.FindElement(By.Id("shareBtn")).Click();
            Thread.Sleep(2000);
            IWebElement message = _webDriver.FindElement(By.Id("message"));
            Assert.Equal("Укажите логин пользователя для совместного доступа.\r\n×", message.Text);
            _webDriver.Close();
        }
        [Fact]
        public void ErrorNoUserNote()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            _webDriver.FindElement(By.Id("notesList")).Click();
            _webDriver.FindElement(By.Id("shareUsername")).SendKeys("111111111111111");
            _webDriver.FindElement(By.Id("shareBtn")).Click();
            Thread.Sleep(2000);
            IWebElement message = _webDriver.FindElement(By.Id("message"));
            Assert.Equal("Пользователь не найден.\r\n×", message.Text);
            _webDriver.Close();
        }
        [Fact]
        public void ComboBoxDisplayed()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            IWebElement ComboBox = _webDriver.FindElement(By.Id("noteScopeFilter"));
            Thread.Sleep(2000);
            Assert.True(ComboBox.Displayed);
            _webDriver.Close();
        }
        [Fact]
        public void ComboBoxMyClick()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            IWebElement ComboBox = _webDriver.FindElement(By.Id("noteScopeFilter"));
            ComboBox.Click();
            IWebElement My = _webDriver.FindElement(By.XPath("//*[@id=\"noteScopeFilter\"]/option[2]"));
            My.Click();
            ComboBox.Click();
            Thread.Sleep(2000);
            Assert.True(My.Displayed);
            _webDriver.Close();
        }
        [Fact]
        public void ComboBoxAllClick()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            IWebElement ComboBox = _webDriver.FindElement(By.Id("noteScopeFilter"));
            ComboBox.Click();
            IWebElement All = _webDriver.FindElement(By.XPath("//*[@id=\"noteScopeFilter\"]/option[1]"));
            All.Click();
            ComboBox.Click();
            Thread.Sleep(2000);
            Assert.True(All.Displayed);
            _webDriver.Close();
        }
        [Fact]
        public void ComboBoxOurClick()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            IWebElement ComboBox = _webDriver.FindElement(By.Id("noteScopeFilter"));
            ComboBox.Click();
            IWebElement Our = _webDriver.FindElement(By.XPath("//*[@id=\"noteScopeFilter\"]/option[3]"));
            Our.Click(); 
            ComboBox.Click();
            Thread.Sleep(2000);
            Assert.True(Our.Displayed);
            _webDriver.Close();
        }
        [Fact]
        public void NewNote()
        {
            _webDriver.Url = "https://test.webmx.ru/";
            _webDriver.FindElement(By.Id("loginTab")).Click();

            _webDriver.FindElement(By.Id("authUsername")).SendKeys("grizzly");
            _webDriver.FindElement(By.Id("authPassword")).SendKeys("Dragon");
            _webDriver.FindElement(By.Id("authSubmit")).Click();
            Thread.Sleep(2000);
            _webDriver.FindElement(By.Id("notesList")).Click();
            Thread.Sleep(2000);
            IWebElement NewNoteBtn = _webDriver.FindElement(By.Id("newNoteBtn"));
            NewNoteBtn.Click();
            Thread.Sleep(2000);

            Assert.True(NewNoteBtn.Displayed);
            _webDriver.Close();
        }
    }
}
