// Program 3
// CIS 199-01
// Due: 11/09/2017
// By: A9371

// This application calculates the earliest registration date
// and time for an undergraduate student given their class standing
// and last name.
// Decisions based on UofL Spring 2018 Priority Registration Schedule

// Solution 3
// This solution keeps the first letter of the last name as a char
// and uses if/else logic for the times.
// It uses defined strings for the dates and times to make it easier
// to maintain.
// It only uses programming elements introduced in the text or
// in class.
// This solution takes advantage of the fact that there really are
// only two different time patterns used. One for juniors and seniors
// and one for sophomores and freshmen. The pattern for sophomores
// and freshmen is complicated by the fact the certain letter ranges
// get one date and other letter ranges get another date.

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

        // Find and display earliest registration time
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

            string[] TimesA = { TIME2, TIME3, TIME4, TIME5, TIME1 }; // String output for time pattern A
            string[] TimesB = { TIME3, TIME4, TIME5, TIME1, TIME2 }; // String output for time pattern B


            // I viewed the underclassman range match as a 5 to 10
            // 5 time outputs to be assigned to 10 ranges
            // I attempted to use nested loops to cycle thru the arrays of different length
            // That failed, and caused much pain
            // So I used 3 limit arrays, and 2 Time arrays to line up the count indexs

            char[] CharUpperLimitA = { 'D', 'I', 'O', 'S', 'Z' };  // Upperlimit test array for upperclassmen, range A
            char[] CharUpperLimitB = { 'B', 'D', 'F', 'I', 'L' };  // First half of underclassmen A - L, range B
            char[] CharUpperLimitC = { 'O', 'Q', 'S', 'V', 'Z' };  // Second half of underclassmen M-Z, range C
            

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            bool isUpperClass;        // Upperclass or not?
            int i = 0;                // Count Index
            bool match = false;       // End loop variable

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
                        
                        /*
                        if (lastNameLetterCh <= 'D')      // A-D
                            timeStr = TIME2;
                        else if (lastNameLetterCh <= 'I') // E-I
                            timeStr = TIME3;
                        else if (lastNameLetterCh <= 'O') // J-O
                            timeStr = TIME4;
                        else if (lastNameLetterCh <= 'S') // P-S
                            timeStr = TIME5;
                        else                              // T-Z
                            timeStr = TIME1;
                        */

                        // Replace time logic

                        i = 0;                  // count variable
                        match = false;          // end condition for loop

                        while (i <= TimesA.Length - 1 && !match)
                        {
                            if (lastNameLetterCh <= CharUpperLimitA[i])
                                match = true;
                            else
                                ++i;
                        }

                        timeStr = TimesA[i];
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

                        /*
                        if (lastNameLetterCh <= 'B')      // A-B
                            timeStr = TIME3;
                        else if (lastNameLetterCh <= 'D') // C-D
                            timeStr = TIME4;
                        else if (lastNameLetterCh <= 'F') // E-F
                            timeStr = TIME5;
                        else if (lastNameLetterCh <= 'I') // G-I
                            timeStr = TIME1;
                        else if (lastNameLetterCh <= 'L') // J-L
                            timeStr = TIME2;
                        else if (lastNameLetterCh <= 'O') // M-O
                            timeStr = TIME3;
                        else if (lastNameLetterCh <= 'Q') // P-Q
                            timeStr = TIME4;
                        else if (lastNameLetterCh <= 'S') // R-S
                            timeStr = TIME5;
                        else if (lastNameLetterCh <= 'V') // T-V
                            timeStr = TIME1;
                        else                              // W-Z
                            timeStr = TIME2;
                        */

                        // Replace time logic

                        i = 0;    // Count variable, reset to zero              
                        while (i <= TimesB.Length - 1 && !match) // Is match in range B
                        {
                            if (lastNameLetterCh <= CharUpperLimitB[i])
                                match = true;
                            else
                                ++i;
                        }

                        if (!match) // Not in range B, test range C
                        {
                            i = 0;  // Count variable, reset to zero
                            while (i <= TimesB.Length - 1 && !match)
                            {
                                if (lastNameLetterCh <= CharUpperLimitC[i])
                                    match = true;
                                else
                                    ++i;
                            }
                        }

                        timeStr = TimesB[i];
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
