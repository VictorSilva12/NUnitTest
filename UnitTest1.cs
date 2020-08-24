using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
//https://chromedriver.chromium.org/ documentação do chromedriver
namespace NUnitTest
{
    public class Tests
    {
        public IWebDriver googleWebdriver;

        public String urlBase;
    
        //[OneTimeSetUp]
        //metodo abaixo realizará a configuração inicial do teste
        //o termo OneTimeSetUp identifica o metodo que será chamado imediatamente uma unica vez
        //indempendente da quantidade de testes.
        [SetUp]//metodo abaixo realizará a configuração inicial do teste
        //o termo SetUp identifica o metodo que será chamado imediatamente ao
        //inicio de cada um dos testes.
        public void abrirChrome()
        {
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");//desabilita o modo sandbox do chrome
            googleWebdriver = new ChromeDriver(options);//cria objeto utilzando a classe ChromeDriver
            googleWebdriver.Manage().Window.Maximize();//faz com que o navegador fique em tela cheia
            urlBase = "https://www.w3schools.com/";

        }
        //[OneTimeTearDown]
        //metodo abaixo lida com a finalização do teste
        //o termo OneTimeTearDown identifica o metodo que será
        //chamado imediatamente ao final de todos os testes.
        [TearDown]//metodo abaixo lida com a finalização do teste
        //o termo TearDown identifica o metodo que será chamado imediatamente
        //ao final de cada teste.
        public void fecharChrome(){
            try{
                Console.WriteLine("Terminando teste.....");
                googleWebdriver.Quit();
            }
            catch(Exception e){
                Console.WriteLine(e.ToString());
            }
        }
        [Test]
        public void Test1()
        {
            try{
                googleWebdriver.Navigate().GoToUrl(urlBase);
                IWebElement menuLateral = googleWebdriver.FindElement(By.ClassName("w3-bar-block"));
                menuLateral.FindElement(By.LinkText("Learn HTML")).Click();
                var titulo = googleWebdriver.FindElement(By.CssSelector("div#main > h1"));
                //Console.WriteLine(titulo.Text);
                //Verifica se o H1 da pagina é o esperado 
                StringAssert.AreEqualIgnoringCase("HTML Tutorial", titulo.Text);
                System.Threading.Thread.Sleep(600);
            }
            catch(Exception e){
                Console.WriteLine(e.ToString());
            }
            /*
            IWebElement menuLateral = googleWebdriver.FindElement(By.ClassName("w3-bar-block"));
            menuLateral.FindElement(By.LinkText("Learn HTML")).Click();
            Assert.Pass();
            */
        }
        [Test]
        public void Test2(){
            String textoPesquisa = "Css Selector vs Xpath";
            try{
                //abre o navegador, e o redireciona para pagina da url base
                googleWebdriver.Navigate().GoToUrl(urlBase);
                //abaixo uso Css Selector para localizar e transformar o navbar do site em 1 objeto.
                IWebElement navbar = googleWebdriver.FindElement(By.CssSelector("div[class=\"w3-bar w3-theme w3-card-2 w3-wide notranslate\"]"));
                //como a barra de pesquisa somente é carregada após click em um elemento na pagina
                //abaixo uso  Css Selector para localizar e realizar a ação de clicar em um link na pagina.
                navbar.FindElement(By.CssSelector("a[title=\"Search W3Schools\"]")).Click();
                //após o novo elemento ser carregado na pagina, uso Css Selector para 
                //encontrar o elemento googleSearch e o transformo em um objeto.
                IWebElement googleSearch = googleWebdriver.FindElement(By.CssSelector("div#googleSearch"));
                //com a barra de pesquisa transformada em objeto, eu a utilizo para localizar
                //o campo onde é inserido o termo de pesquisa, usando Css Selector, e transformo esse
                //elemento em 1 objeto. O mesmo é feito para o botão que dispara a pesquisa.
                IWebElement input = googleSearch.FindElement(By.CssSelector("input[type=\"text\""));                                         
                IWebElement searchBotao = googleSearch.FindElement(By.CssSelector("button[class=\"gsc-search-button gsc-search-button-v2\"]"));
                input.SendKeys(textoPesquisa);
                searchBotao.Click();               
                System.Threading.Thread.Sleep(6000);
                Boolean existe = verificaExiste(googleWebdriver);
                Assert.IsTrue(existe, "Teste");
                /*
                if(existe){
                    Assert.Pass();
                }else{
                    Assert.Fail();
                }
                */
            }catch(Exception e){
                Console.WriteLine("Exceção");
                Console.WriteLine(e.ToString());
            }
        }
        //em c#, caso aconteça algum erro durante a execução do codigo, a execução é 
        //interrompida e com isso um erro de exceção e uma mensagem de erro.
        //o codigo abaixo verifica a existencia de um elemento na pagina,
        //caso o elemento seja encontrado o metodo retorna true, quando não encontrado 
        //o metodo retorna false.
        public bool verificaExiste(IWebDriver el){
            try{
                googleWebdriver.FindElement(By.CssSelector("div[class=\"gsc-results-wrapper-overlay gsc-results-wrapper-visible\"]"));
                return true;
            }catch(NoSuchElementException){
                return false;
            }
        }
    }

}