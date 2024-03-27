using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebOne.Models;

namespace WebOne.Service
{
    public class HangService
    {
        private readonly SqlConnection _con = MvcApplication.GetSunghwa91();

        public List<Dictionary<string, string>> FindByPchNo(string pchNo)
        {
            List<string> list = new List<string>();
            
            List<Dictionary<string, string>> map = new List<Dictionary<string, string>>();
            
            _con.Open();
            using (SqlCommand cmd = _con.CreateCommand())
            {
                cmd.CommandText 
                    = "SELECT DISTINCT A.pch_no, " +
                                      "[dbo].날짜변환1(B.cre_date) as cre_date, " +
                                      "[dbo].날짜변환1(B.end_date) as end_date " +
                      "FROM ls102t0 A " +
                      "INNER JOIN ls201t0 B on (left(A.pch_no, 8)) = B.pch_no " + 
                     $"WHERE A.pch_no LIKE '{pchNo}%'";
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        dictionary.Add("pch_no", reader["pch_no"].ToString());
                        dictionary.Add("cre_date", reader["cre_date"].ToString());
                        dictionary.Add("end_date", reader["end_date"].ToString());

                        //list.Add(reader["pch_no"].ToString());

                        map.Add(dictionary);
                    }

                    _con.Close();
                }
                
            }
            return map;
        }

        public List<HangDetail> FindDetailByPchNo(string pchNo)
        {
            List<HangDetail> list = new List<HangDetail>();
            if (pchNo == null) return list;

            _con.Open();
            using (SqlCommand cmd = _con.CreateCommand())
            {
                cmd.CommandText 
                    = "SELECT B.bra_name, " +
                             "C.cod_cont, " +
                             "[dbo].날짜변환1(A.scann_date) as scann_date, " +
                             "[dbo].시간변환1(A.scann_time) as scann_time, " +
                             "A.car_id, " +
                             "A.trs_id, " +
                             "A.trs_name " +
                      "FROM ls102t0 A " +
                      "LEFT JOIN ls801t0 B on A.bra_id = B.bra_id " +
                      "LEFT JOIN ls901t0 C on A.scann_slt = C.cod " + 
                     $"WHERE A.pch_no = '{pchNo}' " +
                      "ORDER BY A.scann_date DESC";
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HangDetail item = new HangDetail
                        {
                            bra_name = reader["bra_name"].ToString(),
                            cod_cont = reader["cod_cont"].ToString(),
                            scann_date = reader["scann_date"].ToString(),
                            scann_time = reader["scann_time"].ToString(),
                            car_id = reader["car_id"].ToString(),
                            trs_id = reader["trs_id"].ToString(),
                            trs_name = reader["trs_name"].ToString()
                        };

                        list.Add(item);
                    }
                    _con.Close();
                }
            }

            return list;
        }
    }
}