using System.Reflection;
using System.Xml.Xsl;
using System.Xml;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using Microsoft.Maui;

namespace Lab2MAUI
{
    public partial class MainPage : ContentPage
    {
        private string xmlFilePath = "";
        public MainPage()
        {
            InitializeComponent();
            SaxBtn.IsChecked = true;
        }

        private async void GetAllAuthors()
        {
            XmlDocument doc = new XmlDocument();
            var appLocation = Assembly.GetEntryAssembly().Location;
            var appPath = Path.GetDirectoryName(appLocation);
            Directory.SetCurrentDirectory(appPath);

            try
            {
                doc.Load(xmlFilePath);
            }
            catch 
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Incorrect file!", "OK");
                return;
            }

            XmlElement xRoot = doc.DocumentElement;
            XmlNodeList childNodes = xRoot.SelectNodes("publication");

            Debug.WriteLine(childNodes.Count);
            for (int i = 0; i < childNodes.Count; i++)
            {
                XmlNode n = childNodes.Item(i);
                addItems(n);
            }

        }

        private void addItems(XmlNode n)
        {
            if (!NamePicker.Items.Contains(n.SelectSingleNode("@NAME").Value))
                NamePicker.Items.Add(n.SelectSingleNode("@NAME").Value);
            if (!FacultyPicker.Items.Contains(n.SelectSingleNode("@FACULTY").Value))
                FacultyPicker.Items.Add(n.SelectSingleNode("@FACULTY").Value);
            if (!DepartmentPicker.Items.Contains(n.SelectSingleNode("@DEPARTMENT").Value))
                DepartmentPicker.Items.Add(n.SelectSingleNode("@DEPARTMENT").Value);
            if (!LabPicker.Items.Contains(n.SelectSingleNode("@LAB").Value))
                LabPicker.Items.Add(n.SelectSingleNode("@LAB").Value);
            if (!PositionPicker.Items.Contains(n.SelectSingleNode("@POSITION").Value))
                PositionPicker.Items.Add(n.SelectSingleNode("@POSITION").Value);
            if (!ResearchPicker.Items.Contains(n.SelectSingleNode("@RESEARCH").Value))
                ResearchPicker.Items.Add(n.SelectSingleNode("@RESEARCH").Value);
            if (!CustomerPicker.Items.Contains(n.SelectSingleNode("@CUSTOMER").Value))
                CustomerPicker.Items.Add(n.SelectSingleNode("@CUSTOMER").Value);
            if (!CustomerAddressPicker.Items.Contains(n.SelectSingleNode("@CUSTOMER_ADDRESS").Value))
                CustomerAddressPicker.Items.Add(n.SelectSingleNode("@CUSTOMER_ADDRESS").Value);
            if (!SubordinationPicker.Items.Contains(n.SelectSingleNode("@SUBORDINATION").Value))
                SubordinationPicker.Items.Add(n.SelectSingleNode("@SUBORDINATION").Value);
            if (!FieldPicker.Items.Contains(n.SelectSingleNode("@FIELD").Value))
                FieldPicker.Items.Add(n.SelectSingleNode("@FIELD").Value);
        }


        private void SearchBtnHandler(object sender, EventArgs e)
        {
            editor.Text = "";

            MauiProgram.Publication publication = GetSelectedParameters();
            MauiProgram.IStrategy analyzer = GetSelectedAnalyzer();
            PerformSearch(publication, analyzer);
        }

