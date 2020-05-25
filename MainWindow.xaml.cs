using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {

        
        string location = "";
        ObservableCollection<hold> eachrow = new ObservableCollection<hold>();
        ObservableCollection<hold> hide = new ObservableCollection<hold>();
        ObservableCollection<hold> extrarow = new ObservableCollection<hold>();
        int fcount = 0;
        int exactlocation;
        

        Boolean open = true;
        Boolean fopen = false;
        Boolean point = false;

        public MainWindow()
        {
            InitializeComponent();
        }
        public class hold
        {
            public int lineNum { get; set; }
            public string data { get; set; }
            public string suffix { get; set; }
        }

        private void virtualtext_up(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                var commandline = virtualtext.Text;
                string[] lotcommand = commandline.Split(' ');

                if (lotcommand[0].ToLower() == "open")
                {
                    fcount++;
                    eachrow.Clear();

                    exactlocation = 1;

                    int c = 0;
                    point = true;
                    location = lotcommand[1];
                    StreamReader folder = new StreamReader(lotcommand[1]);
                    string currentlinenumber;


                    cls1.Content = "File Path in the local computer: " + location;
                    cls2.Content = "Current Line Number: " + 1;
                    eachrow.Add(new hold { lineNum = 0, data = "-------------Starting point of table-------------", suffix = "=====" });
                    while ((currentlinenumber = folder.ReadLine()) != null)
                    {
                        c++;
                        eachrow.Add(new hold { lineNum = c, data = currentlinenumber, suffix = "=====" });

                    }
                    eachrow.Add(new hold { data = "------------Ending point of table------------", suffix = "=====" });
                    gridinfo.ItemsSource = eachrow;

                    cls3.Content = "SizeInformation: " + c;

                    folder.Close();

                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(updatedRow));
                }
                else if (lotcommand.Count() > 1 && lotcommand[0].ToLower() == "save" && lotcommand[1].ToLower() == "as")
                {
                    StreamWriter file = new StreamWriter(lotcommand[2]);

                    for (int i = 0; i < eachrow.Count(); i++) file.WriteLine(eachrow[i].data);

                    file.Close();

                    MessageBox.Show("Updated Named File Saved " + lotcommand[2]);
                }
                else if (lotcommand[0].ToLower() == "help")
                {

                    MessageBox.Show("Save: Overwrites the original folder.\nSave As: Saves the folder to the location to the referanced direction at command line.\n" +
                                   "Open: Opens a specified folder with the path information.\nSearch: String search option is provided to the user.\n" +
                                   "#: Allow user to skip to line # number.\nUp #: Allow user to scroll up # lines.\nDown #: Allow user to scroll down # lines.\n" +
                                   "Left #: Allow user to scroll left # lines.\nRight #: Allow user to scroll right # lines.\nForward: Allow user to scrolls one screenfull towards the end of folder.\n" +
                                   "Setcl #: Allow user to select which line number is the current line.\nChange: Allow user to finds & modifies a searched string, starting with the defined currentline.\n" +
                                   "Back: Allow user to scrolls one screenfull towards the top of folder.\n");
                }
                else if (lotcommand[0].ToLower() == "save")
                {
                    var command = virtualtext.Text;
                    string[] commands = command.Split(' ');
                    StreamWriter file = new StreamWriter(commands[0]);

                    if (commands[0] == "")
                    {
                        MessageBox.Show("Open a file first to use SAVE option.");

                    }
                    else


                        for (int i = 0; i < eachrow.Count(); i++) file.WriteLine(eachrow[i].data);

                    file.Close();

                    cleanLabels();

                    MessageBox.Show("File saved in new name " + commands[0]);
                }

                else if (lotcommand[0].ToLower() == "search" || lotcommand[0].ToLower() == "find")
                {
                    if (point)
                    {
                        if (lotcommand.Count() == 3)
                        {
                            string textfind = lotcommand[1].Split('/')[1];
                            int linelocation, i, founded = 0, foundedlocation = 0;

                            if (lotcommand[1].Split('/')[2] == "*") linelocation = 0;
                            else linelocation = Int32.Parse(lotcommand[1].Split('/')[2]) - 1;

                            int colNum = Int32.Parse(lotcommand[2]);
                            Boolean pos = false, catchline = true;

                            for (i = linelocation; i < eachrow.Count(); i++)
                            {
                                if (catchline && eachrow[i].data.Substring(colNum).Contains(textfind))
                                {
                                    foundedlocation = i;
                                    founded++;
                                    pos = true;
                                }
                                else if (!catchline && eachrow[i].data.Contains(textfind))
                                {
                                    foundedlocation = i;
                                    founded++;
                                    pos = true;
                                }
                                catchline = false;
                            }

                            if (pos)
                            {
                                MessageBox.Show(textfind + " catched " + founded + " quantity.");
                                scrollViewer.ScrollToBottom();
                                exactlocation = foundedlocation;
                                gridinfo.SelectedItem = gridinfo.Items[exactlocation];
                                gridinfo.ScrollIntoView(gridinfo.Items[exactlocation]);
                            }
                            else MessageBox.Show(textfind + " didn't find by the text editor.");
                        }
                        else MessageBox.Show("The format is wrong. Please enter in proper format.");
                    }
                    else MessageBox.Show("File isn't opened for search.");
                }
                else if (lotcommand[0].Contains("1") || lotcommand[0].Contains("2") || lotcommand[0].Contains("3") || lotcommand[0].Contains("4") || lotcommand[0].Contains("5")
                        || lotcommand[0].Contains("6") || lotcommand[0].Contains("7") || lotcommand[0].Contains("8") || lotcommand[0].Contains("9"))
                {

                    scrollViewer.ScrollToBottom();
                    int a = Int32.Parse(lotcommand[0]);
                    exactlocation = a;
                    gridinfo.SelectedItem = gridinfo.Items[a - 1];
                    gridinfo.ScrollIntoView(gridinfo.Items[a - 1]);


                }
                else if (lotcommand[0].ToLower() == "up")
                {
                    scrollViewer.ScrollToBottom();
                    int countline = exactlocation - Int32.Parse(lotcommand[1]);

                    if (countline < 1)
                    {

                        exactlocation = 0;

                    }
                    else
                    {
                        exactlocation = countline;


                    }
                    gridinfo.SelectedItem = gridinfo.Items[exactlocation];
                    gridinfo.ScrollIntoView(gridinfo.Items[exactlocation]);
                }
                else if (lotcommand[0].ToLower() == "down" && lotcommand.Count() == 2)
                {
                    scrollViewer.ScrollToBottom();
                    int countline = exactlocation + Int32.Parse(lotcommand[1]);

                    if (countline > eachrow.Count() - 1)
                    {
                        exactlocation = eachrow.Count() - 1;
                    }
                    else
                    {
                        exactlocation = countline;
                    }
                    gridinfo.SelectedItem = gridinfo.Items[exactlocation];
                    gridinfo.ScrollIntoView(gridinfo.Items[exactlocation]);
                }
                else if (lotcommand[0].ToLower() == "right")
                {
                    int amount = Int32.Parse(lotcommand[1]);
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + (amount * 7));
                }
                else if (lotcommand[0].ToLower() == "left")
                {
                    int amount = Int32.Parse(lotcommand[1]);
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - (amount * 7));
                }

                else if (lotcommand[0].ToLower() == "forward")
                {

                    scrollViewer.ScrollToBottom();

                    double distance = mainWindow.Height;

                    double var;

                    if (open) var = distance * 0.022;
                    else var = distance * 0.033;

                    int con = exactlocation - Convert.ToInt32(Math.Floor(var));

                    if (con < 1)
                    {
                        exactlocation = 0;
                        gridinfo.SelectedItem = gridinfo.Items[exactlocation];
                        gridinfo.ScrollIntoView(gridinfo.Items[exactlocation]);
                    }
                    else
                    {
                        exactlocation = con;
                        gridinfo.SelectedItem = gridinfo.Items[exactlocation];
                        gridinfo.ScrollIntoView(gridinfo.Items[exactlocation]);
                    }
                }
                else if (lotcommand[0].ToLower() == "back")
                {
                    scrollViewer.ScrollToBottom();
                    double maindistance = mainWindow.Height;
                    double var;

                    if (open) var = maindistance * 0.022;
                    else var = maindistance * 0.033;

                    int b = exactlocation + Convert.ToInt32(Math.Floor(var));

                    if (b < eachrow.Count() - 1)
                    {
                        exactlocation = b;

                    }
                    else
                    {
                        exactlocation = eachrow.Count() - 1;

                    }

                    gridinfo.ScrollIntoView(gridinfo.Items[exactlocation]);
                }
                else if (lotcommand[0].ToLower() == "setcl")
                {
                    int c = Int32.Parse(lotcommand[1]);
                    exactlocation = c + exactlocation - 1;

                    if (exactlocation < eachrow.Count() - 1)
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(updatedRow));
                        cls2.Content = "We are in the line: = " + exactlocation;

                    }
                    else
                    {
                        exactlocation = eachrow.Count();
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(updatedRow));
                        cls2.Content = "We are in the line: = " + exactlocation;
                    }
                }
                else if (lotcommand[0].ToLower() == "change")
                {
                    if (point)
                    {
                        if (lotcommand.Count() == 2 || lotcommand.Count() == 3)
                        {
                            string search = lotcommand[1].Split('/')[1];
                            string newreplace = lotcommand[1].Split('/')[2];
                            int line, k = 0;
                            string rightone;
                            Boolean updatedlocation = false;

                            if (lotcommand[1].Split('/')[3] == "*") line = eachrow.Count();
                            else line = Int32.Parse(lotcommand[1].Split('/')[3]);

                            if (lotcommand[2] != null && lotcommand[2] == "*")
                            {
                                for (int i = 0; i < eachrow.Count(); i++)
                                {
                                    if (eachrow[i].data.Contains(search))
                                    {
                                        updatedlocation = true;
                                        k++;

                                        rightone = eachrow[i].data.Replace(search, newreplace);
                                        eachrow[i].data = rightone;
                                    }
                                }
                            }
                            else
                            {
                                for (int i = exactlocation - 1; i < line && i < eachrow.Count(); i++)
                                {
                                    if (eachrow[i].data.Contains(search))
                                    {
                                        updatedlocation = true;
                                        k++;

                                        rightone = eachrow[i].data.Replace(search, newreplace);
                                        eachrow[i].data = rightone;
                                    }
                                }
                            }

                            if (updatedlocation)
                            {
                                gridinfo.ItemsSource = eachrow;
                                gridinfo.Items.Refresh();
                                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(updatedRow));
                                MessageBox.Show(search + " changed with the text: " + newreplace + " " + k + " quantity.");
                            }
                            else MessageBox.Show(search + " there is no such text.");
                        }
                        else MessageBox.Show("The syntax must be in proper form");
                    }
                    else MessageBox.Show("Can't opened folder .");
                }
                else MessageBox.Show("The syntax must be in proper form");

                virtualtext.Text = "";
            }
        }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            var splitting = virtualtext.Text;
            string[] commands = splitting.Split(' ');

            if (point)
            {
                StreamWriter f = new StreamWriter(location);

                for (int i = 0; i < eachrow.Count(); i++) f.WriteLine(eachrow[i].data);

                f.Close();
                cleanLabels();
                MessageBox.Show("File is saved successfully.");
            }
            else
            {
                MessageBox.Show("Open a file first to use SAVE option.");

            }

            point = false;
        }



        private void updatedRow()
        {
            foreach (hold datas in gridinfo.ItemsSource)
            {
                var row = gridinfo.ItemContainerGenerator.ContainerFromItem(datas) as DataGridRow;
                if (datas.lineNum == exactlocation) row.Foreground = Brushes.Red;
                else row.Foreground = Brushes.Black;
            }
        }



        private void saveAs_Click(object sender, RoutedEventArgs e)
        {
            var command = virtualtext.Text;
            string[] commands = command.Split(' ');
            StreamWriter file = new StreamWriter(commands[0]);

            if (commands[0] == "")
            {
                MessageBox.Show("Open a file first to use SAVE option.");

            }
            else


                for (int i = 0; i < eachrow.Count(); i++) file.WriteLine(eachrow[i].data);

            file.Close();

            cleanLabels();

            MessageBox.Show("File saved in new name " + commands[0]);
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save: Overwrites the original folder.\nSave As: Saves the folder to the location to the referanced direction at command line.\n" +
                                   "Open: Opens a specified folder with the path information.\nSearch: String search option is provided to the user.\n" +
                                   "#: Allow user to skip to line # number.\nUp #: Allow user to scroll up # lines.\nDown #: Allow user to scroll down # lines.\n" +
                                   "Left #: Allow user to scroll left # lines.\nRight #: Allow user to scroll right # lines.\nForward: Allow user to scrolls one screenfull towards the end of folder.\n" +
                                   "Setcl #: Allow user to select which line number is the current line.\nChange: Allow user to finds & modifies a searched string, starting with the defined currentline.\n" +
                                   "Back: Allow user to scrolls one screenfull towards the top of folder.\n"
                              );
        }

        private void dataGrid_up(object sender, KeyEventArgs e)
        {
            int p = 0;
            Boolean newitem = false;
            DataGridCell d = e.OriginalSource as DataGridCell;

            if (e.Key == Key.Enter)
            {
                if (d.Foreground.ToString() == "#ffff4d")
                {
                    foreach (hold holder in gridinfo.ItemsSource)
                    {
                        if (holder.suffix[0] == 'i')
                        {
                            int linequantity = (int)Char.GetNumericValue(holder.suffix[1]);
                            for (int i = p + 1; i < (p + linequantity + 1); i++) eachrow.Insert(i, new hold { lineNum = i, data = "", suffix = "=====" });

                            newitem = true;
                            break;
                        }
                        else if (holder.suffix[0] == 'x')
                        {
                            if (holder.suffix == "x")
                            {
                                hide.Add(eachrow[p + 1]);
                                eachrow.RemoveAt(p + 1);
                                eachrow.Insert(p + 1, new hold { lineNum = p + 1, data = "excluded line", suffix = "=====" });
                            }
                            else
                            {
                                int linequantity = (int)Char.GetNumericValue(holder.suffix[1]);

                                for (int i = p + 1; i < (p + linequantity + 1); i++) hide.Add(eachrow[i]);
                                for (int j = p; j < (p + linequantity); j++) eachrow.RemoveAt(j + 1);
                                eachrow.Insert(p + 1, new hold { lineNum = p + 1, data = "excluded line", suffix = "=====" });
                            }

                            newitem = true;
                            break;
                        }
                        else if (holder.suffix[0] == 's' && holder.data == "excluded line")
                        {
                            if (holder.suffix == "s")
                            {
                                eachrow.Insert(p + 1, hide[(hide.Count() - 1)]);
                                hide.RemoveAt(hide.Count() - 1);
                                if (hide.Count() == 0) eachrow.RemoveAt(p);
                            }
                            else
                            {
                                int linequantity = (int)Char.GetNumericValue(holder.suffix[1]);
                                int count = 0;

                                for (int i = p + 1; i < (p + linequantity + 1); i++)
                                {
                                    eachrow.Insert(i, hide[(hide.Count() - linequantity + count)]);
                                    count++;
                                }

                                for (int i = p + 1; i < (p + linequantity + 1); i++) hide.RemoveAt(hide.Count() - 1);
                                if (hide.Count() == 0) eachrow.RemoveAt(p);
                            }

                            newitem = true;
                            break;
                        }
                        else if (holder.suffix[0] == 'a')
                        {
                            if (extrarow.Count() != 0)
                            {
                                int linequantity = extrarow.Count();
                                int count = 0;

                                for (int i = p + 1; i < (p + linequantity + 1); i++)
                                {
                                    string intert = extrarow[(extrarow.Count() - linequantity + count)].data;
                                    eachrow.Insert(i, new hold { lineNum = p + 1, data = intert, suffix = "=====" });
                                    count++;
                                }
                                extrarow.Clear();
                            }
                            else MessageBox.Show("Move the line initally or copy it.");

                            newitem = true;
                            break;
                        }
                        else if (holder.suffix[0] == 'b')
                        {
                            if (extrarow.Count() != 0)
                            {
                                int linequantity = extrarow.Count();
                                int count = 0;

                                for (int i = p + 1; i < (p + linequantity + 1); i++)
                                {
                                    string datasb = extrarow[(extrarow.Count() - linequantity + count)].data;
                                    eachrow.Insert(i - 1, new hold { lineNum = p + 1, data = datasb, suffix = "=====" });
                                    count++;
                                }
                                extrarow.Clear();
                            }
                            else MessageBox.Show("Please copy line first.");

                            newitem = true;
                            break;
                        }
                        else if (holder.suffix[0] == 'c')
                        {
                            if (holder.suffix == "c") extrarow.Add(eachrow[p]);
                            else
                            {
                                int linequantity = (int)Char.GetNumericValue(holder.suffix[1]);
                                for (int i = p; i < (p + linequantity); i++) extrarow.Add(eachrow[i]);
                            }

                            newitem = true;
                            break;
                        }
                        else if (holder.suffix[0] == 'm')
                        {
                            if (holder.suffix == "m")
                            {
                                extrarow.Add(eachrow[p]);
                                eachrow.RemoveAt(p);
                            }
                            else
                            {
                                int linequantity = (int)Char.GetNumericValue(holder.suffix[1]);
                                for (int i = p; i < (p + linequantity); i++) extrarow.Add(eachrow[i]);
                                for (int j = p; j < (p + linequantity); j++) eachrow.RemoveAt(j);
                            }

                            newitem = true;
                            break;
                        }
                        else if (holder.suffix[0] == '“' || holder.suffix[0] == '"')
                        {
                            string datasb = eachrow[p].data;
                            eachrow.Insert(p + 1, new hold { lineNum = p + 1, data = datasb, suffix = "=====" });

                            newitem = true;
                            break;
                        }

                        p++;
                    }
                }
            }

            if (newitem)
            {
                UpdateColumns();
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(updatedRow));
            }
        }

        private void UpdateColumns()
        {
            int count = 0;

            for (int i = 0; i < eachrow.Count; i++)
            {
                eachrow[i].suffix = "=====";
                eachrow[i].lineNum = i + 1;
                count++;
            }

            cls3.Content = "Size of the line = " + count;
            gridinfo.ItemsSource = eachrow;
            gridinfo.Items.Refresh();
        }

        private void cleanLabels()
        {
            cls1.Content = "Name of the file:";
            cls2.Content = "Current Line Number Information:";
            cls3.Content = "Size: ";

            gridinfo.ItemsSource = null;
            gridinfo.Items.Refresh();
            point = false;
        }



        public void update_size(object sender, RoutedEventArgs e)
        {
            if (fopen) open = false;
            else fopen = true;
        }


    }
    

}