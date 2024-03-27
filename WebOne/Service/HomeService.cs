using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using WebOne.Models;

namespace WebOne.Service
{
    public class HomeService
    {
        private readonly SqlConnection con = MvcApplication.GetSunghwa91();


        public List<TransList> GetTransList(string invNo)
        {
            List<TransList> transList = new List<TransList>();

            con.Open();
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText
                    = "SELECT c.bra_name, " +
                             "b.cod_cont, " +
                             "LEFT(CONVERT(date, a.scann_date), 11) as scann_date, " +
                             "LEFT(substring(a.scann_time, 0, 3) + ':' + substring(a.scann_time, 3, 5), 5) as scann_time, " +
                             "a.trs_name " +
                      "FROM ls101t0 A " +
                      "LEFT JOIN ls901t0 B on A.SCANN_SLT = B.COD " +
                      "LEFT JOIN ls801t0 C on A.BRA_ID = C.BRA_ID " +
                     $"WHERE inv_no = '{invNo}' " +
                      "ORDER BY scann_date";

                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TransList item = new TransList()
                        {
                            bra_name = reader["bra_name"].ToString(),
                            cod_cont = reader["cod_cont"].ToString(),
                            scann_date = reader["scann_date"].ToString(),
                            scann_time = reader["scann_time"].ToString(),
                            trs_name = reader["trs_name"].ToString()
                        };

                        transList.Add(item);
                    }
                    con.Close();
                }
            }

            return transList;
        }

        public TransInfo GetTransInfo(string invNo)
        {
            TransInfo transInfo = new TransInfo();

            con.Open();
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText 
                    = "SELECT g_cust_name as receiver_name, " +
                             "g_addr as receiver_address, " +
                             "s_cust_name as sender_name, " +
                             "s_addr as sender_address " +
                      "FROM ls001t0 " +
                     $"WHERE inv_no = '{invNo}'";

                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transInfo.sender_name = reader["sender_name"].ToString();
                        transInfo.sender_address = reader["sender_address"].ToString();
                        transInfo.receiver_name = reader["receiver_name"].ToString();
                        transInfo.receiver_address = reader["receiver_address"].ToString();
                    }
                    con.Close();
                }
            }

            return transInfo;
        }
    }
}