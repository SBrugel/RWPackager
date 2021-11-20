using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace TS_ReadMe_Maker
{
    public partial class Form1 : Form
    {
        string scenario, sourcePic, packageTo;
        List<int> slashes = new List<int>();
        List<string> folderNames = new List<string>();
        public Form1()
        {
            InitializeComponent();
            reqList.ColumnCount = 5; //fill out column information on window initialize
            reqList.Columns[0].Name = "Provider";
            reqList.Columns[0].ToolTipText = "Who is this product distributed by? (Armstrong Powerhouse, Steam, Just Trains, Major Wales Design...)";
            reqList.Columns[1].Name = "Product";
            reqList.Columns[1].ToolTipText = "What is the name of this product?";
            reqList.Columns[2].Name = "Freeware?";
            reqList.Columns[2].ToolTipText = "If this product is free and can be downloaded without paying, enter Y. If you need to pay for it to legitimately obtain it enter N.";
            reqList.Columns[3].Name = "Required?";
            reqList.Columns[3].ToolTipText = "If the player does not have this item installed, and as such the scenario will NOT still run as intended, enter Y. Else, enter N.";
            reqList.Columns[4].Name = "Notes";
            reqList.Columns[4].ToolTipText = "Optional field that can be used to list notes about each requirement, such as how they are used or if any extensions to them are also required";
        }
        /// <summary>
        /// When a scenario is uploaded to the utility, update filled in forms accordingly
        /// </summary>
        private void UpdateDetails()
        {
            scenario = textBox1.Text;
            XmlDocument properties = new XmlDocument();
            properties.Load(scenario + "\\ScenarioProperties.xml");

            XmlNodeList elemList = properties.GetElementsByTagName("English"); // get all english tags
            nameField.Text = elemList[0].InnerXml; //name
            descField.Text = elemList[1].InnerXml; //desc
            briefField.Text = elemList[2].InnerXml; //brief

            elemList = properties.GetElementsByTagName("DurationMins"); // get duration
            numericUpDown1.Value = Int32.Parse(elemList[0].InnerXml);

            elemList = properties.GetElementsByTagName("Rating"); // get difficulty
            switch (Int32.Parse(elemList[0].InnerXml))
            {
                case 0:
                    comboBox1.SelectedIndex = 1;
                    break;
                case 1:
                    comboBox1.SelectedIndex = 2;
                    break;
                case 2:
                    comboBox1.SelectedIndex = 3;
                    break;
                case 3:
                    comboBox1.SelectedIndex = 4;
                    break;
                case 4:
                    comboBox1.SelectedIndex = 5;
                    break;
                default:
                    comboBox1.SelectedIndex = 0;
                    break;
            }

            for (int i = 0; i < scenario.Length; i++)
            {
                if (scenario[i] == '\\')
                {
                    slashes.Add(i);
                }
            }
            folderNames.Add(scenario.Substring(slashes[slashes.Count - 3] + 1, (slashes[slashes.Count - 2] - (slashes[slashes.Count - 3] + 1))));
            folderNames.Add(scenario.Substring(slashes[slashes.Count - 2] + 1, (slashes[slashes.Count - 1] - (slashes[slashes.Count - 2] + 1))));
            folderNames.Add(scenario.Substring(slashes[slashes.Count - 1] + 1, ((scenario.Length) - (slashes[slashes.Count - 1] + 1))));
        }

        /// <summary>
        /// Remove all form details if an invalid scenario is inputted
        /// </summary>
        private void EmptyDetails()
        {
            nameField.Text = "";
            descField.Text = "";
            briefField.Text = "";
            numericUpDown1.Value = 0;
            comboBox1.SelectedIndex = 0;

            folderNames.Clear();
            slashes.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.BackColor = System.Drawing.Color.LightGreen;
                UpdateDetails();
            } catch (Exception ex) //invalid scenario
            {
                textBox1.BackColor = System.Drawing.Color.White;
                EmptyDetails();
            }
        }

        private void button2_Click(object sender, EventArgs e) //select scenario
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = @folderBrowserDialog1.SelectedPath;
                try
                {
                    textBox1.BackColor = System.Drawing.Color.LightGreen;
                    UpdateDetails();
                }
                catch (Exception ex) //invalid scenario
                {
                    MessageBox.Show("This folder does not contain a scenario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.BackColor = System.Drawing.Color.White;
                    EmptyDetails();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //paste into requirement list (easy copy and paste)
        {
            if (!textBox4.Text.Equals(""))
            {
                if (ECPItems.Lines.Length > 0)
                {
                    for (int i = 0; i < ECPItems.Lines.Length; i++)
                    {
                        string freeware = "";
                        if (textBox4.Text.Equals("Steam") || textBox4.Text.Equals("AP") || textBox4.Text.Equals("JT") || textBox4.Text.Equals("Armstrong Powerhouse")
                            || textBox4.Text.Equals("Just Trains") || textBox4.Text.Equals("JustTrains") || textBox4.Text.Equals("Dovetail Games"))
                        {
                            freeware = "N";
                        }
                        else if (textBox4.Text.Equals("MJW") || textBox4.Text.Equals("Major Wales") || textBox4.Text.Equals("DPS") || textBox4.Text.Equals("DPSimulation")
                            || textBox4.Text.Equals("UKTS") || textBox4.Text.Equals("UKTrainSim") || textBox4.Text.Equals("UK Train Sim"))
                        {
                            freeware = "Y";
                        }

                        if (ECPItems.Lines[i].Last().Equals('*')) //asterisk indicates freeware
                        {
                            Object[] row = new object[] { textBox4.Text, ECPItems.Lines[i].Remove(ECPItems.Lines[i].Length - 1, 1), freeware, "N" }; //remove asterisk from item
                            reqList.Rows.Add(row);
                        } else
                        {
                            Object[] row = new object[] { textBox4.Text, ECPItems.Lines[i], freeware, "Y" };
                            reqList.Rows.Add(row);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You did not input any requirements!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("You did not input a provider!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e) //generate readme
        {
            if (scenarioPhoto.ImageLocation == null || nameField.Text.Equals("") || descField.Text.Equals("") || reqList.RowCount <= 1)
            {
                MessageBox.Show("One or more fields are empty, please check these before packaging.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                MessageBox.Show("Please select a folder to save the readme (and scenario, if applicable).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK)
                {
                    packageTo = @folderBrowserDialog1.SelectedPath;
                    WriteReadme();
                    if (checkBox1.Checked)
                    {
                        PackScenario();
                    }
                    MessageBox.Show("Packaged successfully/readme generated!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Writes a new HTML file based on the scenario details the user has inputted
        /// </summary>
        private void WriteReadme()
        {
            List<String> devs = new List<String>();
            List<String> products = new List<String>();
            //get a list of developers - non replacement
            for (int i = 0; i < reqList.RowCount - 1; i++)
            {
                if (!devs.Contains(reqList.Rows[i].Cells[0].Value.ToString()))
                {
                    devs.Add(reqList.Rows[i].Cells[0].Value.ToString());
                }
            }

            File.Copy(sourcePic, Path.Combine(packageTo, "Preview" + Path.GetExtension(sourcePic)));

            //list all requirements based on developers
            products.Add("---END OF DEV---");
            for (int i = 0; i < devs.Count; i++) //one developer at a time
            {
                for (int j = 0; j < reqList.RowCount - 1; j++) //loop through the full list
                {
                    if (reqList.Rows[j].Cells[0].Value.ToString().Equals(devs[i])) { //check if dev of this row matches
                        string openBrackets, closeBrackets;

                        if (reqList.Rows[j].Cells[2].Value.ToString().Equals("Y") || reqList.Rows[j].Cells[2].Value.ToString().Equals("y")) 
                        { //free?
                            openBrackets = "<li>"; 
                            closeBrackets = "</li>"; //italics
                        } else
                        {
                            openBrackets = "<li><b>";
                            closeBrackets = "</b></li>"; //no italics
                        }

                        if (reqList.Rows[j].Cells[3].Value.ToString().Equals("N") || reqList.Rows[j].Cells[3].Value.ToString().Equals("n"))
                        { //optional?
                            if (openBrackets.Equals("<li>")) //pack is free
                            {
                                openBrackets = "<li class=\"optional\">";
                            } else
                            {
                                openBrackets = "<li class=\"optional\"><b>";
                            }
                        }

                        if (reqList.Rows[j].Cells[4].Value != null)
                        { //any notes?
                            products.Add(openBrackets + reqList.Rows[j].Cells[1].Value.ToString() + " <i>(" + reqList.Rows[j].Cells[4].Value.ToString() + ")</i>" + closeBrackets); //add the name of the item
                        }
                        else
                        {
                            products.Add(openBrackets + reqList.Rows[j].Cells[1].Value.ToString() + closeBrackets); //add the name of the item
                        }
                    }
                }
                //that'll be it for this dev, add a sentinel element to let the program know to move on to the next dev
                products.Add("---END OF DEV---");
            }

            string[] initlines =
            {
                "<!--This template was created by Simon Brugel (https://www.cynxs-stuff.com/). The readme and its contents were generated using the TS Readme Generator, made by the same author. -->",
                "<!DOCTYPE html>",
                "<html>",
                "<head>",
                "<title>README</title>",
                "<link rel=\"preconnect\" href=\"https://fonts.googleapis.com\">",
                "<link rel=\"preconnect\" href=\"https://fonts.gstatic.com\" crossorigin>",
                "<link href=\"https://fonts.googleapis.com/css?family=Encode Sans Condensed\" rel=\"stylesheet\">",
                "<style>",
                "div {",
                "padding: 10px;",
                "font-family: 'Encode Sans Condensed';",
                "font-size: 14pt;",
                "}",
                ".title {",
                "text-align: center;",
                "font-family: 'Encode Sans Condensed';",
                "font-size: 22pt;",
                "}",
                ".req {",
                "text-align: left;",
                "}",
                ".optional {",
                "color: red;",
                "}",
                "</style>",
                "<script type=\"text/javascript\">",
                "var optionalShown = true;",
                "function toggleOptional() {",
                "if (optionalShown) {",
                "optionalShown = false;",
                "document.querySelector(\"#toggleopt\").innerHTML = \"Show All Products Used\";",
                "[].forEach.call(document.getElementsByClassName(\"optional\"), function (el) {",
                "el.style.visibility = 'hidden';",
                "});",
                "} else {",
                "optionalShown = true;",
                "document.querySelector(\"#toggleopt\").innerHTML = \"Show Only Required\";",
                "[].forEach.call(document.getElementsByClassName(\"optional\"), function (el) {",
                "el.style.visibility = 'visible';",
                "});",
                "}",
                "}",
                "</script>",
                "</head>",
                "<body>",
                "<div class=\"title\">",
                "<img src=Preview" + Path.GetExtension(sourcePic) + " width=\"50% \" height=\"50% \">",
                "<p>" + nameField.Text + "</p>",
                "</div>",
                "<div class=\"req\">",
                "<p><b>Description: </b>" + descField.Text + "</p>",
                "<p><b>Briefing: </b>" + briefField.Text + "</p>",
                "<p><b>Length: </b>" + numericUpDown1.Value + " minutes</p>",
                "<p><b>Difficulty: </b>" + comboBox1.SelectedItem + "</p>",
                "<p><b>Conditions: </b>" + textBox5.Text + "</p>",
                "<hr>",
                "<p><b><u>Requirements</u></b></p>",
                "<p>Items in red are optional, meaning that the scenario <i>should</i> still function regardless if you have the item installed or not. However, you withdraw support for the scenario in the case an error occurs.</p>",
                "<button type=\"button\" id=\"toggleopt\" onclick=\"toggleOptional(); \">Show Only Required</button>"
            };

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(packageTo, "README.html")))
            {
                foreach (string line in initlines)
                    outputFile.WriteLine(line);

                //now for the fun part - devs
                int devIteration = 0;
                for (int i = 0; i < products.Count - 1; i++)
                {
                    if (products[i].Equals("---END OF DEV---")) //start iterating through a new dev
                    {
                        if (devIteration != 0) //end current list if not first dev
                        {
                            outputFile.WriteLine("</ul>");
                        }
                        if (devIteration != devs.Count) // write a new list if not last dev
                        {
                            string toPrint = devs[devIteration];
                            toPrint = ExtendString(toPrint, "AP", "Armstrong Powerhouse");
                            toPrint = ExtendString(toPrint, "JT", "Just Trains");
                            toPrint = ExtendString(toPrint, "ATS", "Alan Thomson Simulation");
                            toPrint = ExtendString(toPrint, "MJW", "Major Wales Design");
                            toPrint = ExtendString(toPrint, "DPS", "DPSimulation");
                            toPrint = ExtendString(toPrint, "OTS", "On Track Simulation");
                            toPrint = ExtendString(toPrint, "VP", "Vulcan Productions");
                            toPrint = ExtendString(toPrint, "UKTS", "UK Train Sim");
                            outputFile.WriteLine("<p><b>" + toPrint + "</b></p>");
                            outputFile.WriteLine("<ul>");
                            devIteration++;
                        }
                    } else
                    {
                        string toPrint = products[i];
                        ExtendString(toPrint, "EP", "Enhancement Pack");
                        ExtendString(toPrint, "SP", "Sound Pack");
                        ExtendString(toPrint, "(C)", "(Cummins)");
                        ExtendString(toPrint, "(P)", "(Perkins)");
                        outputFile.WriteLine(toPrint);
                    }
                }
                
                //notes and end tag
                if (textBox6.Lines.Length > 0)
                {
                    outputFile.WriteLine("<hr>");
                    outputFile.WriteLine("</ul>");
                    outputFile.WriteLine("<p><b><u>Notes</u></b></p>");
                    outputFile.WriteLine("<ul>");
                    for (int i = 0; i < textBox6.Lines.Length; i++)
                    {
                        outputFile.WriteLine("<li>" + textBox6.Lines[i] + "</li>");
                    }
                    outputFile.WriteLine("</ul>");
                }
                if (textBox7.Lines.Length > 0)
                {
                    outputFile.WriteLine("<br>");
                    outputFile.WriteLine(textBox8.Text);
                }
            }
        }

        /// <summary>
        /// If the tickbox is enabled, copies the scenario folder to the location of the readme
        /// </summary>
        private void PackScenario()
        {
            string pathString = Path.Combine(packageTo, "Content");
            pathString = Path.Combine(pathString, "Routes");
            for (int i = 0; i < 3; i++)
            {
                pathString = Path.Combine(pathString, folderNames[i]);
            }
            DirectoryCopy(scenario, pathString, true);
            folderNames.Clear();
        }

        /// <summary>
        /// Replaces an inputted string with a longer version of itself
        /// </summary>
        /// <param name="toReplace">The string to find instances of "o"</param>
        /// <param name="o">The substring that will be replaced</param>
        /// <param name="n">What to replace the substring with</param>
        /// <returns></returns>
        private string ExtendString(string toReplace, string o, string n) {
            if (toReplace.Contains(o))
            {
                return toReplace.Replace(o, n);
            }
            else return toReplace;
        }
        /// <summary>
        /// Copy a directory from one place to another
        /// </summary>
        /// <param name="sourceDirName">To copy from</param>
        /// <param name="destDirName">To copy to</param>
        /// <param name="copySubDirs">Copy all subdirectories as well</param>
        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void uploadPhoto_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (JPG/PNG/GIF)|*.JPG;*.PNG;*.GIF;";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK)
            {
                // set picture, directory info, and picture info
                sourcePic = @openFileDialog1.FileName;
                scenarioPhoto.ImageLocation = sourcePic;
            }
        }
    }
}