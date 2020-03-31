using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetPageContentHTMLFormat(string Url)
    {
        System.Net.WebResponse Result = null;
        string Page_Source_Code;
        try
        {
            //Create a Web Request instance using url
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
            //Get the result from request 
            Result = req.GetResponse();
            System.IO.Stream RStream = Result.GetResponseStream();
            //Read result to streamreader to end
            System.IO.StreamReader sr = new System.IO.StreamReader(RStream);
            new System.IO.StreamReader(RStream);
            Page_Source_Code = sr.ReadToEnd();
            sr.Dispose();
        }
        catch
        {
            // error while reading the url: the url dosen’t exist, connection problem...
            Page_Source_Code = "";
        }
        finally
        {
            if (Result != null) Result.Close();
        }
        //return the same html content format read from url.
        return Page_Source_Code.ToString();
    }

    [WebMethod]
    public string GetPageContentHTMLToPlainData(string Url)
    {
        System.Net.WebResponse Result = null;
        string Page_Source_Code;
        try
        {
            //Create a Web Request instance using url
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
            //Get the result from request 
            Result = req.GetResponse();
            System.IO.Stream RStream = Result.GetResponseStream();
            //Read result to streamreader to end
            System.IO.StreamReader sr = new System.IO.StreamReader(RStream);
            new System.IO.StreamReader(RStream);
            Page_Source_Code = sr.ReadToEnd();
            sr.Dispose();
        }
        catch
        {
            // error while reading the url: the url dosen’t exist, connection problem...
            Page_Source_Code = "";
        }
        finally
        {
            if (Result != null) Result.Close();
        }


        //Using HtmlAgilityPack PACKAGE read the original data from html format and return 
        StringBuilder pureText = new StringBuilder();
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(Page_Source_Code);

        foreach (HtmlNode node in doc.DocumentNode.ChildNodes)
        {
            pureText.Append(node.InnerText);
        }

        return pureText.ToString();
    }

    [WebMethod]
    public string GetPageContentHTMLToRandomNameChange(string Url)
    {
        System.Net.WebResponse Result = null;
        string Page_Source_Code;
        try
        {
            //Create a Web Request instance using url
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
            //Get the result from request 
            Result = req.GetResponse();
            System.IO.Stream RStream = Result.GetResponseStream();
            //Read result to streamreader to end
            System.IO.StreamReader sr = new System.IO.StreamReader(RStream);
            new System.IO.StreamReader(RStream);
            Page_Source_Code = sr.ReadToEnd();
            sr.Dispose();
        }
        catch
        {
            // error while reading the url: the url dosen’t exist, connection problem...
            Page_Source_Code = "";
        }
        finally
        {
            if (Result != null) Result.Close();
        }

        //Using HtmlAgilityPack PACKAGE read the original data from html format and return
        StringBuilder pureText = new StringBuilder();
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(Page_Source_Code);

        foreach (HtmlNode node in doc.DocumentNode.ChildNodes)
        {
            pureText.Append(node.InnerText);
        }


        //use list of possible separators to get the sentence seperated by each word.
        string[] separators = new string[] { ",", ".", "!", "\'", " ", "\'s" };

        //get the final data to be changed.
        string finaldata = pureText.ToString();
        foreach (string word in finaldata.Split(separators, StringSplitOptions.RemoveEmptyEntries))
        {
            //fix the length of random name generate to be length of the replacing word length
            int len = word.Length;
            Random r = new Random();

            //list a few possible combination of alphabets  separating constants and vowels

            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            //generate a name substring using both 
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }
            //replace the current word with new randow word.
            finaldata = finaldata.Replace(word, Name);
        }

        return finaldata;
    }

}
