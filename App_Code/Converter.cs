using System;
using System.Data;
using Newtonsoft.Json;
using System.IO;

public class Converter
{
    public static string convertToJSON(DataTable dt)
    {
       return JsonConvert.SerializeObject(dt);
    }
    public static string convertToXML(DataTable dt)
    {
        try
        {
            using (StringWriter sw = new StringWriter())
            {
                dt.WriteXml(sw);
                return sw.ToString();
            }
        }
        catch(Exception e)
        {
            throw e;
        }
    }

    //http://stackoverflow.com/questions/19682996/datatable-to-html-table
    public static string convertToHTML(DataTable dt)
    {
        string html = "<table>";
        //add header row
        html += "<tr>";
        for (int i = 0; i < dt.Columns.Count; i++)
            html += "<td>" + dt.Columns[i].ColumnName + "</td>";
        html += "</tr>";
        //add rows
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            html += "<tr>";
            for (int j = 0; j < dt.Columns.Count; j++)
                html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
            html += "</tr>";
        }
        html += "</table>";
        return html;
    }

}