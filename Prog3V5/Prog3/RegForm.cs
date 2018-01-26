// Program 3
// CIS 199-XX
// Due: 11/9/2017
// By: Andrew L. Wright (Students use Grading ID)

// This application calculates the earliest registration date
// and time for an undergraduate student given their class standing
// and last name.
// Decisions based on UofL Spring 2018 Priority Registration Schedule
// Now uses parallel arrays and range matching for time decision logic.

// Solution 5
// This solution uses lower range values and searches from back
// of the range array to the front.
// It uses one search method.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog3
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        // Precondition:  'A' <= lastNameLetter <= 'Z'
        //                lowLetters and times are non-empty, parallel arrays
        //                used for range matching
        //                lowLetters in ascending order
        // Postcondition: Registration time associated with specified last name letter
        //                is returned for specified pattern in parallel arrays
        private string FindRegTime(char lastNameLetter, char[] lowLetters, string[] times)
        {
            string timeStr = "Error"; // Holds time of registration
            int index;                // Subscript of array for range match
            bool found;               // Range match found?

            // Range match search
            found = false;
            index = lowLetters.Length - 1;   // Start from end
                                             // since lower limits
            while (index >= 0 && !found)
            {
                if (lastNameLetter >= lowLetters[index])
                    found = true;
                else
                    --index;
            }

            if (found)
                timeStr = times[index];

            return timeStr;
        }

        // Precondition:  Find registration time button clicked
        // Postcondition: If last name entered, earliest registration
        //                date/time output. Otherwise, error message displayed
        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const string DAY1 = "November 3";   // 1st day of registration
            const string DAY2 = "November 6";   // 2nd day of registration
            const string DAY3 = "November 7";   // 3rd day of registration
            const string DAY4 = "November 8";   // 4th day of registration
            const string DAY5 = "November 9";   // 5th day of registration
            const string DAY6 = "November 10";  // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            // Low end of letter range for juniors and seniors
            char[] juniorSeniorLowLetters = { 'A', 'E', 'J', 'P', 'T' };
            // Times associated with range for juniors and seniors
            string[] juniorSeniorTimes = { TIME2, TIME3, TIME4, TIME5, TIME1 };

            // Low end of letter range for freshmen and sophomores
            char[] freshSophLowLetters = { 'A', 'C', 'E', 'G', 'J',
                                           'M', 'P', 'R', 'T', 'W' };
            // Times associated with range for freshmen and sophomores
            string[] freshSophTimes = { TIME3, TIME4, TIME5, TIME1, TIME2,
                                        TIME3, TIME4, TIME5, TIME1, TIME2 };

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            bool isUpperClass;        // Upperclass or not?

            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name
                lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                {
                    isUpperClass = (seniorRBtn.Checked || juniorRBtn.Checked);

                    // Juniors and Seniors share same schedule but different days
                    if (isUpperClass)
                    {
                        if (seniorRBtn.Checked)
                            dateStr = DAY1;
                        else // Must be juniors
                            dateStr = DAY2;

                        timeStr = FindRegTime(lastNameLetterCh, juniorSeniorLowLetters, juniorSeniorTimes); // Call method
                    }
                    // Sophomores and Freshmen
                    else // Must be soph/fresh
                    {
                        if (sophomoreRBtn.Checked)
                        {
                            // G-S on one day
                            if ((lastNameLetterCh >= 'G') && // >= G and
                                (lastNameLetterCh <= 'S'))   // <= S
                                dateStr = DAY4;
                            else // All other letters on previous day
                                dateStr = DAY3;
                        }
                        else // must be freshman
                        {
                            // G-S on one day
                            if ((lastNameLetterCh >= 'G') && // >= G and
                                (lastNameLetterCh <= 'S'))   // <= S
                                dateStr = DAY6;
                            else // All other letters on previous day
                                dateStr = DAY5;
                        }

                        timeStr = FindRegTime(lastNameLetterCh, freshSophLowLetters, freshSophTimes); // Call method
                    }

                    // Output results
                    dateTimeLbl.Text = dateStr + " at " + timeStr;
                }
                else // First char not a letter
                    MessageBox.Show("Make sure last name starts with a letter");
            }
            else // Empty textbox
                MessageBox.Show("Enter a last name!");
        }
    }
}
