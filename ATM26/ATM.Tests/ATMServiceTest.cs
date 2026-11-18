using System.Net.NetworkInformation;

namespace ATM.ATM.Tests;
public class AtmServiceTest
{
    Card card;
    AtmService atm;

    void setupService()
    {
        Account account = new Account(5000);
        card = new Card("1234-5678", "1234", account);
        atm = new AtmService(5000);
    }
    [Fact]
    public void InsertCardTest()
    {
        //setup
        Account account = new Account(5000);
        Card card = new Card("1234-5678", "1234", account);
        AtmService atm = new AtmService(5000);

        //test
        atm.InsertCard(card);
        Assert.True(atm.HasCardInserted);
    }

    [Theory]
    [InlineData("1234", true)]
    [InlineData("0123", false)]
    //Kan fortsätta hur länge som helst 
    public void EnterPinTest(string pin, bool expected)
    {
        //setup
        setupService();
        atm.InsertCard(card);

        //test
        bool actual = atm.EnterPin(pin);
        Assert.Equal(expected, actual);

    }

    [Fact]
    public void EjectCardTest()
    {
        setupService();
        atm.InsertCard(card);

        atm.EnterPin("1234");
        atm.EjectCard();

        Assert.False(atm.HasCardInserted);
        Assert.False(atm.IsAuthenticated);

    }

    [Fact]
    public void GetBalanceTest()
    {
        setupService();
        atm.InsertCard(card);

        atm.EnterPin("1234");

        int actual = atm.GetBalance();
        Assert.Equal(5000, actual);
    }

    [Fact]
    public void GetBalanceWithoutPinTest()
    {
        setupService();
        atm.InsertCard(card);

        Assert.Throws<InvalidOperationException>(() => atm.GetBalance());

    }
    [Fact]
    public void WithdrawTest()
    {
        setupService();
        atm.InsertCard(card);

        atm.EnterPin("1234");

        atm.Withdraw(1000);

        int actual = atm.GetBalance();
        Assert.Equal(4000, actual);

        Assert.Equal(4000, atm.AtmBalance);
        
    }

    [Fact]
    public void WithdrawWithoutPinTest()
    {
        setupService();
        atm.InsertCard(card);

        Assert.Throws<InvalidOperationException>(() => atm.Withdraw(1000));
    }

    [Fact]
    public void DepositTest()
    {
        setupService();
        atm.InsertCard(card);

        atm.EnterPin("1234");

        atm.Deposit(1000);

        int actual = atm.GetBalance();
        Assert.Equal(6000, actual);

        Assert.Equal(5000, atm.AtmBalance);
    }
     [Fact]
    public void DepositWithoutPinTest()
    {
        setupService();
        atm.InsertCard(card);

        Assert.Throws<InvalidOperationException>(() => atm.Deposit(1000));
    }
}