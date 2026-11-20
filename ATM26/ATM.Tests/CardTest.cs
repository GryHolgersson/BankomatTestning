namespace ATM.Tests;

using Xunit;

public class CardTest
{

    [Fact]
    public void CreateCardTest()
    {
        // setup
        Account account = new Account(5000);
        Card card = new Card("1234-5678", "1234", account);

        // test
        Assert.Equal("1234-5678", card.CardNumber);
        Assert.Equal("1234", card.PinCode);
        Assert.Same(account, card.Account);
    }


    [Theory]
    [InlineData("1234", true)]
    [InlineData("0123", false)]
    [InlineData("123", false)]       // en för kort PIN
    [InlineData("789456", false)]       // en för lång PIN
    [InlineData("", false)]         // tom PIN
    public void MatchesPinTest(string pin, bool expected)
    {
        // setup
        Account account = new Account(5000);
        Card card = new Card("1234-5678", "1234", account);

        // test
        bool actual = card.MatchesPin(pin);
        Assert.Equal(expected, actual);
    }

}

