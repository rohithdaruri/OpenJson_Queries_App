using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenJson_Queries_App
{
    public class QueryJson
    {
        public void Filter()
        {
            using (SqlConnection con = new SqlConnection("server=(localdb)\\MSSQLLocalDB;database=SampleDataDB;Trusted_Connection=True;"))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(JsonQuery(), con);
                adapter.Fill(ds);
                con.Close();

                var data = (from DataRow dr in ds.Tables[0].Rows
                 select new
                 {
                     Name = dr["Name"].ToString(),
                     Age = dr["Age"].ToString(),
                     FatherName = dr["FatherName"].ToString(),
                     Hno = dr["Hno"].ToString(),
                     StreetName = dr["StreetName"].ToString(),
                     City = dr["City"].ToString(),
                     State = dr["State"].ToString(),
                     PIN = dr["PIN"].ToString(),
                     Mobile = dr["Mobile"].ToString(),
                     AlternateMobile = dr["AlternateMobile"].ToString(),
                     IntrestedMovies = dr["IntrestedMovies"].ToString()
                 }).ToList();

                var sampledata = JsonConvert.SerializeObject(data);
                Console.WriteLine(sampledata);
                ds.Dispose();
            }
        }

        public string JsonQuery()
        {
            string query = "Select main.Name, main.Age, main.FatherName, address.Hno, address.StreetName, address.City, address.State, address.PIN, contact.Mobile, contact.AlternateMobile, intrestedmovies.IntrestedMovies from SampleDatas cross apply openjson(CAST(content as nvarchar(max))) with(Name varchar(500) '$.Name',Age varchar(500) '$.Age',FatherName varchar(500) '$.FatherName') as main cross apply openjson(CAST(content as nvarchar(max))) with (Hno varchar(500) '$.Address.Hno',StreetName varchar(500) '$.Address.StreetName',City varchar(500) '$.Address.City',State varchar(500) '$.Address.State',PIN varchar(500) '$.Address.PIN') as address cross apply openjson(CAST(content as nvarchar(max))) with (Mobile varchar(500) '$.Contact.Mobile',AlternateMobile varchar(500) '$.Contact.AlternateMobile') as contact cross apply openjson(CAST(content as nvarchar(max))) with (intrestedmovies nvarchar(max) '$.IntrestedMovies' as json) as intrestedmovies";
            return query;
        }

    }

}
