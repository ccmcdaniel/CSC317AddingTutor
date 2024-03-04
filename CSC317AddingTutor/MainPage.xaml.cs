namespace CSC317AddingTutor
{
    public partial class MainPage : ContentPage
    {
        //Note: You can use the Character Map in Windows to determine the character
        //      codes needed to produce checkmark icons.
        //      Make sure to set the font family correctly on the front end as well.
        char icon_incorrect = 'ý';
        char icon_correct = 'þ';
        char icon_blank = '¨';

        //Green for Correct, Red for Incorrect, and Grey when no input is yet given.
        Color icon_correct_color = Color.Parse("#229922");
        Color icon_incorrect_color = Color.Parse("#992222");
        Color icon_blank_color = Color.Parse("#999999");

        //Store the original colors of the message text
        Color message_original_text_color;

        //Store the numbers previously generated.
        int num1, num2;
        
        //When the user makes a guess, set a flag to generate
        //a new number.
        bool needNewNumber = false;

        //Create a random number generator.
        Random r = new Random();

        public MainPage()
        {
            InitializeComponent();
            message_original_text_color = lblMessage.TextColor;

            ResetBoard();
        }

        private void ConfirmClicked(object sender, EventArgs e)
        {
            if(needNewNumber)
            {
                ResetBoard();
                return;
            }

            try
            {
                int user_answer = Convert.ToInt32(txtInput.Text);
                
                if(user_answer == num1 + num2)
                {
                    //Win
                    ProcessResult(true);
                }
                else
                {
                    //Lose
                    ProcessResult(false);
                }
            }
            catch(FormatException)
            {
                lblMessage.Text = "Error: Invalid response.  Enter a valid number in the text box below.";
                lblMessage.TextColor = Color.Parse("#BB4444");
            }
        }

        /* Utility Functions.
         * 
         * 
         * 
         */

        private void ResetBoard()
        {
            lblCheckbox.Text = icon_blank.ToString();
            lblCheckbox.TextColor = icon_blank_color;
            btnConfirm.Text = "Confirm";

            //Pick the solution to be from 0 to 100.
            int solution = r.Next(0, 100);

            //Pick a random number between 0 and the solution.
            num1 = r.Next(0, solution);

            //num2 will be the difference.
            num2 = solution - num1;

            lblNumber1.Text = num1.ToString();
            lblNumber2.Text = num2.ToString();

            txtInput.Text = String.Empty;

            lblMessage.Text = "Try to solve the math problem below and enter your answer.";
            lblMessage.TextColor = message_original_text_color;

            needNewNumber = false;
        }

        private void ProcessResult(bool result)
        {
            needNewNumber = true;

            if(result == true)
            {
                //win
                lblCheckbox.Text = icon_correct.ToString();
                lblCheckbox.TextColor = icon_correct_color;
            }
            else
            {
                //lose
                lblCheckbox.Text = icon_incorrect.ToString();
                lblCheckbox.TextColor = icon_incorrect_color;
            }

            btnConfirm.Text = "New Problem";
            needNewNumber = true;
        }
    }
}
