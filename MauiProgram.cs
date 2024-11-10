using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Maui.Controls;

namespace Lab2MAUI
{
    public static class MauiProgram
    {
        public class Publication
        {
            public string Name { get; set; }
            public string Faculty { get; set; }
            public string Department { get; set; }
            public string Lab { get; set; }
            public string Position { get; set; }
            public string Research { get; set; }
            public string Customer { get; set; }
            public string CustomerAddress { get; set; }
            public string Subordination { get; set; }
            public string Field { get; set; }

            public Publication() { }
        }

        static private string xmlFilePath;

        public interface IStrategy
        {
            List<Publication> Search(Publication publication);
        }
        public class Searcher
        {
            private Publication publication;
            private IStrategy strategy;

            public Searcher(Publication p, IStrategy str, string path)
            {
                publication = p;
                strategy = str;
                xmlFilePath = path;
            }

            public List<Publication> SearchAlgorithm()
            {
                return strategy.Search(publication);
            }
        }

        public class Sax : IStrategy
        {
            public List<Publication> Search(Publication publication)
            {
                List<Publication> results = new List<Publication>();
                XmlTextReader xmlReader;

                try
                {
                    xmlReader = new XmlTextReader(xmlFilePath);
                }
                catch
                {
                    return null;
                }

                while (xmlReader.Read())
                {
                    if (xmlReader.HasAttributes)
                    {
                        while (xmlReader.MoveToNextAttribute())
                        {
                            string name = "";
                            string faculty = "";
                            string department = "";
                            string lab = "";
                            string position = "";
                            string reserch = "";
                            string customer = "";
                            string customerAddress = "";
                            string subordination = "";
                            string field = "";

                            if (xmlReader.Name.Equals("NAME") && (xmlReader.Value.Equals(publication.Name) || publication.Name == null))
                            {
                                name = xmlReader.Value;
                                xmlReader.MoveToNextAttribute();
                                if (xmlReader.Name.Equals("FACULTY") && (xmlReader.Value.Equals(publication.Faculty) || publication.Faculty == null))
                                {
                                    faculty = xmlReader.Value;
                                    xmlReader.MoveToNextAttribute();
                                    if (xmlReader.Name.Equals("DEPARTMENT") && (xmlReader.Value.Equals(publication.Department) || publication.Department == null))
                                    {
                                        department = xmlReader.Value;
                                        xmlReader.MoveToNextAttribute();
                                        if (xmlReader.Name.Equals("LAB") && (xmlReader.Value.Equals(publication.Lab) || publication.Lab == null))
                                        {
                                            lab = xmlReader.Value;
                                            xmlReader.MoveToNextAttribute();
                                            if (xmlReader.Name.Equals("POSITION") && (xmlReader.Value.Equals(publication.Position) || publication.Position == null))
                                            {
                                                position = xmlReader.Value;
                                                xmlReader.MoveToNextAttribute();
                                                if (xmlReader.Name.Equals("RESEARCH") && (xmlReader.Value.Equals(publication.Research) || publication.Research == null))
                                                {
                                                    reserch = xmlReader.Value;
                                                    xmlReader.MoveToNextAttribute();
                                                    if (xmlReader.Name.Equals("CUSTOMER") && (xmlReader.Value.Equals(publication.Customer) || publication.Customer == null))
                                                    {
                                                        customer = xmlReader.Value;
                                                        xmlReader.MoveToNextAttribute();
                                                        if (xmlReader.Name.Equals("CUSTOMER_ADDRESS") && (xmlReader.Value.Equals(publication.CustomerAddress) || publication.CustomerAddress == null))
                                                        {
                                                            customerAddress = xmlReader.Value;
                                                            xmlReader.MoveToNextAttribute();
                                                            if (xmlReader.Name.Equals("SUBORDINATION") && (xmlReader.Value.Equals(publication.Subordination) || publication.Subordination == null))
                                                            {
                                                                subordination = xmlReader.Value;
                                                                xmlReader.MoveToNextAttribute();
                                                                if (xmlReader.Name.Equals("FIELD") && (xmlReader.Value.Equals(publication.Field) || publication.Field == null))
                                                                {
                                                                    field = xmlReader.Value;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (name != "" && faculty != "" && department != "" && lab != "" && position != "" && reserch != "" && customer != "" && customerAddress != "" && subordination != "" && field != "")
                            {
                                Publication newPublication = new Publication { Name = name, Department = department, Faculty = faculty, Lab = lab, Position = position, Research = reserch, Customer = customer, CustomerAddress = customerAddress, Subordination = subordination, Field = field };
                                results.Add(newPublication);
                            }
                        }
                    }
                }
                xmlReader.Close();
                return results;
            }
        }

        public class Dom : IStrategy
        {
            public List<Publication> Search(Publication publication)
            {
                List<Publication> results = new List<Publication>();
                XmlDocument doc = new XmlDocument();

                try
                {
                    doc.Load(xmlFilePath);
                }
                catch
                {
                    return null;
                }

                XmlNode node = doc.DocumentElement;
                foreach (XmlNode n in node.ChildNodes)
                {
                    string name = "";
                    string faculty = "";
                    string department = "";
                    string lab = "";
                    string position = "";
                    string reserch = "";
                    string customer = "";
                    string customerAddress = "";
                    string subordination = "";
                    string field = "";

                    foreach (XmlAttribute attribute in n.Attributes)
                    {
                        if (attribute.Name.Equals("NAME") && (attribute.Value.Equals(publication.Name) || publication.Name == null))
                            name = attribute.Value;
                        if (attribute.Name.Equals("FACULTY") && (attribute.Value.Equals(publication.Faculty) || publication.Faculty == null))
                            faculty = attribute.Value;
                        if (attribute.Name.Equals("DEPARTMENT") && (attribute.Value.Equals(publication.Department) || publication.Department == null))
                            department = attribute.Value;
                        if (attribute.Name.Equals("LAB") && (attribute.Value.Equals(publication.Lab) || publication.Lab == null))
                            lab = attribute.Value;
                        if (attribute.Name.Equals("POSITION") && (attribute.Value.Equals(publication.Position) || publication.Position == null))
                            position = attribute.Value;
                        if (attribute.Name.Equals("RESEARCH") && (attribute.Value.Equals(publication.Research) || publication.Research == null))
                            reserch = attribute.Value;
                        if (attribute.Name.Equals("CUSTOMER") && (attribute.Value.Equals(publication.Customer) || publication.Customer == null))
                            customer = attribute.Value;
                        if (attribute.Name.Equals("CUSTOMER_ADDRESS") && (attribute.Value.Equals(publication.CustomerAddress) || publication.CustomerAddress == null))
                            customerAddress = attribute.Value;
                        if (attribute.Name.Equals("SUBORDINATION") && (attribute.Value.Equals(publication.Subordination) || publication.Subordination == null))
                            subordination = attribute.Value;
                        if (attribute.Name.Equals("FIELD") && (attribute.Value.Equals(publication.Field) || publication.Field == null))
                            field = attribute.Value;
                    }

                    if (name != "" && faculty != "" && department != "" && lab != "" && position != "" && reserch != "" && customer != "" && customerAddress != "" && subordination != "" && field != "")
                    {
                        Publication newPublication = new Publication { Name = name, Department = department, Faculty = faculty, Lab = lab, Position = position, Research = reserch, Customer = customer, CustomerAddress = customerAddress, Subordination = subordination, Field = field };
                        results.Add(newPublication);
                    }
                }
                return results;
            }
        }

        public class Linq : IStrategy
        {
            public List<Publication> Search(Publication publication)
            {
                List<Publication> results = new List<Publication>();
                XDocument doc;

                Debug.WriteLine(1);

                doc = XDocument.Load(xmlFilePath);

                var result = from obj in doc.Descendants("publication")
                             where
                             (
                             (obj.Attribute("NAME").Value.Equals(publication.Name) || publication.Name == null) &&
                             (obj.Attribute("FACULTY").Value.Equals(publication.Faculty) || publication.Faculty == null) &&
                             (obj.Attribute("DEPARTMENT").Value.Equals(publication.Department) || publication.Department == null) &&
                             (obj.Attribute("LAB").Value.Equals(publication.Lab) || publication.Lab == null) &&
                             (obj.Attribute("POSITION").Value.Equals(publication.Position) || publication.Position == null) &&
                             (obj.Attribute("RESEARCH").Value.Equals(publication.Research) || publication.Research == null) &&
                             (obj.Attribute("CUSTOMER").Value.Equals(publication.Customer) || publication.Customer == null) &&
                             (obj.Attribute("CUSTOMER_ADDRESS").Value.Equals(publication.CustomerAddress) || publication.CustomerAddress == null) &&
                             (obj.Attribute("SUBORDINATION").Value.Equals(publication.Subordination) || publication.Subordination == null) &&
                             (obj.Attribute("FIELD").Value.Equals(publication.Field) || publication.Field == null)
                             )
                             select new
                             {
                                 name = (string)obj.Attribute("NAME"),
                                 faculty = (string)obj.Attribute("FACULTY"),
                                 department = (string)obj.Attribute("DEPARTMENT"),
                                 lab = (string)obj.Attribute("LAB"),
                                 position = (string)obj.Attribute("POSITION"),
                                 research = (string)obj.Attribute("RESEARCH"),
                                 customer = (string)obj.Attribute("CUSTOMER"),
                                 customerAddress = (string)obj.Attribute("CUSTOMER_ADDRESS"),
                                 subordination = (string)obj.Attribute("SUBORDINATION"),
                                 field = (string)obj.Attribute("FIELD"),
                             };

                Debug.WriteLine(2);

                foreach (var p in result)
                {
                    Debug.WriteLine(3);
                    Publication newPublication = new Publication { Name = p.name, Department = p.department, Faculty = p.faculty, Lab = p.lab, Position = p.position, Research = p.research, Customer = p.customer, CustomerAddress = p.customerAddress, Subordination = p.subordination, Field = p.field };
                    results.Add(newPublication);
                }
                return results;
            }
        }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