        private MauiProgram.Publication GetSelectedParameters()
        {
            MauiProgram.Publication publication = new MauiProgram.Publication();

            if (NameCheckBox.IsChecked)
            {
                if (NamePicker.SelectedIndex != -1)
                    publication.Name = NamePicker.SelectedItem.ToString();
                else
                    publication.Name = "";
            }
            if (FacultyCheckBox.IsChecked)
            {
                if (FacultyPicker.SelectedIndex != -1)
                    publication.Faculty = FacultyPicker.SelectedItem.ToString();
                else
                    publication.Faculty = "";
            }
            if (DepartmentCheckBox.IsChecked)
            {
                if (DepartmentPicker.SelectedIndex != -1)
                    publication.Department = DepartmentPicker.SelectedItem.ToString();
                else
                    publication.Department = "";
            }
            if (LabCheckBox.IsChecked)
            {
                if (LabPicker.SelectedIndex != -1)
                    publication.Lab = LabPicker.SelectedItem.ToString();
                else
                    publication.Lab = "";
            }
            if (PositionCheckBox.IsChecked)
            {
                if (PositionPicker.SelectedIndex != -1)
                    publication.Position = PositionPicker.SelectedItem.ToString();
                else
                    publication.Position = "";
            }
            if (ResearchCheckBox.IsChecked)
            {
                if (ResearchPicker.SelectedIndex != -1)
                    publication.Research = ResearchPicker.SelectedItem.ToString();
                else
                    publication.Research = "";
            }
            if (CustomerCheckBox.IsChecked)
            {
                if (CustomerPicker.SelectedIndex != -1)
                    publication.Customer = CustomerPicker.SelectedItem.ToString();
                else
                    publication.Customer = "";
            }
            if (CustomerAddressCheckBox.IsChecked)
            {
                if (CustomerPicker.SelectedIndex != -1)
                    publication.CustomerAddress = CustomerAddressPicker.SelectedItem.ToString();
                else
                    publication.Customer = "";
            }
            if (SubordinationCheckBox.IsChecked)
            {
                if (SubordinationPicker.SelectedIndex != -1)
                    publication.Subordination = SubordinationPicker.SelectedItem.ToString();
                else
                    publication.Subordination = "";
            }
            if (FieldCheckBox.IsChecked)
            {
                if (FieldPicker.SelectedIndex != -1)
                    publication.Field = FieldPicker.SelectedItem.ToString();
                else
                    publication.Field = "";
            }

            return publication;
        }

        private MauiProgram.IStrategy GetSelectedAnalyzer()
        {
            MauiProgram.IStrategy analyzer = null;

            try
            {
                if (SaxBtn.IsChecked)
                {
                    analyzer = new MauiProgram.Sax();
                }
                if (DomBtn.IsChecked)
                {
                    analyzer = new MauiProgram.Dom();
                }
                if (LinqBtn.IsChecked)
                {
                    analyzer = new MauiProgram.Linq();
                }
            }
            catch
            {
                return null;
            }

            return analyzer;
        }

        private void PerformSearch(MauiProgram.Publication publication, MauiProgram.IStrategy analyzer)
        {
            MauiProgram.Searcher search = new MauiProgram.Searcher(publication, analyzer, xmlFilePath);
            List<MauiProgram.Publication> results = search.SearchAlgorithm();

            if (results == null) return;

            foreach (MauiProgram.Publication p in results)
            {
                
                editor.Text += "Name: " + p.Name + "\n";
                editor.Text += "Faculty: " + p.Faculty + "\n";
                editor.Text += "Department: " + p.Department + "\n";
                editor.Text += "Lab: " + p.Lab + "\n";
                editor.Text += "Position: " + p.Position + "\n";
                editor.Text += "Research: " + p.Research + "\n";
                editor.Text += "Customer: " + p.Customer + "\n";
                editor.Text += "Customer address: " + p.CustomerAddress + "\n";
                editor.Text += "Subordination: " + p.Subordination + "\n";
                editor.Text += "Field: " + p.Field + "\n";
                editor.Text += "\n";
            }
        }

        private void ClearFields(object sender, EventArgs e)
        {
            editor.Text = "";

            NameCheckBox.IsChecked = false;
            FacultyCheckBox.IsChecked = false;
            DepartmentCheckBox.IsChecked = false;
            LabCheckBox.IsChecked = false;
            PositionCheckBox.IsChecked = false;
            ResearchCheckBox.IsChecked = false;
            CustomerCheckBox.IsChecked = false;
            CustomerAddressCheckBox.IsChecked = false;
            SubordinationCheckBox.IsChecked = false;
            FieldCheckBox.IsChecked = false;

            NamePicker.SelectedItem = null;
            FacultyPicker.SelectedItem = null;
            DepartmentPicker.SelectedItem = null;
            LabPicker.SelectedItem = null;
            PositionPicker.SelectedItem = null;
            ResearchPicker.SelectedItem = null;
            CustomerPicker.SelectedItem = null;
            CustomerAddressPicker.SelectedItem = null;
            SubordinationPicker.SelectedItem = null;
            FieldPicker.SelectedItem = null;
        }

