using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Media;

void Click(IWebDriver driver)
{
    var button = driver.FindElement(By.Id("ctl00_mainContent_btSave"));
    button.Click();

    try
    {
        Thread.Sleep(1000);
        // Check the presence of alert
        IAlert alert = driver.SwitchTo().Alert();
        if (alert.Text.Contains("accepted"))
        {
            var player = new SoundPlayer(@"D:\baodong.wav");
            player.Play();
        }
        // Alert present; set the flag
        //presentFlag = true;
        // if present consume the alert
        alert.Accept();
        driver.SwitchTo().DefaultContent();
    }
    catch (NoAlertPresentException ex)
    {
        // Alert not present
        Console.WriteLine(ex.StackTrace);
    }
}
var options = new ChromeOptions();
options.AddArgument("--disable-popup-blocking");
IWebDriver driver = new ChromeDriver(options);

driver.Url = "https://fap.fpt.edu.vn";
var cookie = new Cookie("ASP.NET_SessionId", args[0]);
driver.Manage().Cookies.AddCookie(cookie);
driver.Navigate().GoToUrl($"https://fap.fpt.edu.vn/FrontOffice/MoveSubject.aspx?id={args[1]}");

var selectElement = new SelectElement(driver.FindElement(By.Id("ctl00_mainContent_dllCourse")));
selectElement.SelectByValue(args[2]);

while (true)
{
    Click(driver);
    Thread.Sleep(5000);
}