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

    }
}