        private async void OnTransformToHTMLBtnClicked(object sender, EventArgs e)
        {
            XslCompiledTransform xct = LoadXSLT();
            if (xct == null) return;

            string xmlPath = xmlFilePath;

            if (xmlFilePath.Length == 0) return;
            string htmlPath = xmlFilePath.Substring(0, xmlFilePath.Length - 4) + ".html";

            XsltArgumentList xslArgs = await CreateXSLTArguments();

            TransformXMLToHTML(xct, xslArgs, xmlPath, htmlPath);

            await Application.Current.MainPage.DisplayAlert("Message", "File saved.", "Ok");
        }

        private async void OnOpenFileButton(object sender, EventArgs e)
        {
            var fileResult = await FilePicker.PickAsync();

            if (fileResult != null)
            {
                xmlFilePath = fileResult.FullPath;
            }

            editor.Text = "";

            GetAllAuthors();
        }

        private XslCompiledTransform LoadXSLT()
        {
            XslCompiledTransform xct = new XslCompiledTransform();

            try
            {
                xct.Load(Path.GetDirectoryName(xmlFilePath) + "Transform.xslt");
            }
            catch { }

            return xct;
        }

        private Task CreateXSLError()
        {
            return Application.Current.MainPage.DisplayAlert("Error", "Fill in all attributes!", "Ok");
        }

        private async Task<XsltArgumentList> CreateXSLTArguments()
        {
            XsltArgumentList xslArgs = new XsltArgumentList();

            // Add filter values from Pickers (null means "show all")
            AddParamIfNotNull(xslArgs, "name", NamePicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "faculty", FacultyPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "department", DepartmentPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "lab", LabPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "position", PositionPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "research", ResearchPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "customer", CustomerPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "customerAddress", CustomerAddressPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "subordination", SubordinationPicker.SelectedItem?.ToString());
            AddParamIfNotNull(xslArgs, "field", FieldPicker.SelectedItem?.ToString());

            // Add visibility flags from CheckBoxes
            xslArgs.AddParam("showName", "", NameCheckBox.IsChecked);
            xslArgs.AddParam("showFaculty", "", FacultyCheckBox.IsChecked);
            xslArgs.AddParam("showDepartment", "", DepartmentCheckBox.IsChecked);
            xslArgs.AddParam("showLab", "", LabCheckBox.IsChecked);
            xslArgs.AddParam("showPosition", "", PositionCheckBox.IsChecked);
            xslArgs.AddParam("showResearch", "", ResearchCheckBox.IsChecked);
            xslArgs.AddParam("showCustomer", "", CustomerCheckBox.IsChecked);
            xslArgs.AddParam("showCustomerAddress", "", CustomerAddressCheckBox.IsChecked);
            xslArgs.AddParam("showSubordination", "", SubordinationCheckBox.IsChecked);
            xslArgs.AddParam("showField", "", FieldCheckBox.IsChecked);

            return xslArgs;
        }

        private void AddParamIfNotNull(XsltArgumentList xslArgs, string name, string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                xslArgs.AddParam(name, "", value);
            }
        }

        private void TransformXMLToHTML(XslCompiledTransform xct, XsltArgumentList xslArgs, string xmlPath, string htmlPath)
        {
            using (XmlReader xr = XmlReader.Create(xmlPath))
            {
                using (XmlWriter xw = XmlWriter.Create(htmlPath))
                {
                    try
                    {
                        xct.Transform(xr, xslArgs, xw);
                    }
                    catch { }
                }
            }
        }

        private async void OnExitBtnClicked(object sender, EventArgs e)
        {
            var result = await Application.Current.MainPage.DisplayAlert("Exit", "Are you sure you want to exit the program?", "Yes", "No");
            if (result)
            {
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            int textLength = editor.Text.Length;
            int fontSize = CalculateFontSize(textLength);
            editor.FontSize = fontSize;
        }

        private int CalculateFontSize(int textLength)
        {
            if (textLength < 100)
            {
                return 18;
            }
            else if (textLength < 500)
            {
                return 14;
            }
            else
            {
                return 10;
            }
        }
    }

}
