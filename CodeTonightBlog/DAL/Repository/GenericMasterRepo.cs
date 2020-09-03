using CodeTonightBlog.DAL.Common;
using CodeTonightBlog.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodeTonightBlog.DAL.Repository
{
    public abstract class GenericMasterRepo<T> : IGenericMaster<T> where T : class
    {
        public IDbConnection DapperCon
        {
            get
            {
                return new SqlConnection(Connection.sqlConStr);

            }
        }
      
        public List<T> GetAll(string spname, string Activity)
        {
            List<T> objlist = new List<T>();
            Connection objCon = new Connection();
            SqlConnection con = objCon.GetConnection();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                con.Open();
            }
            var list = new List<T>();

            //using (IDbConnection db = objCon.GetConnection())
            //{
            //    var queryParameters = new DynamicParameters();
            //    if (!string.IsNullOrEmpty(Activity))
            //    queryParameters.Add("@Activity", Activity);

            //    list = db.Query<T>(spname, queryParameters,null,false,999999999,CommandType.StoredProcedure).ToList();
            //}
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 200000;
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spname;
            if (!string.IsNullOrEmpty(Activity))
                cmd.Parameters.AddWithValue("@Activity", Activity);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            objlist = Encrypt.ConvertDataTableToList<T>(dt);
            con.Close();
            return objlist;
        }

        public IEnumerable<T> GetList(string spname, DynamicParameters param)
        {
            using (IDbConnection db = DapperCon)
            {
                DapperCon.Open();
                return db.Query<T>(spname, param, commandType: CommandType.StoredProcedure);
            }
        }




        public List<T> GetListOptimized(string spname, string Activity, int? Id = 0)
        {
            using (IDbConnection db = DapperCon)
            {
                DapperCon.Open();
                var queryParameters = new DynamicParameters();
                if (!string.IsNullOrEmpty(Activity))
                    queryParameters.Add("@Activity", Activity);
                if (Id != 0)
                    queryParameters.Add("@id", Id);
                return db.Query<T>(spname, queryParameters, commandType: CommandType.StoredProcedure).ToList<T>();
            }
        }

        
        public T GetRecordsById(string spname, string Activity, int id)
        {

            List<T> objlist = new List<T>();
            Connection objCon = new Connection();
            SqlConnection con = objCon.GetConnection();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 2000;
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spname;
            cmd.Parameters.AddWithValue("@id", id);
            if (!string.IsNullOrEmpty(Activity))
                cmd.Parameters.AddWithValue("@Activity", Activity);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            objlist = Encrypt.ConvertDataTableToList<T>(dt);

            con.Close();

            return objlist.FirstOrDefault();
        }

        public DataTable GetTableById(string spname, string Activity, int? id = 0)
        {
            Connection objCon = new Connection();
            SqlConnection con = objCon.GetConnection();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 2000;
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spname;
            if (id != 0)
                cmd.Parameters.AddWithValue("@id", id);
            if (!string.IsNullOrEmpty(Activity))
                cmd.Parameters.AddWithValue("@Activity", Activity);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            return dt;
        }

        public DataSet GetDataSetById(string spname, string Activity, int? id = 0)
        {
            Connection objCon = new Connection();
            SqlConnection con = objCon.GetConnection();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 2000;
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spname;
            if (id != 0)
                cmd.Parameters.AddWithValue("@id", id);
            if (!string.IsNullOrEmpty(Activity))
                cmd.Parameters.AddWithValue("@Activity", Activity);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            adp.Fill(dt);
            con.Close();
            return dt;
        }
     

        public List<T> GetAllById(string spname, string Activity, int? id)
        {
            List<T> objlist = new List<T>();
            Connection objCon = new Connection();
            SqlConnection con = objCon.GetConnection();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 200000;
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spname;
            if (id != 0)
                cmd.Parameters.AddWithValue("@id", id);
            if (!string.IsNullOrEmpty(Activity))
                cmd.Parameters.AddWithValue("@Activity", Activity);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            objlist = Encrypt.ConvertDataTableToList<T>(dt);
            con.Close();

            return objlist;
        }
        public string Insert(string spname,DynamicParameters  param)
        {
            using (IDbConnection db = DapperCon)
            {
                DapperCon.Open();
                var result = db.Execute(spname, param, commandType: CommandType.StoredProcedure);
                return Convert.ToString(result);
            }
       
        }
        public int ExecuteScalar(string spname, DynamicParameters param)
        {
            using (IDbConnection db = DapperCon)
            {
                DapperCon.Open();
                var result = db.ExecuteScalar(spname, param, commandType: CommandType.StoredProcedure);
                return Convert.ToInt32(result);
            }

        }
        public List<T> GetAllByUserId(string spname, string Activity, string id)
        {
            List<T> objlist = new List<T>();
            Connection objCon = new Connection();
            SqlConnection con = objCon.GetConnection();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 200000;
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spname;
            cmd.Parameters.AddWithValue("@id", id);
            if (!string.IsNullOrEmpty(Activity))
                cmd.Parameters.AddWithValue("@Activity", Activity);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            objlist = Encrypt.ConvertDataTableToList<T>(dt);
            con.Close();

            return objlist;
        }

    }

}