namespace Calculator_MAUI;

public partial class MainPage : ContentPage
{
	
	public MainPage()
	{
		InitializeComponent();
        OnClear(this, null);
    }
    int currentState = 1, calculateState = 0, operatorLimit=1;
    string operatorMath;
    double firstNum, secondNum;

    void OnClear(object sender, EventArgs e)
    {
        firstNum = 0;
        secondNum = 0;
        currentState = 1;
        calculateState = 1;
        this.result.Text = "0";
        this.result2.Text = "0";
    }

    void OnNumberSelection(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string btnPressed = button.Text;
        if (this.result.Text == "0" || currentState < 0)
        {
            this.result.Text = string.Empty;
            
            if (currentState < 0)
                currentState *= -1;
        }


        this.result.Text += btnPressed;
        if (calculateState == 1)
        {
            this.result2.Text = this.result.Text;
            calculateState = 0;
        }
        else
        {
            char lastCharacter = this.result.Text[this.result.Text.Length - 1];
            this.result2.Text += Char.ToString(lastCharacter);
        }
        
        double number;
        if (double.TryParse(this.result.Text, out number))
        {
            this.result.Text = number.ToString("N0");

            if (currentState == 1)
            {
                firstNum = number;
            }
            else
            {
                secondNum = number;
            }
        }
    }

    void onOperatorSelection(object sender, EventArgs e)
    {
        if(operatorLimit == 0) { }
        else { 
        currentState = -2;
        Button button = (Button)sender;
        string btnPressed = button.Text;
        operatorMath = btnPressed;
        if(btnPressed == "X^") { this.result2.Text += "^"; }
        else this.result2.Text += button.Text;
        operatorLimit-=1;
    }
}

    void onCalculate(object sender, EventArgs e)
    {
        if (currentState == 2)
        {
            var result = Calculate.DoCalculation(firstNum, secondNum, operatorMath);

            this.result.Text = result.ToString();
            this.result2.Text = result.ToString();
            firstNum = result;
            currentState = -1;
            calculateState = 1;
            operatorLimit = 1;
        }
    }
    void OnSquareRoot(object sender, EventArgs e)
    {
        if (firstNum == 0)
            return;
        firstNum = firstNum * firstNum;
        this.result.Text = firstNum.ToString();
        this.result2.Text = this.result.Text;
    }

    public static class Calculate
    {
        public static double DoCalculation(double val1, double val2, string operatorMath)
        {
            double result = 0;

            switch (operatorMath)
            {
                case "÷":
                    result = val1 / val2;
                    break;
                case "-":
                    result = val1 - val2;
                    break;
                case "×":
                    result = val1 * val2;
                    break;
                case "+":
                    result = val1 + val2;
                    break;
                case "X^":
                    result = Math.Pow(val1, val2);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
    
}

